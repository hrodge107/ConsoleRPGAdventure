using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    public static class Combat
    {
        public static Random random = new Random();

        public static bool StartEncounter(Player player, CombatArea combatArea, Map currentMap)
        {
            Enemy templateEnemy = combatArea.PotentialEnemies[random.Next(combatArea.PotentialEnemies.Count)];
            Enemy enemy = (Enemy)Activator.CreateInstance(templateEnemy.GetType());

            // save reference of original background
            AsciiImage originalBackground = combatArea.BackgroundArt;

            // change bg to enemyArt
            combatArea.BackgroundArt = enemy.EnemyArt;

            // Start the fight
            Fight(player, enemy, currentMap, true);

            // turn bg back to normal
            combatArea.BackgroundArt = originalBackground; // or originalBackground

            // Show battle result
            if (player.IsAlive())
            {
                player.PlayerStatus = GameState.Exploring;
                return true;
            }
            
            // this means player died
            else
            {
                Console.Clear();
                EntityImage.ShowGrave(player.Name, enemy.Name);
                Console.ReadKey();
                return false;
            }

        }

        public static bool StartBossEncounter(Player player, BossArea bossArea, Map currentMap)
        {
            Enemy boss = bossArea.Boss;

            AsciiImage originalBackground = bossArea.BackgroundArt;
            bossArea.BackgroundArt = boss.EnemyArt;

            Fight(player, boss, currentMap, false);


            bossArea.BackgroundArt = originalBackground;

            if (player.IsAlive())
            {
                player.PlayerStatus = GameState.Exploring;
                bossArea.IsCleared = true; // Mark boss as defeated
                return true;
            }
            
            else
            {
                Console.Clear();
                EntityImage.ShowGrave(player.Name, boss.Name);
                Console.ReadKey();
                return false;
            }
        }


        // Handles the entire combat sequence
        public static void Fight(Player player, Enemy enemy, Map currentMap, bool canFlee)
        {
            CombatDisplay.ShowScreen(player, enemy, currentMap, true);

            while (player.IsAlive() && enemy.IsAlive())
            {
                int enemyDamage = enemy.GetAtkPower();  // <-- this is set outside the conditions unlike the get player atk because we need this to calculate "penalty damage" when trying to flee

                CombatDisplay.ShowScreen(player, enemy, currentMap);
                string duelResult = Duel(player, enemy, canFlee); // <-- starts hitbar

                Console.WriteLine();


                if (duelResult == "hit")
                {
                    int playerDamage = player.GetAtkPower();
                    int actualDamage = player.OnSuccessfulHit(playerDamage);

                    enemy.CurrentHp -= actualDamage;
                    enemy.CurrentHp = Math.Max(0, enemy.CurrentHp);

                    #region Final Boss Shenanigans
                    if (enemy is Death deathBoss && deathBoss.CanEnterNextPhase())
                    {
                        deathBoss.EnterNextPhase();
                        continue; 
                    }
                    #endregion

                    if (actualDamage != 0)
                    {
                        OutputHelper.BattleLog($"{player.Name} hit for {actualDamage} damage!", ConsoleColor.Green);
                    }
                }


                else if (duelResult == "flee")
                {
                    OutputHelper.BattleLog($"{player.Name} tries to flee...", ConsoleColor.Cyan);

                    int fleeChance = 25;

                    if (random.Next(1, 101) <= fleeChance)
                    {
                        OutputHelper.BattleLog($"You successfully escaped!", ConsoleColor.Green, " ");
                        return;  // <-- escaped the battle
                    }

                    else // <-- flee penalty so mechanic wont be abused
                    {
                        OutputHelper.BattleLog($"You failed to escape!", ConsoleColor.Red);

                        int penaltyDamage = Math.Max(1, enemyDamage / 2);

                        player.CurrentHp -= penaltyDamage;
                        player.CurrentHp = Math.Max(0, player.CurrentHp);
                        OutputHelper.BattleLog($"{enemy.Name} hits you for {penaltyDamage} damage!", ConsoleColor.Red);
                    }
                }


                else // <-- means you missed, and now enemy will attack
                {
                    enemyDamage = player.OnMissedHit(enemyDamage); 

                    player.CurrentHp -= enemyDamage;
                    player.CurrentHp = Math.Max(0, player.CurrentHp);

                    if (enemyDamage > 0)
                    {
                        OutputHelper.BattleLog($"{enemy.Name} hit for {enemyDamage} damage!", ConsoleColor.Red);
                        enemy.OnSuccessfulHit(enemyDamage);
                    }

                }

                if (!player.IsAlive() || !enemy.IsAlive())
                {
                    break;
                }

                OutputHelper.ClearInputBuffer(); 
            }

            CombatDisplay.ShowScreen(player, enemy, currentMap);

            // Post combat logic
            if (player.IsAlive())
            {
                OutputHelper.BattleLog($"VICTORY, you have defeated the {enemy.Name}!", ConsoleColor.Green, " ");
                player.Gold += enemy.Bounty;
                OutputHelper.BattleLog($"You earned {enemy.Bounty} gold.", ConsoleColor.Yellow, " ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                OutputHelper.BattleLog($"DEFEAT, you have lost to the {enemy.Name}!", ConsoleColor.Red, " ");
            }

            OutputHelper.ClearInputBuffer();
        }

        // Handles the Hitbar and inidivual round's RNG variable.
        public static string Duel(Player player, Enemy enemy, bool canFlee)
        {
            int barSize = enemy.GetRandomBarSize();
            int markerSpeed = enemy.GetRandomSpeed();
            int targetAmount = enemy.GetRandomTargetAmount();

            HitBar hitBar = new HitBar(barSize, targetAmount);

            string duelResult = "miss";
            bool keyPressed = false;
            bool roundComplete = false;

            int gameDisplayTop = Console.CursorTop;
            
            OutputHelper.ClearInputBuffer(); 

            // Ranger Special Skill
            if (player is Ranger ranger)
            {
                var (newBarSize, newTargetAmount) = ranger.KeenEye(barSize, targetAmount);
                barSize = newBarSize;
                targetAmount = newTargetAmount;
                hitBar = new HitBar(barSize, targetAmount);
            }

            while (!roundComplete)
            {
                try
                {
                    Console.SetCursorPosition(0, gameDisplayTop); // <-- error
                }
                catch
                {
                    
                }


                hitBar.Print();

                int spacesToWrite = Math.Max(0, Console.WindowWidth - Console.CursorLeft);
                Console.Write(new string(' ', spacesToWrite));
                Console.WriteLine();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
                    {
                        duelResult = hitBar.IsHit() ? "hit" : "miss";
                        keyPressed = true;
                        roundComplete = true;
                    }

                    // Items
                    else if (key.Key == ConsoleKey.Q && player.TimeSlowPotion >= 1)
                    {
                        if (markerSpeed < 550)
                        {
                            markerSpeed += 150;
                            player.TimeSlowPotion--;
                            CombatDisplay.DisplayItemCounts(player);
                            OutputHelper.ItemUseLog("Used [Time Slow]!", ConsoleColor.Cyan);
                        }
                        else
                        {
                            OutputHelper.QuickLog("You can't slow down time anymore further!", ConsoleColor.Red);
                        }

                        continue;
                    }

                    else if (key.Key == ConsoleKey.W && player.PerceptionLens >= 1)
                    {
                        int ADDITIONAL_TARGETS = 5;
                        targetAmount = Math.Min(targetAmount + ADDITIONAL_TARGETS, barSize);
                        hitBar = new HitBar(barSize, targetAmount);
                        player.PerceptionLens--;
                        CombatDisplay.DisplayItemCounts(player);
                        OutputHelper.ItemUseLog("Used [Perception Lens]!", ConsoleColor.Cyan);
                        continue;
                    }

                    else if (key.Key == ConsoleKey.E && player.HpPotion >= 1)
                    {
                        //OutputHelper.ItemUseLog("Used [Health Potion]!", ConsoleColor.Cyan);

                        bool IsHealed = Heal(player);

                        if (IsHealed)
                        {
                            player.HpPotion--;
                        }

                        Console.SetCursorPosition(16,33);
                        CombatDisplay.DisplayHpBars(player, enemy);
                        CombatDisplay.DisplayItemCounts(player);

                        continue;
                    }

                    else if (key.Key == ConsoleKey.R)
                    {
                        if (canFlee)
                        {
                            duelResult = "flee";
                            roundComplete = true; // <-- ends round and calculates RNG if flee is successful or not
                        }
                        else
                        {
                            OutputHelper.QuickLog("\t\t\tYou cannot escape a boss fight!", ConsoleColor.Red);
                            continue;
                        }
                    }


                }

                if (!roundComplete)
                {
                    hitBar.MoveMarker();
                }

                Thread.Sleep(markerSpeed);
            }

            return duelResult;
        }
    

        // Use of Healing Potion. 
        public static bool Heal(Player player)
        {
            if (player.CurrentHp == player.MaxHp)
            {
                OutputHelper.QuickLog("\t\tYou're already at full health!", ConsoleColor.Yellow);
                return false;
            }
            else
            {
                int healAmount = (player.MaxHp / 5) + 2;
                int hpBeforeHealing = player.CurrentHp;
                player.CurrentHp += healAmount;
                if (player.CurrentHp > player.MaxHp)
                {
                    player.CurrentHp = player.MaxHp;
                }
                int actualHpRecovered = player.CurrentHp - hpBeforeHealing;

                OutputHelper.QuickLog($"\t\t\t\tYou recovered {actualHpRecovered} HP!", ConsoleColor.Green);
                return true;
            }
        }
    }
}

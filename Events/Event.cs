namespace ConsoleRPGAdventure
{
    public abstract class AreaEvent 
    {
    } 

    public class HealingArea : AreaEvent
    {
        public bool FirstRun { get; set; }
        public string Message { get; set; }

        public HealingArea(string message=null) 
        {
            FirstRun = true;
            Message = message;
        }
        
        public void Heal(Player player)
        {
            Random random = new Random();
            int HealAmount = random.Next((player.MaxHp / 4), (player.MaxHp / 4) + 5);

            if (HealAmount <= 0)
            {
                HealAmount = 1;
            }

            if (player.CurrentHp == player.MaxHp)
            {
                if (FirstRun)
                {
                    OutputHelper.StatusMessage("You're already at full health, come back to this area later to heal.", ConsoleColor.Green);
                    FirstRun = false;
                }
                return;
            }

            else
            {
                int hpBeforeHealing = player.CurrentHp;
                player.CurrentHp += HealAmount;
                if (player.CurrentHp > player.MaxHp)
                {
                    player.CurrentHp = player.MaxHp;
                }
                int actualHpRecovered = player.CurrentHp - hpBeforeHealing;


                OutputHelper.StatusMessage(Message, ConsoleColor.Green);
                OutputHelper.StatusMessage($"You recovered {actualHpRecovered} HP!", ConsoleColor.Green);
            }

        }
    }

    // Add new events here
    public class CutsceneArea : AreaEvent
    {
        public string[] Dialogue { get; set; }

        public CutsceneArea(params string[] lines)
        {
            Dialogue = lines;
        }

        public void ShowDialogue(Player player)
        {
            foreach (string s in Dialogue)
            {
                string displayText = s;
                if (player != null && s.Contains("{player}"))
                {
                    displayText = s.Replace("{player}", player.Name);
                }

                if (displayText.StartsWith('(') || displayText.StartsWith('\"'))
                {
                    OutputHelper.CutsceneLog(displayText, 2, ConsoleColor.Cyan);
                }
                else
                {
                    OutputHelper.CutsceneLog(displayText, 2);
                }
            }
        }
    }

    public class ShopArea : AreaEvent 
    {
        public bool FirstRun { get; set; }
        public MapDisplay display = new MapDisplay();

        public ShopArea()
        {
            FirstRun = true;
        }

        public void ShopIntro()
        {
            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("Welcome to my shop!");

            display.ShowCutscene(CutsceneImage.WizardDefault);
            OutputHelper.CutsceneLog("....");
            OutputHelper.CutsceneLog("Oh, it's you! We meet again.");

            OutputHelper.CutsceneLog("So, this here is my shop.");
            OutputHelper.CutsceneLog("You can exchange your gold here for some items.");
            OutputHelper.CutsceneLog("... surely, you know by now how to earn gold, right?");
            OutputHelper.CutsceneLog("Just keep walking around the open areas to find and slay monsters.");

            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("Anyway.");

            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("I recommend you buy items if you're gonna continue your adventure.");
            FirstRun = false;
        }

        public void Shop(Player player, Map currentMap)
        {
            if (FirstRun)
            {
                ShopIntro();
            }

            display.ShowCutscene(CutsceneImage.WizardGreeting);
            OutputHelper.CutsceneLog("Welcome to my shop!");
            OutputHelper.CutsceneLog("What would you like to buy for today?");

            bool running = true;
            while (running)
            {
                OutputHelper.QuickLog($"              Gold: {player.Gold} | Bag (Time Slow x{player.TimeSlowPotion}, Perception x{player.PerceptionLens}, Heal x{player.HpPotion})", ConsoleColor.Yellow);
                Cutscene.ShowBook(CutsceneImage.ItemBook, 19, 31);

                Console.SetCursorPosition(40, 42);
                string choice = Console.ReadLine().ToUpper().Trim();

                switch (choice)
                {
                    case "A": 
                        if ( (player.Gold - 10) >= 0 )
                        {
                            player.Gold -= 10;
                            player.TimeSlowPotion += 1;
                            ClearInputLine();
                            OutputHelper.CutsceneLog("Thank you for your purchase!",2,ConsoleColor.Green);
                            continue;
                        }
                        else
                        {
                            IllegalTransaction(display);
                        }

                        break;

                    case "B":
                        if ((player.Gold - 10) >= 0)
                        {
                            player.Gold -= 10;
                            player.PerceptionLens += 1;
                            ClearInputLine();
                            OutputHelper.CutsceneLog("Thank you for your purchase!", 2, ConsoleColor.Green);
                            continue;
                        }
                        else
                        {
                            IllegalTransaction(display);
                        }

                        break;

                    case "C":
                        if ((player.Gold - 5) >= 0)
                        {
                            player.Gold -= 5;
                            player.HpPotion++;
                            ClearInputLine();
                            OutputHelper.CutsceneLog("Thank you for your purchase!", 2, ConsoleColor.Green);
                            continue;
                        }
                        else
                        {
                            IllegalTransaction(display);
                        }

                        break;

                    case "D":
                        if ((player.Gold - 20) >= 0)
                        {
                            player.Gold -= 20;
                            player.Atk += 2;
                            ClearInputLine();
                            OutputHelper.CutsceneLog("Thank you for your purchase!", 2, ConsoleColor.Green);
                            OutputHelper.CutsceneLog("(You feel your strength increasing!)", 2, ConsoleColor.Cyan);
                            continue;
                        }
                        else
                        {
                            IllegalTransaction(display);
                        }

                        break;

                    case "E":
                        if ((player.Gold - 20) >= 0)
                        {
                            player.Gold -= 20;
                            player.MaxHp += 5;
                            ClearInputLine();
                            OutputHelper.CutsceneLog("Thank you for your purchase!", 2, ConsoleColor.Green);
                            OutputHelper.CutsceneLog("(You feel your vitality increasing!)", 2, ConsoleColor.Cyan);
                            continue;
                        }
                        else
                        {
                            IllegalTransaction(display);
                        }

                        break;

                    case "X":
                        display.ShowCutscene(CutsceneImage.WizardGreeting);
                        OutputHelper.CutsceneLog("Farewell, I hope we will meet again soon.");
                        Cutscene.EraseBook(CutsceneImage.ItemBook, 19, 31);
                        display.ShowGameScreen(player, currentMap);
                        running = false;
                        break;



                    default:    
                        display.ShowCutscene(CutsceneImage.WizardRead);
                        OutputHelper.CutsceneLog("Sorry, anything that is, we don't have that on the catalogue...");
                        OutputHelper.CutsceneLog("Please only choose from A, B, C, D, or E.");
                        break;
                }


            }
        }

        public void ClearInputLine()
        {
            Console.SetCursorPosition(40, 42);
            Console.Write(new string(' ', 5));
        }

        public void IllegalTransaction(MapDisplay display) // <-- sounds ominous lol
        {
            display.ShowCutscene(CutsceneImage.WizardPoint);
            OutputHelper.CutsceneLog("I don't think you have enough gold for that...", 2, ConsoleColor.Red);
        }
    }

    public class TreasureArea : CutsceneArea
    {
        public int HpBonus { get; set; }
        public int GoldBonus { get; set; }
        public int AtkBonus { get; set; }

        public TreasureArea(int hpBonus, int atkBonus, int goldBonus, params string[] lines) : base(lines)
        {
            HpBonus = hpBonus;
            AtkBonus = atkBonus;
            GoldBonus = goldBonus;
        }

        public void GetTreasure(Player player) 
        {
            ShowDialogue(player);

            player.MaxHp += HpBonus;
            player.CurrentHp += HpBonus;
            player.Atk += AtkBonus;

            if (AtkBonus >= 1 && HpBonus >= 1)
            {
                OutputHelper.StatusMessage($"{player.Name}'s ATK and HP increased!", ConsoleColor.Green);
            }


            if (GoldBonus > 0)
            {
                player.Gold += GoldBonus;
                OutputHelper.StatusMessage($"You got {GoldBonus} gold!", ConsoleColor.Yellow);
            }

        }
    }

    public class BossCutsceneArea : AreaEvent
    {
        public Enemy Boss { get; set; }
        public bool HasTriggered { get; set; }

        public BossCutsceneArea(Enemy boss)
        {
            Boss = boss;
            HasTriggered = false;
        }
    }
}



using System;
using System.Numerics;

namespace ConsoleRPGAdventure;

public enum GameState
{
    Exploring,
    InCombat
}

public abstract class Player
{
    public string Name { get; private set; }
    public int MaxHp { get; set; }
    public int CurrentHp { get; set; }
    public int Atk { get; set; }

    public int Gold { get; set; }
    public int TimeSlowPotion { get; set; }
    public int PerceptionLens { get; set; }
    public int HpPotion { get; set; }

    public Area CurrentLocation { get; set; }
    public CombatArea LastCombatArea { get; set; } // <-- prevents fight trigger after one ends
    public GameState PlayerStatus { get; set; }


    public Player(string name, Area startingLocation)
    {
        Name = name;
        Gold = 0;
        TimeSlowPotion = 2;
        PerceptionLens = 1;
        HpPotion = 1;
        CurrentLocation = startingLocation;
        PlayerStatus = GameState.Exploring;
    }

    // Logic Relevant to Fight System
    public bool IsAlive() => (CurrentHp >= 1);
    public abstract int GetAtkPower();
    public abstract int OnSuccessfulHit(int baseDamage);
    public abstract int OnMissedHit(int enemyDamage);


    // Map Traversal Logic
    public bool Move(Direction direction)
    {
        Area? nextArea = null;

        switch (direction)
        {
            case Direction.North:
                nextArea = CurrentLocation.North;
                break;
            case Direction.East:
                nextArea = CurrentLocation.East;
                break;
            case Direction.South:
                nextArea = CurrentLocation.South;
                break;
            case Direction.West:
                nextArea = CurrentLocation.West;
                break;
        }

        if (nextArea != null) // <-- successful movement
        {
            CurrentLocation = nextArea;
            return true;
        }

        else // <-- means you tried to move to a invalid place
        { 
            OutputHelper.QuickLog("You can't go that way...",ConsoleColor.Red);
            return false;
        }
    }


    // Handles input 
    public bool TryMove(Map currentMap)
    {
        while (true)
        {
            OutputHelper.ClearInputBuffer();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Move(Direction.North))
                        return true;
                    break;

                case ConsoleKey.DownArrow:
                    if (Move(Direction.South))
                        return true;
                    break;

                case ConsoleKey.RightArrow:
                    if (Move(Direction.East))
                        return true;
                    break;

                case ConsoleKey.LeftArrow:
                    if (Move(Direction.West))
                        return true;
                    break;

                case ConsoleKey.S:
                    SaveGameManager.SaveGame(this, currentMap);
                    continue;

                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("\n\t\tThanks for playing!");
                    return false;

                default:
                    OutputHelper.QuickLog("Use ARROW KEYS to move!", ConsoleColor.Yellow);
                    continue;
            }
        }
    }


    public void CheckAreaEvent(Map currentMap)
    {
        // NOTE: I could've done a "Catch-all" approach with a Basic "ExecuteEvent()" but... I ran into some flexibility issues  so this if else ladder stays.

        if (CurrentLocation.Event is BossCutsceneArea bossCutsceneArea)
        {
            if (!bossCutsceneArea.HasTriggered)
            {
                Cutscene.FinalBossCutscene(this);
                bossCutsceneArea.HasTriggered = true;
                PlayerStatus = GameState.InCombat;
            }
        }

        else if (CurrentLocation.Event is TreasureArea treasureArea)
        {
            treasureArea.GetTreasure(this);


            // Note: I should've used another attribute to tag this.

            if (treasureArea.GoldBonus == 0) // Knight Encampment
            {
                CurrentLocation.Event = new HealingArea("Oh, you're back, Hero? I also serve as a medic... so let me see your wounds.");
            }
            else if (treasureArea.GoldBonus == 88) // Queen
            {
                CurrentLocation.Event = new HealingArea("Oh, you're back, Hero? I can use healing magic... come here, let me see your wounds.");
            }
            else
            {
                CurrentLocation.Event = null;
            }

        }

        else if (CurrentLocation.Event is HealingArea healArea)
        {
            healArea.Heal(this);
        }


        else if (CurrentLocation.Event is CutsceneArea areaCutscene)
        {
            areaCutscene.ShowDialogue(this);
            CurrentLocation.Event = null;
        }


        else if (CurrentLocation.Event is ShopArea shop)
        {
            shop.Shop(this, currentMap);
        }


        else if (CurrentLocation is BossArea bossArea)
        {
            if (!bossArea.IsCleared)
            {
                PlayerStatus = GameState.InCombat;
            }
        }


        else if (CurrentLocation is CombatArea combatArea)
        {
            if (LastCombatArea == combatArea)
            {
                LastCombatArea = null; 
                return;
            }

            // Check spawn chance
            Random random = new Random();
            double roll = random.NextDouble(); //  value between 0.0 and 1.0

            if (roll < combatArea.SpawnChance)
            {
                PlayerStatus = GameState.InCombat;
                LastCombatArea = combatArea; 
            }
        }

    }
}

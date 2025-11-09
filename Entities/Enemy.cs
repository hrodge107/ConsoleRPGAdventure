using System;
using System.Data.Common;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleRPGAdventure
{
    public class Enemy
    {
        public string Name { get; private set; }
        public int CurrentHp { get; set; }
        public int MaxHp { get; set; }
        public (int, int) Atk { get; set; }
        public AsciiImage EnemyArt { get; set; }

        public (int, int) Speed { get; set; } // <-- thread speed (100,200)
        public (int, int) Dodge { get; set; } // <-- target amount (1,2)
        public (int, int) Size { get; set; } // <-- bar size (5,7)

        public int Bounty { get; private set; }

        private Random random = new Random();

        public Enemy(AsciiImage enemyArt, string name, int hp, (int, int) attackPower, (int, int) speed, (int, int) dodge, (int, int) size, int bounty)
        {
            Name = name;
            MaxHp = hp;
            CurrentHp = MaxHp;
            Atk = attackPower;
            EnemyArt = enemyArt;

            Speed = speed;
            Dodge = dodge;
            Size = size;

            Bounty = bounty;
        }

        public bool IsAlive()
        {
            return (CurrentHp >= 1);
        }

        public int GetAtkPower()
        {
            return random.Next(Atk.Item1, Atk.Item2 + 1);
        }

        public int GetRandomBarSize()
        {
            return random.Next(Size.Item1, Size.Item2 + 1);
        }

        public int GetRandomSpeed()
        {
            return random.Next(Speed.Item1, Speed.Item2 + 1);
        }

        public int GetRandomTargetAmount()
        {
            return random.Next(Dodge.Item1, Dodge.Item2 + 1);
        }

        public virtual void OnSuccessfulHit(int damage) { }
    }

    #region forest monster
    // Forest Monsters
    public class Goblin : Enemy
    {
        public Goblin() : base(EntityImage.Goblin, "Goblin", 5, (1, 3), (100, 200), (1, 3), (7, 10), 1) { }
    }

    public class Spider : Enemy
    {
        public Spider() : base(EntityImage.Spider, "Spider", 5, (1, 3), (100, 200), (1, 3), (5, 7), 2) { }
    }

    public class Mushroom : Enemy
    {
        public Mushroom() : base(EntityImage.Mushroom, "Mushroom", 10, (2, 3), (150, 200), (1, 3), (10, 15), 5) { }
    }

    public class Treant : Enemy
    {
        public Treant() : base(EntityImage.Treant, "Treant", 15, (2, 4), (150, 200), (2, 3), (10, 12), 5) { }
    }

    public class SnakeWithLegs : Enemy
    {
        public SnakeWithLegs() : base(EntityImage.SnakeWithLegs, "Legged Snake", 7, (1, 3), (70, 140), (2, 3), (5, 7), 3) { }
    }

    public class Mouse : Enemy
    {
        public Mouse() : base(EntityImage.Mouse, "Thunder Mouse", 3, (1, 1), (40, 70), (1, 1), (3, 5), 3) { }
    }

    #endregion


    #region Sea Monsters
    public class Serpent : Enemy
    {
        public Serpent() : base(EntityImage.Serpent, "Sea Serpent", 25, (5, 8), (100, 120), (0, 4), (10, 15), 20) { }
    }

    public class Octopus : Enemy
    {
        public Octopus() : base(EntityImage.Octupi, "Octopus", 15, (4, 6), (80, 90), (0, 3), (5, 9), 20) { }
    }

    public class Shark : Enemy
    {
        public Shark() : base(EntityImage.Shark, "Megalodon", 50, (8, 10), (120, 150), (1, 5), (15, 20), 100) { }
    }
    #endregion


    #region Mountain Monsters
    public class Yeti : Enemy
    {
        public Yeti() : base(EntityImage.Yeti, "Yeti", 45, (4, 7), (120, 150), (2, 4), (7, 8), 10) { }
    }

    public class Gryphon : Enemy
    {
        public Gryphon() : base(EntityImage.Gryphon, "Gryphon", 40, (4, 5), (70, 100), (1, 3), (5, 10), 10) { }
    }

    public class Raptor : Enemy
    {
        public Raptor() : base(EntityImage.Raptor, "Raptor", 35, (6, 7), (100, 120), (2, 4), (10, 15), 10) { }
    }

    public class Godzilla : Enemy
    {
        public Godzilla() : base(EntityImage.Godzilla, "Mini Kaiju", 35, (1, 10), (60, 80), (1, 2), (4, 7), 15) { }
    }

    public class Minotaur : Enemy
    {
        public Minotaur() : base(EntityImage.Minotaur, "Minotaur", 40, (6, 12), (60, 70), (1, 2), (5, 10), 50) { }
    }

    public class Bear : Enemy
    {
        public Bear() : base(EntityImage.Bear, "Kuma", 88, (10, 12), (60, 70), (1, 3), (15, 25), 100) { }
    }
    #endregion


    #region Fortress Enemy
    public class GiantSkeleton : Enemy
    {
        public GiantSkeleton() : base(EntityImage.Skeleton, "Giant Skeleton", 111, (15, 20), (50, 120), (1, 1), (15, 25), 44) { }
    }

    public class CastleMonster : Enemy
    {
        public CastleMonster() : base(BackgroundImage.CastleMonster, "Fortress Guardian", 150, (10, 12), (100, 120), (3, 5), (15, 25), 100) { }
    }

    public class Minion : Enemy
    {
        public Minion() : base(EntityImage.Minion, "Minion", 35, (5, 7), (50, 70), (1, 1), (3, 5), 20) { }
    }

    public class DragonKnight : Enemy
    {
        public DragonKnight() : base(EntityImage.Dragon, "Evil Dragon Knight", 144, (10, 12), (30, 80), (1, 3), (7, 10), 100) { }
    }
    #endregion


    // Final Boss
    public class Death  : Enemy
    {
        private int currentPhase = 1;
        private const int MAX_PHASES = 3;

        public int Phase => currentPhase;

        public Death() : base(EntityImage.Death, "???", 444, (10, 14), (40, 60), (1, 1), (5, 10), 999) { }

        public override void OnSuccessfulHit(int damage)
        {
            MaxHp += damage;
            CurrentHp += damage;
            OutputHelper.BattleLog("The enemy stole your HP!", ConsoleColor.Red);
        }

        public bool CanEnterNextPhase()
        {
            return currentPhase < MAX_PHASES && CurrentHp <= 0;
        }

        public void EnterNextPhase()
        {
            if (currentPhase >= MAX_PHASES)
                return;

            currentPhase++;

            // Restore HP
            CurrentHp = MaxHp;

            // Increase difficulty each phase
            Atk = (Atk.Item1 + 5, Atk.Item2 + 5);           
            Speed = (Math.Max(20, Speed.Item1 - 10), Math.Max(30, Speed.Item2 - 10)); 
            Size = (Math.Max(2, Size.Item1 - 2), Math.Max(2, Size.Item2 - 2));        

            ShowPhaseDialogue();
        }

        private void ShowPhaseDialogue()
        {
            switch (currentPhase)
            {
                case 2:
                    OutputHelper.BattleLog("Hah... Hahahaha!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("Impressive... You've actually pushed me to low HP.", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("But did you really think it would be THAT easy?", ConsoleColor.Red, "");

                    OutputHelper.BattleLog("Do you know?", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("The 'Dev' who created this game made me unbeatable.", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("All because he was too lazy to design an ending cutscene!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("And if you still dare to challenge me knowing that.", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("COME ON AND TRY IT!!!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("Let me show you my TRUE power!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog(">>> GM FULLY RESTORED HIS HP! <<<", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("The boss became faster and deadlier!", ConsoleColor.Red, "");
                    break;

                case 3:
                    OutputHelper.BattleLog("WHAT?! You're STILL standing?!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("This... This wasn't supposed to happen!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("I am the GAME MASTER! I control EVERYTHING!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("EVEN THE DEV DIDN'T TEST THIS FAR!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("\"FINE! I'll end this NOW!\"", ConsoleColor.DarkRed, "");
                    OutputHelper.BattleLog(">>> GM FULLY RESTORED AND ENTERED FINAL PHASE! <<<", ConsoleColor.DarkRed, "");
                    OutputHelper.BattleLog("The boss's power reached its peak!", ConsoleColor.Red, "");
                    OutputHelper.BattleLog("(This is my last chance - I need to finish him!)", ConsoleColor.Cyan, "");
                    break;
            }
        }
    }
}
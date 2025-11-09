using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    public class Swordsman : Player
    {
        private Random random = new Random();

        public Swordsman(string name, Area startingLocation) : base(name, startingLocation)
        {
            MaxHp = 15;
            CurrentHp = MaxHp;
            Atk = 3;
        }

        public override int GetAtkPower()
        {
            return Math.Max(1, random.Next(Atk - 1, Atk + 2)); ;
        }

        public override int OnSuccessfulHit(int baseDamage) => baseDamage; // <-- TODO: add mana points if successful hits that allows special skill use if have time to implement.


        // Swordsman reduces DMG
        public override int OnMissedHit(int enemyDamage) 
        {
            enemyDamage = (int)(enemyDamage * 0.75);
            enemyDamage = Math.Max(0, enemyDamage);

            if (enemyDamage == 0) // <-- should only happen if enemy atk value is too weak. In other words, rarely will happen.
            {
                OutputHelper.BattleLog("You successfully blocked the enemy attack!", ConsoleColor.Cyan);
            }

            return enemyDamage;
        }

    }


    public class Ranger : Player
    {
        private Random random = new Random();

        public Ranger(string name, Area startingLocation) : base(name, startingLocation)
        {
            MaxHp = 10;
            CurrentHp = MaxHp;
            Atk = 2; // <-- low atk if buying potion too?
        }

        // Special Ranger Skill - Makes the Hitbar bigger and increases targets to hit
        public (int newBarSize, int newTargetAmount) KeenEye(int barSize, int targetAmount)
        {
            int barIncrease = Math.Max(1, (int)(barSize * random.Next(30, 51) / 100.0));
            int newBarSize = barSize + barIncrease;

            int maxTargets = newBarSize - 1;
            int minTargets = 1;

            // calculate, 1 to 4 more targets
            int additionalTargets = random.Next(minTargets, Math.Min(4, maxTargets - targetAmount + 1));
            
            int newTargetAmount = targetAmount + additionalTargets;

            return (newBarSize, newTargetAmount);
        }

        public override int GetAtkPower()
        {
            return Math.Max(1, random.Next(Atk - 1, Atk + 2));
        }

        public override int OnSuccessfulHit(int baseDamage) => baseDamage;

        
        // Ranger has chance to Evade 
        public override int OnMissedHit(int enemyDamage)
        {
            int evadeChance = random.Next(1, 101);

            if (evadeChance <= 20) // 20% chance to evade attack
            {
                OutputHelper.BattleLog("You evaded the enemy attack!", ConsoleColor.Cyan);
                enemyDamage = 0;
            }

            return enemyDamage;
        }
    }


    public class Wizard : Player
    {
        public int chantCounter { get; set; } // <-- 3 consecutive hits
        private Random random = new Random();

        public Wizard(string name, Area startingLocation) : base(name, startingLocation)
        {
            MaxHp = 10;
            CurrentHp = MaxHp;
            Atk = 5;
        }

        public override int GetAtkPower()
        {
            int magicPowerAmp = random.Next(10, 20); // <-- because magic is strong, power adds.
            return random.Next(Atk - 1, Atk + 2) + magicPowerAmp;
        }

        public override int OnSuccessfulHit(int baseDamage)
        {
            chantCounter += 1;

            if (chantCounter >= 3)
            {
                OutputHelper.BattleLog("You finished your chanting!", ConsoleColor.Cyan);
                OutputHelper.BattleLog("      MAGIC BLAST!!!", ConsoleColor.Magenta);
                chantCounter = 0;
                return baseDamage;
            }

            else
            {
                OutputHelper.BattleLog($"  Magic Chant... ({chantCounter}/3)", ConsoleColor.Cyan);
                return 0; // <-- no damage, you're still chanting
            }

        }

        // There's a 40% chance that the chant will be disrupted. Does not reduce damage like the other 2 classes.
        public override int OnMissedHit(int enemyDamage)
        {
            int disruptChance = random.Next(1, 101);

            if (chantCounter > 0 && disruptChance <= 40)
            {
                OutputHelper.BattleLog("Your chant was disrupted!", ConsoleColor.Red);
                chantCounter = 0;
            }

            return enemyDamage;
        }
    }


    public class Admin : Player // <-- Best Class, super OP (testing only)
    {
        public Admin(string name, Area startingLocation) : base(name, startingLocation)
        {
            MaxHp = 100;
            CurrentHp = 50;
            Atk = 1;
            Gold = 100;
            TimeSlowPotion = 10;
            PerceptionLens = 10;
            HpPotion = 10;
        }

        public override int GetAtkPower()
        {
            return Atk * 999;
        }

        public override int OnSuccessfulHit(int baseDamage) => baseDamage;
        public override int OnMissedHit(int enemyDamage) => enemyDamage;
    }
}

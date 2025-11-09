using System;
using System.Collections.Generic;

namespace ConsoleRPGAdventure
{
    public class SaveData
    {
        // Player Data
        public string PlayerName { get; set; }
        public string PlayerRole { get; set; } 
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }
        public int Atk { get; set; }
        public int Gold { get; set; }
        public int TimeSlowPotion { get; set; }
        public int PerceptionLens { get; set; }
        public int HpPotion { get; set; }

        // Location
        public int CurrentX { get; set; }
        public int CurrentY { get; set; }

        // Cleared States 
        public Dictionary<string, bool> ClearedBossAreas { get; set; }
        public Dictionary<string, bool> ClearedEventAreas { get; set; }
        public Dictionary<string, string> ConvertedHealingAreas { get; set; }

        public SaveData()
        {
            ClearedBossAreas = new Dictionary<string, bool>();
            ClearedEventAreas = new Dictionary<string, bool>();
            ConvertedHealingAreas = new Dictionary<string, string>(); // <-- special healing areas
        }

        // => is shortcut for return
        public static string GetAreaData(int x, int y) => $"{x},{y}";
    }
}
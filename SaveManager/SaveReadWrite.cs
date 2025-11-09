using System;
using System.IO;
using System.Text;

namespace ConsoleRPGAdventure
{
    public static class SaveFileWriter
    {
        private const string SAVE_DIRECTORY = "SaveData";
        private const string SAVE_FILE_NAME = "save.txt";

        private static string SaveFilePath => Path.Combine(SAVE_DIRECTORY, SAVE_FILE_NAME);

        public static bool WriteSave(SaveData saveData)
        {
            try
            {
                if (!Directory.Exists(SAVE_DIRECTORY))
                {
                    Directory.CreateDirectory(SAVE_DIRECTORY);
                }

                using (StreamWriter writer = new StreamWriter(SaveFilePath, false, Encoding.UTF8))
                {
                    // Write header
                    writer.WriteLine("# CONSOLE RPG ADVENTURE - SAVE FILE");
                    writer.WriteLine();

                    // Write player data
                    writer.WriteLine("[PLAYER_DATA]");
                    writer.WriteLine($"Name={saveData.PlayerName}");
                    writer.WriteLine($"Role={saveData.PlayerRole}");
                    writer.WriteLine($"MaxHp={saveData.MaxHp}");
                    writer.WriteLine($"CurrentHp={saveData.CurrentHp}");
                    writer.WriteLine($"Atk={saveData.Atk}");
                    writer.WriteLine($"Gold={saveData.Gold}");
                    writer.WriteLine($"TimeSlowPotion={saveData.TimeSlowPotion}");
                    writer.WriteLine($"PerceptionLens={saveData.PerceptionLens}");
                    writer.WriteLine($"HpPotion={saveData.HpPotion}");
                    writer.WriteLine();

                    writer.WriteLine("[LOCATION_DATA]");
                    writer.WriteLine($"CurrentX={saveData.CurrentX}");
                    writer.WriteLine($"CurrentY={saveData.CurrentY}");
                    writer.WriteLine();

                    // Write cleared boss areas
                    writer.WriteLine("[CLEARED_BOSS_AREAS]");
                    foreach (var entry in saveData.ClearedBossAreas)
                    {
                        writer.WriteLine($"{entry.Key}={entry.Value}");
                    }
                    writer.WriteLine();

                    // Write cleared event areas
                    writer.WriteLine("[CLEARED_EVENT_AREAS]");
                    foreach (var entry in saveData.ClearedEventAreas)
                    {
                        writer.WriteLine($"{entry.Key}={entry.Value}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing save file: {ex.Message}");
                return false;
            }
        }

        public static bool SaveFileExists()
        {
            return File.Exists(SaveFilePath);
        }


        public static bool DeleteSaveFile()
        {
            try
            {
                if (SaveFileExists())
                {
                    File.Delete(SaveFilePath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static string GetSaveFilePath() => SaveFilePath;
    }



    public static class SaveFileReader
    {
        private const string SAVE_DIRECTORY = "SaveData";
        private const string SAVE_FILE_NAME = "save.txt";

        private static string SaveFilePath => Path.Combine(SAVE_DIRECTORY, SAVE_FILE_NAME);
        public static SaveData ReadSave()
        {
            if (!File.Exists(SaveFilePath))
            {
                throw new FileNotFoundException("Save file not found.");
            }

            SaveData saveData = new SaveData();
            string currentSection = "";

            try
            {
                using (StreamReader reader = new StreamReader(SaveFilePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip empty lines and comments
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        {
                            continue;
                        }

                        // Check section headers
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            currentSection = line.Trim('[', ']');
                            continue;
                        }

                        string[] parts = line.Split('=');
                        if (parts.Length != 2)
                        {
                            continue;
                        }

                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        // Process based on current section
                        switch (currentSection)
                        {
                            case "PLAYER_DATA":
                                ParsePlayerData(saveData, key, value);
                                break;

                            case "LOCATION_DATA":
                                ParseLocationData(saveData, key, value);
                                break;

                            case "CLEARED_BOSS_AREAS":
                                if (bool.TryParse(value, out bool bossCleared))
                                {
                                    saveData.ClearedBossAreas[key] = bossCleared;
                                }
                                break;

                            case "CLEARED_EVENT_AREAS":
                                if (bool.TryParse(value, out bool eventCleared))
                                {
                                    saveData.ClearedEventAreas[key] = eventCleared;
                                }
                                break;
                        }
                    }
                }

                return saveData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading save file: {ex.Message}", ex);
            }
        }

        private static void ParsePlayerData(SaveData saveData, string key, string value)
        {
            switch (key)
            {
                case "Name":
                    saveData.PlayerName = value;
                    break;

                case "Role":
                    saveData.PlayerRole = value;
                    break;

                case "MaxHp":
                    if (int.TryParse(value, out int maxHp))
                        saveData.MaxHp = maxHp;
                    break;

                case "CurrentHp":
                    if (int.TryParse(value, out int currentHp))
                        saveData.CurrentHp = currentHp;
                    break;

                case "Atk":
                    if (int.TryParse(value, out int atk))
                        saveData.Atk = atk;
                    break;

                case "Gold":
                    if (int.TryParse(value, out int gold))
                        saveData.Gold = gold;
                    break;

                case "TimeSlowPotion":
                    if (int.TryParse(value, out int timeSlowPotion))
                        saveData.TimeSlowPotion = timeSlowPotion;
                    break;

                case "PerceptionLens":
                    if (int.TryParse(value, out int perceptionLens))
                        saveData.PerceptionLens = perceptionLens;
                    break;

                case "HpPotion":
                    if (int.TryParse(value, out int hpPotion))
                        saveData.HpPotion = hpPotion;
                    break;
            }
        }

        private static void ParseLocationData(SaveData saveData, string key, string value)
        {
            switch (key)
            {
                case "CurrentX":
                    if (int.TryParse(value, out int x))
                        saveData.CurrentX = x;
                    break;

                case "CurrentY":
                    if (int.TryParse(value, out int y))
                        saveData.CurrentY = y;
                    break;
            }
        }
    }
}
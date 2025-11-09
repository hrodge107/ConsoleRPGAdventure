using System;
using System.Linq;

namespace ConsoleRPGAdventure
{
    public static class SaveGameManager
    {
        public static bool SaveGame(Player player, Map currentMap)
        {
            try
            {
                SaveData saveData = new SaveData
                {
                    PlayerName = player.Name,
                    PlayerRole = player.GetType().Name,
                    MaxHp = player.MaxHp,
                    CurrentHp = player.CurrentHp,
                    Atk = player.Atk,
                    Gold = player.Gold,
                    TimeSlowPotion = player.TimeSlowPotion,
                    PerceptionLens = player.PerceptionLens,
                    HpPotion = player.HpPotion,
                    CurrentX = player.CurrentLocation.X,
                    CurrentY = player.CurrentLocation.Y,
                };

                // event or boss has been cleared state
                foreach (var area in currentMap.GetAreas())
                {
                    string areaData = SaveData.GetAreaData(area.X, area.Y);

                    if (area is BossArea bossArea)
                    {
                        saveData.ClearedBossAreas[areaData] = bossArea.IsCleared;
                    }

                    if (area is EventArea eventArea)
                    {
                        bool isCleared = eventArea.Event == null;
                        saveData.ClearedEventAreas[areaData] = isCleared;

                        if (eventArea.Event is HealingArea healingArea)
                        {
                            saveData.ConvertedHealingAreas[areaData] = healingArea.Message ?? string.Empty;
                        }
                    }
                }

                bool success = SaveFileWriter.WriteSave(saveData);

                if (success)
                {
                    OutputHelper.StatusMessage("Game saved successfully!", ConsoleColor.Green);
                }
                else
                {
                    OutputHelper.StatusMessage("Failed to save game.", ConsoleColor.Red);
                }

                return success;
            }
            catch (Exception ex)
            {
                OutputHelper.StatusMessage($"Error saving game: {ex.Message}", ConsoleColor.Red);
                return false;
            }
        }


        public static (Player player, Map map) LoadGame()
        {
            try
            {
                SaveData saveData = SaveFileReader.ReadSave();

                // Create the map
                Map currentMap = new BeginnerMap();

                // Find the starting area based on saved coordinates
                Area startingArea = currentMap.GetAreas().FirstOrDefault(a => a.X == saveData.CurrentX && a.Y == saveData.CurrentY)!;

                // Create player based on saved role
                Player player = saveData.PlayerRole switch
                {
                    "Swordsman" => new Swordsman(saveData.PlayerName, startingArea),
                    "Ranger" => new Ranger(saveData.PlayerName, startingArea),
                    "Wizard" => new Wizard(saveData.PlayerName, startingArea),
                    _ => new Admin(saveData.PlayerName, startingArea), // <-- intentional easter egg 
                };

                // Restore player stats and area states
                player.MaxHp = saveData.MaxHp;
                player.CurrentHp = saveData.CurrentHp;
                player.Atk = saveData.Atk;
                player.Gold = saveData.Gold;
                player.TimeSlowPotion = saveData.TimeSlowPotion;
                player.PerceptionLens = saveData.PerceptionLens;
                player.HpPotion = saveData.HpPotion;


                foreach (var area in currentMap.GetAreas())
                {
                    string areaData = SaveData.GetAreaData(area.X, area.Y);

                    if (area is BossArea bossArea && saveData.ClearedBossAreas.ContainsKey(areaData))
                    {
                        bossArea.IsCleared = saveData.ClearedBossAreas[areaData];
                    }

                    if (area is EventArea eventArea)
                    {
                        if (saveData.ConvertedHealingAreas.ContainsKey(areaData))
                        {
                            string healingMessage = saveData.ConvertedHealingAreas[areaData];
                            eventArea.Event = new HealingArea(healingMessage);
                        }
                        else if (saveData.ClearedEventAreas.ContainsKey(areaData))
                        {
                            if (saveData.ClearedEventAreas[areaData])
                            {
                                eventArea.Event = null; // <-- means cleared
                            }
                        }
                    }
                }

                Console.Clear();
                OutputHelper.StatusMessage("Game loaded successfully!", ConsoleColor.Green);
                return (player, currentMap);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to load game: {ex.Message}", ex);
            }
        }


        public static bool SaveFileExists()
        {
            return SaveFileWriter.SaveFileExists();
        }


        public static bool ConfirmOverwrite()
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            OutputHelper.StatusMessage("Warning: Existing Game File has been detected.", ConsoleColor.Red);
            OutputHelper.StatusMessage("Starting a new game will overwrite your existing save.", ConsoleColor.White);
            
            string overwriteDecision = OutputHelper.CutsceneGetInput("Do you want to continue? (Y/N)").Trim().ToUpper();
            Console.WriteLine();

            while (true)
            {
                if (overwriteDecision == "Y" || overwriteDecision == "YES")
                {
                    SaveFileWriter.DeleteSaveFile();
                    OutputHelper.StatusMessage("Old save deleted. Starting new game...", ConsoleColor.Green);
                    return true;
                }
                else
                {
                    OutputHelper.StatusMessage("New game cancelled.");;
                    return false;
                }
            }
        }
    }
}
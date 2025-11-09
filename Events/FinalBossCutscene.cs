using System;

namespace ConsoleRPGAdventure
{
    public static class BossCutsceneHandler
    {
        public static bool HandleBossCutscene(Player player, BossCutsceneArea bossCutsceneArea, Map currentMap)
        {
            Area originalLocation = player.CurrentLocation;

            // Create temporary boss area
            BossArea tempBossArea = new BossArea(
                bossCutsceneArea.Boss.Name,
                player.CurrentLocation.BackgroundArt,
                player.CurrentLocation.X,
                player.CurrentLocation.Y,
                bossCutsceneArea.Boss);


            player.CurrentLocation = tempBossArea;
            bool battleWon = Combat.StartBossEncounter(player, tempBossArea, currentMap);

            // After battle
            player.CurrentLocation = originalLocation;

            if (battleWon)
            {
                ShowVictory();
                return true; 
            }

            return false; // lose
        }

        private static void ShowVictory()
        {
            Console.Clear();
            OutputHelper.StatusMessage("Wait... how did you actually win????");
            OutputHelper.StatusMessage("You messed with the text files right?");
            OutputHelper.StatusMessage("Or discovered the secret admin role with 999 atk?");
            OutputHelper.StatusMessage("Anyway, congrats...");
            OutputHelper.StatusMessage("Kudos to you if you managed to beat it fair and square... but I highly doubt that.");
            OutputHelper.StatusMessage("To be fair, i designed this game in a way that the items are OP if you farm enough of it.");
        }
    }
}
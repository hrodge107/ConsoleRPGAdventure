using System.Runtime.CompilerServices;
using ConsoleRPGAdventure;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            string choice = Launcher.ShowScreen();

            switch (choice)
            {
                case "1": // NEW GAME
                    StartGame();
                    break;

                case "2": // CONTINUE (Load Game)
                    ContinueGame();
                    break;

                case "3": // QUIT
                    return;
            }
        }
    }


    private static void StartGame()
    {
        // Set-up
        Map currentMap = new BeginnerMap();
        var player = Cutscene.Introduction(currentMap);
        var mapDisplay = new MapDisplay();

        RunGameLoop(player, currentMap, mapDisplay);
    }


    private static void ContinueGame()
    {
        try
        {
            // Load saved game
            var (player, currentMap) = SaveGameManager.LoadGame();
            var mapDisplay = new MapDisplay();

            Console.Clear();
            OutputHelper.StatusMessage($"Welcome back, {player.Name}!", ConsoleColor.Cyan);

            RunGameLoop(player, currentMap, mapDisplay);
        }
        catch (Exception ex)
        {
            Console.Clear();
            OutputHelper.StatusMessage($"\nError loading game - {ex.Message}", ConsoleColor.Red);
            Console.ReadKey(true);
        }
    }


    private static void RunGameLoop(Player player, Map currentMap, MapDisplay mapDisplay)
    {
        // Main game loop
        bool isRunning = true;
        while (isRunning)
        {
            mapDisplay.ShowGameScreen(player, currentMap);
            player.CheckAreaEvent(currentMap);

            if (player.PlayerStatus == GameState.InCombat)
            {
                bool battleWon = false;

                // Final Boss
                if (player.CurrentLocation.Event is BossCutsceneArea bossCutsceneArea)
                {
                    BossCutsceneHandler.HandleBossCutscene(player, bossCutsceneArea, currentMap);
                    return; 
                }

                // regular boss battles - one time encounter
                else if (player.CurrentLocation is BossArea bossArea)
                {
                    battleWon = Combat.StartBossEncounter(player, bossArea, currentMap);
                }

                // combat encounters - reoccuring random spawns
                else if (player.CurrentLocation is CombatArea combatArea)
                {
                    battleWon = Combat.StartEncounter(player, combatArea, currentMap);
                }

                // Means player died in battle
                if (battleWon == false) 
                {
                    return; 
                }
            }

            else if (player.PlayerStatus == GameState.Exploring)
            {
                PlayerDisplay.ShowStatus(player);
                isRunning = player.TryMove(currentMap); // <-- handles input, pressing [ESC] will make this false.
            }

        }
    }
}
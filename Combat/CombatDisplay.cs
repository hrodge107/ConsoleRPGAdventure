namespace ConsoleRPGAdventure
{
    public static class CombatDisplay
    {
        private const int LeftMargin = 16;
        private const int EnemyColumn = 88;

        private const int ItemLeft = 111;
        private const int ItemRow1 = 20;
        private const int ItemRow2 = 21;
        private const int ItemRow3 = 22;
        private const int ItemRow4 = 23;


        public static void ShowScreen(Player player, Enemy enemy, Map currentMap, bool firstEncounter=false)
        {
            Console.Clear();
            
            MapDisplay mapDisplay = new MapDisplay();
            mapDisplay.ShowGameScreen(player, currentMap);

            // Easter Egg
            string enemyName = (enemy.Name == "Goblin") ? "creature that\n\t\t\t\t\t     looks like you" : enemy.Name;
           
            if (firstEncounter)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\t\t\t\t\t          === COMBAT START ===");
                Console.Write("\t\t\t\t\t     ");
                string message = $"  You encountered a {enemyName}!";
                foreach (char c in message)
                {
                    Console.Write(c);
                    Thread.Sleep(10);
                }
                Console.ResetColor();
                Console.ReadKey();
                firstEncounter = false;
            }
            else
            {
                Console.WriteLine("\t\t\t\t\t\t=== COMBAT ENCOUNTER ===\n");
            }

            DisplayHpBars(player, enemy);
            DisplayItemCounts(player);
        }

        public static void DisplayHpBars(Player player, Enemy enemy)
        {
            Console.SetCursorPosition(LeftMargin, Console.CursorTop);
            Console.Write($"{player.Name}: [ ");
            DisplayColoredBar(player.CurrentHp, player.MaxHp, ConsoleColor.Green);
            Console.Write(" ]");

            Console.SetCursorPosition(EnemyColumn, Console.CursorTop);
            Console.Write($"{enemy.Name}: [ ");
            DisplayColoredBar(enemy.CurrentHp, enemy.MaxHp, ConsoleColor.Red);
            Console.Write(" ]");

            Console.WriteLine(); 

            Console.SetCursorPosition(LeftMargin, Console.CursorTop);
            Console.Write($"HP: {player.CurrentHp}/{player.MaxHp}");

            Console.SetCursorPosition(EnemyColumn, Console.CursorTop);
            Console.WriteLine($"HP: {enemy.CurrentHp}/{enemy.MaxHp}");
        }

        public static void DisplayItemCounts(Player player)
        {
            Console.SetCursorPosition(ItemLeft, ItemRow1-1);
            Console.Write($"  ==");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ACTIONS ");
            Console.ResetColor();
            Console.Write($"==");


            // Time Slow Potion
            Console.SetCursorPosition(ItemLeft, ItemRow1);
            Console.Write(new string(' ', 30)); 
            Console.SetCursorPosition(ItemLeft, ItemRow1);
            OutputHelper.WriteKeyAction($"Q - Time Slow: {player.TimeSlowPotion}");

            // Perception Lens
            Console.SetCursorPosition(ItemLeft, ItemRow2);
            Console.Write(new string(' ', 30)); 
            Console.SetCursorPosition(ItemLeft, ItemRow2);
            OutputHelper.WriteKeyAction($"W - Target Up: {player.PerceptionLens}");

            // HP Potion
            Console.SetCursorPosition(ItemLeft, ItemRow3);
            Console.Write(new string(' ', 30));
            Console.SetCursorPosition(ItemLeft, ItemRow3);
            OutputHelper.WriteKeyAction($"E - Heal: {player.HpPotion}");

            // HP Potion
            Console.SetCursorPosition(ItemLeft, ItemRow4);
            Console.Write(new string(' ', 30));
            Console.SetCursorPosition(ItemLeft, ItemRow4);
            OutputHelper.WriteKeyAction($"R - Escape");

            // For Hit Bar
            Console.SetCursorPosition(LeftMargin, 35);
        }

        public static void DisplayColoredBar(int currentHp, int maxHp, ConsoleColor color)
        {
            int totalBlocks = 10; // Fixed number of blocks for visual consistency
            int filledBlocks = (int)Math.Ceiling((double)currentHp / maxHp * totalBlocks);
            filledBlocks = Math.Max(0, Math.Min(filledBlocks, totalBlocks)); // <-- Ensure filled blocks doesn't exceed total or go negative


            Console.ForegroundColor = color;
            for (int i = 0; i < filledBlocks; i++)
            {
                Console.Write("█");
            }

            Console.ForegroundColor = ConsoleColor.DarkGray; // <-- empty blocks
            for (int i = filledBlocks; i < totalBlocks; i++)
            {
                Console.Write("█");
            }

            Console.ResetColor();
        }
    }
}
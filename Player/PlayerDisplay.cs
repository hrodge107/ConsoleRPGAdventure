using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    public static class PlayerDisplay
    {
        public static void ShowStatus(Player player, string Message = "Exploring...")
        {
            Console.WriteLine($"\t\t{Message}");
            Console.WriteLine($"\t\tLocation: {player.CurrentLocation.Name}\n");
            ShowHp(player);
            Console.Write($"\t\tGold: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{player.Gold}\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\t\tS");
            Console.ResetColor();
            Console.Write(" - Save Game");

            //Console.Write("\t\tAvailable Exits: ");
            //if (player.CurrentLocation.North != null) Console.Write("North ");
            //if (player.CurrentLocation.South != null) Console.Write("South ");
            //if (player.CurrentLocation.East != null) Console.Write("East ");
            //if (player.CurrentLocation.West != null) Console.Write("West ");
            //Console.WriteLine("\n\t\t------------------------------------------");
            //Console.WriteLine("\t\tUse ARROW KEYS to move.");

            Console.SetCursorPosition(0, 0);
        }

        public static void ShowHp(Player player)
        {
            int totalBlocks = player.MaxHp;
            int filledBlocks = player.CurrentHp;

            Console.Write("\t\t[ ");
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < filledBlocks; i++)
            {
                Console.Write("█");
            }

            // Draw empty spaces
            Console.ForegroundColor = ConsoleColor.DarkGray;

            for (int i = filledBlocks; i < totalBlocks; i++)
            {
                Console.Write("█");
            }
            Console.ResetColor();

            Console.WriteLine(" ]");
            Console.WriteLine($"\t\tHP: {player.CurrentHp}/{player.MaxHp}\n");
        }
    }
}

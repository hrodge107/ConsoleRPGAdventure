using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    // Note: This are hard-coded values for displaying text. It adds spacing and directs the cursor to a set positin
    public static class OutputHelper
    {
        public static void SetConsoleSize()
        {
            try
            {
                Console.SetWindowSize(200, 35);
                Console.SetBufferSize(200, 35);
            }
            catch (ArgumentOutOfRangeException)
            {
                int maxWidth = Console.LargestWindowWidth;
                int maxHeight = Console.LargestWindowHeight;

                int width = Math.Min(120, maxWidth);
                int height = Math.Min(35, maxHeight);

                Console.SetWindowSize(width, height);
                Console.SetBufferSize(width, height);
            }
        }

        public static void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }

        public static void StatusMessage(string message, ConsoleColor color= ConsoleColor.White)
        {

            Console.SetCursorPosition(16, 31);
            Console.Write(new string(' ', 100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(16, 31);
            //Console.Write($"\t\t");
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }
            Console.WriteLine();
            Console.ResetColor();
            
            ClearInputBuffer();
            Console.ReadKey(true);
        }

        public static void BattleLog(string message, ConsoleColor color = ConsoleColor.White, string padding= "       ")
        {

            Console.SetCursorPosition(20, 31);
            Console.Write(new string (' ',100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(20, 31);
            Console.Write($"\t\t\t{padding}");
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(5);
            }
            Console.ResetColor();
            
            ClearInputBuffer(); 
            Console.ReadKey(true);
        }

        public static void QuickLog(string message, ConsoleColor color = ConsoleColor.White, string padding = "       ")
        {

            Console.SetCursorPosition(16, 31);
            Console.Write(new string(' ', 100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(16, 31);

            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(5);
            }
            Console.ResetColor();

            ClearInputBuffer();
        }

        public static void ItemUseLog(string message, ConsoleColor color = ConsoleColor.White, string padding = "       ")
        {

            Console.SetCursorPosition(20, 31);
            Console.Write(new string(' ', 100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(20, 31);
            Console.Write($"\t\t\t{padding}");
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(5);
            }
            Console.ResetColor();

            ClearInputBuffer();
        }

        public static void CutsceneLog(string message, int speed = 0, ConsoleColor color = ConsoleColor.White, string padding = "       ")
        {
            Console.SetCursorPosition(16, 31);
            Console.Write(new string(' ', 100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(16, 31);
            //Console.Write($"\t");

            // Get delay per character based on speed
            int delayMs = speed switch
            {
                1 => 80,
                2 => 40,
                3 => 15,
                99 => 1,
                _ => 40
            };

            // Display each character with delay
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }

            Console.WriteLine();
            Console.ResetColor();
            
            ClearInputBuffer();
            Console.ReadKey(true);
        }

        public static string CutsceneGetInput(string message, int speed = 0, ConsoleColor color = ConsoleColor.White, string padding = "       ")
        {
            Console.SetCursorPosition(16, 31);
            Console.Write(new string(' ', 100));
            Console.ForegroundColor = color;
            Console.SetCursorPosition(16, 31);

            // Get delay per character based on speed
            int delayMs = speed switch
            {
                1 => 80,
                2 => 40,
                3 => 15,
                99 => 1,
                _ => 40
            };

            // Display each character with delay
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }

            Console.Write(" ");
            Console.ResetColor();

            ClearInputBuffer(); // Clear any buffered keys before getting input
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }
    
        public static void WriteKeyAction(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < message.Length; i++)
            {
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(message[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(message[i]);
                }
            }
        }
    }
}


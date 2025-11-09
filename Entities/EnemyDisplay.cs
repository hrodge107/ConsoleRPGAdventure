using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure.Entities
{
    internal class EnemyDisplay
    {
        public void ShowHp(Enemy enemy)
        {
            int totalBlocks = enemy.MaxHp;
            int filledBlocks = enemy.CurrentHp;

            Console.Write("\t\t[ ");
            Console.ForegroundColor = ConsoleColor.Red;

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
            Console.WriteLine($"\t\tHP: {enemy.CurrentHp}/{enemy.MaxHp}\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleRPGAdventure;

namespace ConsoleRPGAdventure
{

    public class HitBar
    {
        public DLL bar;
        public Node? currentNode;
        public bool isMovingRight;


        public HitBar(int size, int numberOfTargets = 1)
        {
            bar = CreateBarWithTargets(size, numberOfTargets);
            currentNode = bar.Head;
            isMovingRight = true;
            UpdateVisuals();
        }

        private DLL CreateBarWithTargets(int size, int numberOfTargets = 1)
        {
            DLL outputDLL = new DLL();
            Random random = new Random();

            numberOfTargets = Math.Min(numberOfTargets, size);

            HashSet<int> targetPositions = new HashSet<int>();
            while (targetPositions.Count < numberOfTargets)
            {
                int position = random.Next(0, size);
                targetPositions.Add(position);
            }

            for (int i = 0; i < size; i++)
            {
                if (targetPositions.Contains(i))
                {
                    outputDLL.Append("target");
                }
                else
                {
                    outputDLL.Append("empty");
                }
            }

            return outputDLL;
        }

        private void UpdateVisuals()
        {
            Node? current = bar.Head;
            while (current != null)
            {
                current.VisualDisplay = (current.Data == "target") ? "O" : "-";
                current = current.Next;
            }

            if (currentNode != null)
            {
                currentNode.VisualDisplay = "X";
            }
        }

        public void MoveMarker()
        {
            if (isMovingRight)
            {
                if (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                else
                {
                    isMovingRight = false;
                    currentNode = currentNode.Previous;
                }
            }
            else
            {
                if (currentNode.Previous != null)
                {
                    currentNode = currentNode.Previous;
                }
                else
                {
                    isMovingRight = true;
                    currentNode = currentNode.Next;
                }
            }

            UpdateVisuals();
        }

        public bool IsHit()
        {
            return (currentNode != null && currentNode.Data == "target");
        }

        public void Print()
        {

            Node? current = bar.Head;
            //Console.WriteLine();

            // Upper frame
            Console.Write(new string('\t', 6)); Console.Write("    ");
            Console.Write("╔");
            for (int i = 0; i<(bar.Size); i++)
            {
                Console.Write("═");
            }
            Console.WriteLine("╗");

            // Middle frame
            Console.Write(new string('\t', 6)); Console.Write("    ");
            Console.Write("║");
            while (current != null)
            {
                if (current.VisualDisplay == "X")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                }
                else if (current.Data == "target")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("█");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("░");
                }

                current = current.Next;
            }

            Console.ResetColor();
            Console.Write("║\n");


            // Double Middle
            current = bar.Head;
            Console.Write(new string('\t', 6)); Console.Write("    ");
            Console.Write("║");
            while (current != null)
            {
                if (current.VisualDisplay == "X")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                }
                else if (current.Data == "target")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("█");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("░");
                }

                current = current.Next;
            }

            Console.ResetColor();
            Console.Write("║");


            // Lower frame
            Console.WriteLine();
            Console.Write(new string('\t', 6)); Console.Write("    ");
            Console.Write("╚");
            for (int i = 0; i < (bar.Size); i++)
            {
                Console.Write("═");
            }
            Console.Write("╝");
        }

    }
}



//  Considerations:
//   - The hitbar was more hard than expected in a gameplay perspective (or did I just get used to the game??)
//   - Accordingly, the time limit further increased the difficulty of the game and complex implementation. It is removed for now, lead to headaches. 

// Sword Hitbar Version
// Looks decent, but the general hitbar looks good enough. Just laying it here in case.
/*
public void Print()
{

    Node? current = bar.Head;
    Console.WriteLine();

    // Upper frame
    Console.Write(new string('\t', 6)); Console.Write("  ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("     [");

    // Middle frame
    Console.Write(new string('\t', 6)); Console.Write("  ");
    Console.Write("@");
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.Write("xxxx");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("[{");

    while (current != null)
    {
        if (current.VisualDisplay == "X")
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("█");
        }
        else if (current.Data == "target")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("█");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
        }

        current = current.Next;
    }

    Console.ResetColor();
    Console.Write(">");
    Console.WriteLine();

    // Lower frame
    Console.Write(new string('\t', 6)); Console.Write("  ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("     [");
    Console.ResetColor();
}
*/
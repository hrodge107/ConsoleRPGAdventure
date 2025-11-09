using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPGAdventure
{
    public class Node
    {
        public string Data { get; set; }
        public string VisualDisplay { get; set; }
        public Node? Next { get; set; }
        public Node? Previous { get; set; }

        public Node(string data)
        {
            Data = data;
            Next = null;
            Previous = null;
            VisualDisplay = (Data == "target") ? "█" : "█";
        }
    }

    public class DLL
    {
        public Node? Head { get; set; }
        public Node? Tail { get; set; }
        public int Size { get; set; }

        public DLL()
        {
            Head = null;
            Tail = null;
            Size = 0;
        }

        public void Append(string data)
        {
            Node newNode = new Node(data);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail!.Next = newNode;
                newNode.Previous = Tail;
                Tail = newNode;
            }
            Size++;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Node
    {
        public string Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node() { }

        public Node(string data, Node left, Node right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public Node(string data)
        {
            Data = data;
        }

        //public void CreateTree(int elements)
        //{
        //    if (elements == 0)
        //        return;

        //    CreateTree(elements--);
            
            
        //}

        //public override string ToString()
        //{
        //    PrintNode(this);
        //}

        private static bool IsEqual(Node a, Node b, ref int numComparisons)
        {
            numComparisons++;
            if (a == null && b == null)
                return true;
            if (a == null)
                return false;

            if (b == null)
                return false;


            if (a.Data == b.Data)
            {
                if (IsEqual(a.Left, b.Left, ref numComparisons) && IsEqual(a.Right, b.Right, ref numComparisons))
                    return true;
                return false;

            }
            return false;
        }

        public override bool Equals(object obj)
        {
            int numComparisons = 0;
            var result = IsEqual(this, obj as Node, ref numComparisons);
            Console.WriteLine("Number of Comparisons: " + numComparisons);
            return result;

        }

        public bool IsLeafNode()
        {
            return Left == null && Right == null;
        }

        
    }

    class Program
    {
        public static Node FindNode(Node rootNode, string search)
        {
            if (rootNode == null)
                return null;

            if (rootNode.Data == search)
                return rootNode;

            Node node;

            if ((node = FindNode(rootNode.Left, search)) != null)
                return node;

            if ((node = FindNode(rootNode.Right, search)) != null)
                return node;

            return null;

        }

        public static void PreOrderPrint(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Data);
                PreOrderPrint(node.Left);
                PreOrderPrint(node.Right);
            }
            
        }

        public static void InOrderPrint(Node node)
        {
            if (node != null)
            {
                InOrderPrint(node.Left);
                Console.WriteLine(node.Data);
                InOrderPrint(node.Right);
            }
        }


        public static void PostOrderPrint(Node node)
        {
            if (node == null)
                return;

            PostOrderPrint(node.Left);
            PostOrderPrint(node.Right);
            Console.WriteLine(node.Data);
        }

        
        static void Main(string[] args)
        {
            var root = new Node
            {
                Data = "A",
                Left = new Node("B", new Node("C"), new Node("D", new Node("E"), new Node("F", null, new Node("G")) ) ),
                Right = new Node("H", new Node("I"), new Node("J") )
            };

            var root2 = new Node
            {
                Data = "A",
                Left = new Node("B", new Node("C"), new Node("D", new Node("E"), new Node("F", null, new Node("G")))),
                Right = new Node("H", new Node("I"), new Node("J", new Node("K", new Node("L"), null ), new Node("M")  ))
            };

            Console.WriteLine("Node A = Node B: " + root.Equals(root2));

            Console.WriteLine("Pre-order");
            PreOrderPrint(root);
            Console.WriteLine("In-order");
            InOrderPrint(root);
            Console.WriteLine("Post-order");
            PostOrderPrint(root);

            string s;
            do
            {
                Console.Write("Enter Command: ");
                s = Console.ReadLine();
                var foundNode = FindNode(root, s);

                if (foundNode == null)
                    Console.WriteLine("Node Not found " + s);
                else
                {
                    Console.WriteLine("Found Node!!!!");
                }

            } while (s != "STOP");

            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeApplication
{
    public class Program
    {
        public static void Main()
        {
            var bst = new Bst(6);
            bst.InsertNode( 5);
            bst.InsertNode(10);
            bst.InsertNode(8);
            bst.InsertNode(4);
            bst.InsertNode(2);
            bst.InsertNode(3);
            bst.InsertNode(9);
            bst.InsertNode(7);
            bst.InsertNode(11);

            Console.WriteLine("Pre Order Print.......");

            bst.PreOrderPrint();
            Console.WriteLine("Count: " +  bst.Count());
            Console.WriteLine("Max Depth: " + bst.AltMaxDepth());
            bst.PrintDepths();
            Console.WriteLine(bst.Sum());
            Console.WriteLine(bst.IsBst());
            Console.WriteLine("Paths that add up to 31:");
            bst.PrintPathsWithSum(31);
            Console.WriteLine("Path with Largest Sum: ");
            bst.PrintPathWithLargestSum();
            Console.WriteLine("Is Post Order: " + bst.IsPostOrderTraversal(new int[10] {3,2, 4, 5, 7, 9, 8, 11, 10, 6}, 10));
            Console.WriteLine("Closest Node to 12: " + bst.ClosestNode(12));
            Console.WriteLine("Closest Node to 4: " + bst.ClosestNode(4));
            Console.WriteLine("Closest Node to 1: " + bst.ClosestNode(1));
            Console.WriteLine("Cousin Nodes: " + bst.AreCousinNodes(bst.FindNode(8), bst.FindNode(11)));
            Console.WriteLine("Cousin Nodes: " + bst.AreCousinNodes(bst.FindNode(5), bst.FindNode(9)));
            bst.PrintInRange(6, 10);
            Console.WriteLine("Predecessor for 11: " + bst.FindPredecessor(11) + " Successor: " + bst.FindSuccessor(11));
            Console.WriteLine("Predecessor for 10: " + bst.FindPredecessor(10) + " Successor: " + bst.FindSuccessor(10));
            Console.WriteLine("Predecessor for 2: " + bst.FindPredecessor(2) + " Successor: " + bst.FindSuccessor(2));
            Console.WriteLine("BST Adds to Sum 10: " + bst.AddsToSum(10));
            Console.WriteLine("BST Adds to Sum 17: " + bst.AddsToSum(17));
            Console.WriteLine("BST Adds to Sum 22: " + bst.AddsToSum(22));
            Console.WriteLine("BST Adds to Sum 5: " + bst.AddsToSum(5));
            Console.Write("Parents of Node 7: ");
            bst.PrintAncestors(7);
            Console.Write("Parents of Node 4: ");
            bst.PrintAncestors(4);


            bst.DeleteAllLeafNodes();
            bst.TreeToList();

            Console.WriteLine("NEW BALANCED BST");
            {
                var newBst = new Bst(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
                newBst.PreOrderPrint();
                newBst.PrintPaths();
                Console.WriteLine("NEW BST ENDS!!");
            }

            //Console.WriteLine("NEW BST CREATED FROM PREORDER INORDER");
            //{
            //    var newBst = new Bst(new List<int>(){2,3,4,5,6,7,8,9,10,11}, new List<int>(){6,5,4,2,3,10,8,7,9,11});
            //    newBst.PreOrderPrint();
            //    newBst.PrintPaths();
            //    Console.WriteLine("NEW BST ENDS!!");
            //}

            var notBstRoot = new Node(10, null);
            notBstRoot.Left = new Node(8, notBstRoot);
            notBstRoot.Right = new Node(20, notBstRoot);
            notBstRoot.Right.Left = new Node(7, notBstRoot.Right);
            notBstRoot.Right.Right = new Node(25, notBstRoot.Right);

            Console.WriteLine(Bst.IsBst(notBstRoot, int.MinValue, int.MaxValue));

           

            

            //bst.ToMirror();
            //bst.PreOrderPrint();
            //bst.ToMirror();
            //bst.PreOrderPrint();
            //Console.WriteLine("Has Path Sum 20: " + bst.HasPathSum(20));
            //Console.WriteLine("Has Path Sum 31: " + bst.HasPathSum(31));
            //Console.WriteLine("Has Path Sum 33: " + bst.HasPathSum(33));
            //Console.WriteLine("Has Path Sum 27: " + bst.HasPathSum(27));
            //Console.WriteLine("Has Path Sum 7: " + bst.HasPathSum(7));
            //Console.WriteLine("Has Path Sum 6: " + bst.HasPathSum(6));
            //Console.WriteLine("Has Path Sum 26: " + bst.HasPathSum(26));



            string valueAsString;
            int number;
            do
            {
                Console.Write("Enter value to delete: ");
                valueAsString = Console.ReadLine();

                if (int.TryParse(valueAsString, out number))
                {

                    Console.WriteLine("Deleting " + number);
                    bst.DeleteNode(number);

                    Console.WriteLine("In Order Print.......");

                    bst.InOrderPrint();
                }

            } while (valueAsString != "STOP");

            Console.WriteLine("----------------Equality Test--------------------------");

            do
            {
                Console.Write("Enter values in Node 1: ");
                string node1Values = Console.ReadLine();
                Console.Write("Enter values in Node 2: ");
                string node2Values = Console.ReadLine();

                var node1 = new Bst(node1Values.Split(',').Select(int.Parse).ToList());
                var node2 = new Bst(node2Values.Split(',').Select(int.Parse).ToList());

                if (node1.Equals(node2))
                    Console.WriteLine("Node 1 equals Node 2");
                else
                {
                    Console.WriteLine("Node 1 and 2 are not equal");
                }

                Console.Write("Do you wanna continue?: ");
                valueAsString = Console.ReadLine();


            } while (valueAsString != "STOP");




        }
    }
    
}

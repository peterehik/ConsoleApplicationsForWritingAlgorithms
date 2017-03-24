using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeApplication
{
    public class Program
    {
        public static Node InsertInTree(Node root, int key )
        {
            if (root == null)
            {
                return  new Node(key);
            }

            if (key < root.Key)
                 root.Left =  InsertInTree(root.Left, key);

            if(key > root.Key) 
                root.Right = InsertInTree(root.Right, key);

            //if the key already exists, then do nothing. 
            return root;
        }

        public static Node GetMaxSuccessor(Node node)
        {
            if (node == null)
                return null;

            return GetMaxSuccessor(node.Right);

           //non recursive way.
            //while (true)
            //{
            //    if (node == null)
            //        return null;

            //    node = node.Right;
            //}
        }

        public static void ReplaceNodeInParent(Node node, Node childNode)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Left == node)
                    node.Parent.Left = childNode;
                else
                {
                    node.Parent.Right = childNode;
                }
            }

            if (childNode != null)
                childNode.Parent = node.Parent;

        }

        public static void DeleteFromTree(Node rootNode, int key)
        {
            if (rootNode == null)
                return;

            if (rootNode.Key != key)
            {
                if (rootNode.Key < key)
                    DeleteFromTree(rootNode.Left, key);
                else if (rootNode.Key > key)
                    DeleteFromTree(rootNode.Right, key);
            }
            else
            {

                if (rootNode.Left != null && rootNode.Right != null)
                {
                    var successor = GetMaxSuccessor(rootNode);
                    rootNode.Key = successor.Key;
                    DeleteFromTree(successor, successor.Key);
                }
                else if (rootNode.Left != null)
                {
                    ReplaceNodeInParent(rootNode, rootNode.Left);
                    
                }
                else if (rootNode.Right != null)
                {
                    ReplaceNodeInParent(rootNode, rootNode.Right);
                }
                else
                {
                    ReplaceNodeInParent(rootNode, null);

                }

            }
        }

        public static void InOrderPrint(Node node)
        {
            if (node != null)
            {
                InOrderPrint(node.Left);
                Console.WriteLine(node.Key);
                InOrderPrint(node.Right);
            }
        }

        public static void PreOrderPrint(Node node)
        {
            if (node != null)
            {
                Console.WriteLine(node.Key);
                PreOrderPrint(node.Left);
                PreOrderPrint(node.Right);
            }
        }

        static void Main(string[] args)
        {
            var treeKeys = new List<int> {6, 3, 5, 4, 2, 1, 8, 7, 10, 9}; //6 is root

            var root = InsertInTree(null, treeKeys.First());

            var currentNode = root;
            foreach (var key in treeKeys.Skip(1))
            {
                var node = InsertInTree(root, key);
                node.Parent = currentNode;
                currentNode = node;
            }


            Console.WriteLine("Pre-order");
            PreOrderPrint(root);
            Console.WriteLine("In-order");
            InOrderPrint(root);
            Console.WriteLine("Deleting 3.............");
            DeleteFromTree(root, 3);
            Console.WriteLine("Pre-order new root");
            PreOrderPrint(root);

            Console.ReadLine();
        }
    }
}

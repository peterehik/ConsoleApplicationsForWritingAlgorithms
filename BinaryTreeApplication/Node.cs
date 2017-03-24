using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeApplication
{
    public class BinarySearchTree
    {
        public Node Root { get; set; }

        public static Node InsertInTree(Node root, int key)
        {
            if (root == null)
            {
                return new Node(key);
            }

            if (key < root.Key)
                root.Left = InsertInTree(root.Left, key);

            if (key > root.Key)
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


        public void DeleteFromTree(Node rootNode, int key)
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
                    rootNode = rootNode.Left;
                }
                else if (rootNode.Right != null)
                {
                    rootNode = rootNode.Right;
                }
                else
                {
                    rootNode = null;
                }

            }
        }

    }


    public class Node
    {
        public int Key { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }

        public Node()
        {
            
        }

        public Node(int key)
        {
            Key = key;
        }
    }
}

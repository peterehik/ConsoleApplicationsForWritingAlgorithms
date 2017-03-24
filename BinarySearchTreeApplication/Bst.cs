using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;

namespace BinarySearchTreeApplication
{
    public class Bst
    {
        
        private readonly Node _root;

        public Bst(List<int> values)
        {
            int root = values.First();
            _root = new Node(root, null);

            foreach (var key in values.Skip(1))
            {
                InsertNode(key);
            }
        }

        public Bst(int rootkey)
        {
            _root = new Node(rootkey, null);
        }

        //public Bst(List<int> inorder, List<int> preorder)
        //{
        //    _root = ConstructTree(inorder, preorder);
        //}

        //public static Node ConstructTree(List<int> inorder, List<int> preOrder)
        //{
        //    var rootValue = preOrder.First();
        //    var rootNode = Insert(null, rootValue, null);

        //    foreach (var element in preOrder.Skip(1))
        //    {
        //        Insert(rootNode, element, null);
        //    }

        //    return rootNode;
        //}

        public Node InsertNode(int key)
        {
            return Insert(_root, key, null);
        }

        public Node FindNode(int key)
        {
            return FindNode(_root, key);
        }

        public Bst(int[] array)
        {
            _root = CreateTree(null, array, 0, array.Length-1);
        }

        public void PrintAncestors(int value)
        {
            PrintAncestors(_root, null, new int[100], 0, value);
        }

        public static void PrintAncestors(Node root, Node parent, int[] array, int arrayLen, int value)
        {
            if (root == null)
                return;
            if (parent != null)
            {
                array[arrayLen++] = parent.Key;
                
            }

            if(value < root.Key)
                PrintAncestors(root.Left, root, array, arrayLen, value );
            else if(value > root.Key)
                PrintAncestors(root.Right, root, array, arrayLen, value);
            else
            {
                for (int i = 0; i < arrayLen; i++)
                {
                    Console.Write(array[i] + " ");
                }
                Console.WriteLine("");
            }
        }

        public bool AddsToSum(int sum)
        {
            return AddsToSum(_root, FindMin(_root), FindMax(_root), sum);
        }

        
        public static bool AddsToSum(Node root, Node a, Node b, int sum)
        {
            if (a == null || b == null)
                return false;
            if (a.Key < b.Key)
            {
                if (a.Key + b.Key == sum)
                {
                    return true;
                }
                else if (a.Key + b.Key < sum)
                {
                    return AddsToSum(root, FindSuccessor(root, null, a.Key), b, sum);
                }
                return AddsToSum(root, a, FindPredecessor(root, null, b.Key), sum);
            }
            return false;
        }

        public Node FindPredecessor(int value)
        {
            return FindPredecessor(_root, null, value);
        }

        public Node FindSuccessor(int value)
        {
            return FindSuccessor(_root, null, value);
        }


        public static Node FindPredecessor(Node root, Node lastRight, int value)
        {
            if (root == null)
                return null;
            if (value > root.Key)
            {
                return FindPredecessor(root.Right, root, value);
            }
            if (value < root.Key)
                return FindPredecessor(root.Left, lastRight, value);

            if (root.Left == null)
                return lastRight;

            return FindMax(root.Left);
        }

        public static Node FindSuccessor(Node root, Node lastLeft, int value)
        {
            if (root == null)
                return null;
            if (value > root.Key)
            {
                return FindSuccessor(root.Right, lastLeft, value);
            }
            if (value < root.Key)
                return FindSuccessor(root.Left, root, value);

            if (root.Right == null)
                return lastLeft;

            return FindMin(root.Right);
        }

        public static int SumTree(Node root)
        {
            if (root == null)
                return 0;

            int oldValue = root.Key;
            root.Key = SumTree(root.Left) + SumTree(root.Right);
            return root.Key + oldValue;
        }

        
        public static Node CreateTree(Node parent, int[] array, int start, int end)
        {
            if (start <= end)
            {
                var rootIndex = start + (end-start)/2;
                var rootVal = array[rootIndex];
                var node = new Node(rootVal, parent);
                node.Left = CreateTree(node, array, start, rootIndex - 1);
                node.Right = CreateTree(node, array, rootIndex + 1, end);
                return node;
            }
            return null;
        }

        public static Node FindNode(Node root, int key)
        {
            if (root == null)
                return null;

            if (root.Key == key)
                return root;

            if (key < root.Key)
                return FindNode(root.Left, key);

            return FindNode(root.Right, key);
        }

        public void PrintInRange(int min, int max)
        {
             PrintInRange(_root, min, max);
            Console.WriteLine("");
        }

        private static void PrintInRange(Node root, int min, int max)
        {
            if (root == null)
                return;
            PrintInRange(root.Left, min, max);
            if (root.Key >= min && root.Key <= max)
            {
                Console.Write(root + " ");
            }
            PrintInRange(root.Right, min, max);
        }

        public void DeleteAllLeafNodes()
        {
            DeleteAllLeafNodes(_root);
        }
        private static void DeleteAllLeafNodes(Node root)
        {
            if (root != null)
            {
                if (root.Left == null && root.Right == null)
                {
                    if (root.Parent != null)
                    {
                        if (root.Parent.Left == root)
                        {
                            root.Parent.Left = null;
                        }
                        else if (root.Parent.Right == root)
                        {
                            root.Parent.Right = null;
                        }
                    }
                }
                else
                {
                    DeleteAllLeafNodes(root.Left);
                    DeleteAllLeafNodes(root.Right);
                }
            }
        }

        public bool AreCousinNodes(Node a, Node b)
        {
            int aValue = GetNodeLevel(_root, a.Key, 0);
            int bValue = GetNodeLevel(_root, b.Key, 0);

            return aValue == bValue && a.Parent != b.Parent;
        }

       
        private static int GetNodeLevel(Node root, int nodeValue, int level)
        {
            if (root == null)
                return 0;
            if (root.Key == nodeValue)
                return level + 1;
            if (nodeValue < root.Key)
                return GetNodeLevel(root.Left, nodeValue, level+1);
            return GetNodeLevel(root.Right, nodeValue, level+1);
        }

        public bool IsBalanced()
        {
            return IsBalanced(_root);
        }
        
        public static bool IsBalanced(Node node)
        {
            if (node == null)
                return true;

            int lHeight = MaxDepth(node.Left);
            int rHeight = MaxDepth(node.Right);

            if (Math.Abs(lHeight - rHeight) <= 1 && IsBalanced(node.Left) && IsBalanced(node.Right))
                return true;

            return false;
        }

        //public isPostOrderTraversal()

        //private static bool IsPostOrderTraversal(Node node, int[] array, int arrayLen, int index)
        //{
        //    if (node == null)
        //        return true;

        //    bool leftPostOrder = IsPostOrderTraversal(node.Left, array, arrayLen, index);
        //    bool rightPostOrder = IsPostOrderTraversal(node.Right, array, arrayLen, index);

        //    if (rightPostOrder && leftPostOrder && index < arrayLen)
        //    {
        //        return array[index] == node.Key;
        //    }

        //    return false;

        //}

        //public bool AddsToSum(int sum)
        //{
        //    return AddsToSum(FindMin(_root), FindMax(_root), sum);
        //}

        //public static Node GetNext(Node a, int key)
        //{
        //    if (a.Right != null)
        //    {
        //        return FindMin(a.Right);
        //    }
        //    if (a.Parent != null )
        //    {
                
        //    }
        //}

        //public static bool AddsToSum(Node a, Node b, int sum)
        //{
        //    if (a == null || b == null)
        //        return false;
        //    if (a.Key + b.Key == sum)
        //    {
        //        //Console.WriteLine(a.Key + " + " + b.Key);
        //        return true;
        //    }
        //    else if(a.Key + b.Key < sum)
        //    {
        //        return AddsToSum(FindMin(a.Right), b, sum);
        //    }
        //    return AddsToSum(a, FindMax(b.Left), sum);

        //}
        
        public bool IsPostOrderTraversal(int[] array, int arrayLen)
        {
            int index = 0;
            return (PostOrderTraverse(_root, array, arrayLen, ref index) && index == arrayLen);
        }

        public Node ClosestNode(int value)
        {
            return ClosestNode(_root, value);
        }

        private static Node ClosestNode(Node node, int value)
        {
            if (node == null)
                return null;

            if (value != node.Key)
            {
                var closestNode = value < node.Key ? ClosestNode(node.Left, value) : ClosestNode(node.Right, value);
                if (closestNode != null)
                {
                    if (Math.Abs(closestNode.Key - value) < Math.Abs(node.Key - value))
                        return closestNode;
                    return node;
                }
                return node;
            }
            return node;
            
        }

        //private static Node ClosestNode(Node node, int value, ref int minDifference)
        //{
        //    if (node == null)
        //        return null;

        //    if (node.Key == value)
        //        return node;

        //    var difference = Math.Abs(node.Key - value);
        //    if (difference < minDifference)
        //    {
        //        minDifference = difference;
        //        return 
        //    }


        //    var closestLeftNode = ClosestNode(node.Left, value, difference);
        //    var closestRightNode = ClosestNode(node.Right, value, difference);

        //    if (node.Key == value)
        //        return node;

        //    if (closestLeftNode != null && closestRightNode != null)
        //    {
        //        if (Math.Abs(closestLeftNode.Key - value) < Math.Abs(closestRightNode.Key - value))
        //        {
        //            return closestLeftNode;
        //        }
        //        return closestRightNode;
        //    }
        //    else if (closestLeftNode != null)
        //        return closestLeftNode;
        //    else if (closestRightNode != null)
        //        return closestRightNode;
        //    else
        //        return node;

        //}

        private static bool PostOrderTraverse(Node node, int[] array, int arrayLen, ref int index )
        {
            if (node != null)
            {
                if(PostOrderTraverse(node.Left, array, arrayLen, ref index) && PostOrderTraverse(node.Right, array, arrayLen, ref index) && index < arrayLen && (node.Key == array[index++]))
                    return true;
                return false;
            }
            return true;
        }
        public void PrintPathWithLargestSum()
        {
            var maxpath = PathWithLargestSum(_root, new int[1000], 0 );

            Console.WriteLine(string.Join(", ", maxpath));
        }

        private static void PathWithLargestSum(Node node, int[] currentPath, int currentPathLen, List<int> maxPath  )
        {
            if (node == null)
                return;
            currentPath[currentPathLen++] = node.Key;
            if (node.Left == null && node.Right == null)
            {
                int currentPathSum = currentPath.Take(currentPathLen).Sum();
                int maxPathSum = maxPath.Sum();

                if (currentPathSum >= maxPathSum)
                {
                    maxPath.Clear(); 
                    for(int i = 0; i < currentPathLen; i++)
                        maxPath.Add(currentPath[i]);
                }
            }
            else
            {
                PathWithLargestSum(node.Left, currentPath, currentPathLen, maxPath);
                PathWithLargestSum(node.Right, currentPath, currentPathLen, maxPath);
            }

        }

        private static List<int> PathWithLargestSum(Node node, int[] currentPath, int currentPathLen)
        {
            if (node == null)
                return new List<int>();
            currentPath[currentPathLen++] = node.Key;
            if (node.Left == null && node.Right == null)
            {
                return currentPath.Take(currentPathLen).ToList();
            }
            else
            {
                List<int> lPath = PathWithLargestSum(node.Left, currentPath, currentPathLen);
                List<int> rPath = PathWithLargestSum(node.Right, currentPath, currentPathLen);

                if (lPath.Sum() > rPath.Sum())
                    return lPath;
                return rPath;
            }

        }
        
        private static void PrintArray(int[] array, int arrayLen)
        {
            int i = 0;
            for (i = 0; i < arrayLen - 1; i++)
            {
                Console.Write(array[i] + ", ");
            }
            Console.Write(array[i]);
            Console.WriteLine("");
        }

        public void PrintPathsWithSum(int sum)
        {
            PrintPathsWithSum(_root, sum, new int[1000], 0 );
        }

        public static void PrintPathsWithSum(Node node, int sum, int[] pathSoFar, int arrayLen)
        {
            if (node == null)
                return;

            pathSoFar[arrayLen++] = node.Key;
            int remainder = sum - node.Key;
            if (node.Left == null && node.Right == null)
            {
                if (remainder == 0)
                {
                    PrintArray(pathSoFar, arrayLen);
                }
            }
            else
            {
                PrintPathsWithSum(node.Left, remainder, pathSoFar, arrayLen);
                PrintPathsWithSum(node.Right, remainder, pathSoFar, arrayLen);
            }
        }

        public static void PrintNodeLikeTree(Node node)
        {
            Console.Write(node.Key + "{" + node.Left.Key + "} -> ");
            var curNode = node.Right;
            var last = node.Left;

            while (curNode !=  node)
            {
                Console.Write(curNode.Key + "{" + curNode.Left.Key + "} -> ");
                curNode = curNode.Right;
            }
            Console.WriteLine("");
        }

        public void TreeToList()
        {
            var head = TreeToList(_root);
            PrintNodeLikeTree(head);
        }

        public static Node TreeToList(Node node)
        {
            //recursively convert left and right sub trees
            //convert single node to circular linked list, i.e. make left and right point to the same node
            //concatenate left subree with current node and concatenate the result with the right subtree
            if (node == null)
                return null;

            var leftTree = TreeToList(node.Left);
            var rightTree = TreeToList(node.Right);

            node.Right = node.Left = node;

            return Append(Append(leftTree, node), rightTree);

        }

        public static void Join(Node a, Node b)
        {
            a.Right = b;
            b.Left = a;
        }

        public static Node Append(Node a, Node b)
        {
            if (a == null)
                return b;
            if (b == null)
                return a;
            var aTail = a.Left;;
            var bTail = b.Left;

            Join(aTail, b);
            Join(bTail, a);

            return a;
        }

        public void DoubleTree()
        {
            DoubleTree(_root);
        }

        private static void DoubleTree(Node node)
        {
            if (node == null)
                return;

            var newNode = new Node(node.Key, node);
            newNode.Left = node.Left;
            //newNode.Right = node.Right;
            node.Left = newNode;

            DoubleTree(newNode.Left);
            DoubleTree(node.Right);
        }

        public int Count()
        {
            int count = Size(_root);
            return count;
        }

        public void PrintPaths()
        {
            //PrintPathsRecur(_root, new List<int>());
            PrintPathsRecur(_root, new int[1000], 0);
        }

        //won't work with repeating values in tree because of Remove function
        public static void PrintPathsRecur(Node node, List<int> pathSoFar)
        {
            if (node == null)
                return;

            pathSoFar.Add(node.Key);

            if (node.Left == null && node.Right == null)
            {
                Console.WriteLine(string.Join(", ", pathSoFar));
            }
            else
            {
                PrintPathsRecur(node.Left, pathSoFar);
                PrintPathsRecur(node.Right, pathSoFar);
            }
            pathSoFar.Remove(node.Key);

        }

        //more elegant solution
        public static void PrintPathsRecur(Node node, int[] pathSoFar, int pathLen)
        {
            if (node == null)
                return;

            pathSoFar[pathLen++] = node.Key;

            if (node.Left == null && node.Right == null)
            {
                int i;
                for (i = 0; i < pathLen - 1; i++)
                {
                    Console.Write(pathSoFar[i] + ", ");
                }
                Console.Write(pathSoFar[i] + Environment.NewLine);

            }
            else
            {
                PrintPathsRecur(node.Left, pathSoFar, pathLen);
                PrintPathsRecur(node.Right, pathSoFar, pathLen);
            }

        }

        public bool HasPathSum(int sum)
        {
            int currentSum = 0;
            return HasPathSum(_root, sum);
        }

        public void ToMirror()
        {
            Mirror(_root);
        }

        private static void Mirror(Node node)
        {
            if (node == null)
                return;

            if (node.Left == null && node.Right != null)
            {
                node.Left = node.Right;
                node.Right = null;
            }
            if (node.Left != null && node.Right == null)
            {
                node.Right = node.Left;
                node.Left = null;
            }
            if (node.Right != null && node.Left != null)
            {
                Node tempNode = node.Right;
                node.Right = node.Left;
                node.Left = tempNode;
            }
            Mirror(node.Left);
            Mirror(node.Right);
        }

        public static bool IsBst(Node node, int min, int max)
        {
            if (node == null)
                return true;

            if (node.Key < min || node.Key > max)
                return false;

            return IsBst(node.Left, min, node.Key) && IsBst(node.Right, node.Key, max);
        }


        public bool IsBst()
        {
            return IsBst(_root, int.MinValue, int.MaxValue);
           //return IsBst(_root);
        }


        

        private static Node FindMax(Node node)
        {
            if (node == null)
                return null;

            if (node.Right != null)
                return FindMax(node.Right);

            return node;
        }

        
        private static bool HasPathSum(Node node, int sum)
        {
            if (node == null)
                return sum == 0;

            int subSum = sum - node.Key;

            return HasPathSum(node.Left, subSum) || HasPathSum(node.Right, subSum);


        }

        
        public int Sum()
        {
            return GetSum(_root);
        }

        private static int GetSum(Node node)
        {
            if (node == null)
                return 0;

            int sum = node.Key;
            return sum + GetSum(node.Left) + GetSum(node.Right);
        }

        public int MaxDepth()
        {
            int maxDepth = MaxDepth(_root);
            return maxDepth;
        }

        public int AltMaxDepth()
        {
            int maxDepth = 0;
            MaxDepth(_root, 0, ref maxDepth);
            return maxDepth;
        }

        public void PrintDepths()
        {
            PrintDepths(_root, 0);
        }

        private static void PrintDepths(Node node, int depth)
        {
            if (node == null)
                return;

            depth++;

            if (node.Right == null && node.Left == null)
            {
                Console.Write(depth + " ");
            }
            else
            {
                PrintDepths(node.Left, depth);
                PrintDepths(node.Right, depth);
            }
        }

        private static void MaxDepth(Node node, int depth, ref int maxDepth)
        {
            if (node == null)
                return;

            depth++;

            if (node.Right == null && node.Left == null)
            {
                if (depth >= maxDepth)
                    maxDepth = depth;
            }
            else
            {
                MaxDepth(node.Left, depth, ref maxDepth);
                MaxDepth(node.Right, depth, ref maxDepth);
            }
        }

        public int MinValue()
        {
            return FindMin(_root).Key;
        }

        private static int MaxDepth(Node node)
        {
            if (node != null)
            {
                int lDepth = MaxDepth(node.Left);
                int rDepth = MaxDepth(node.Right);

                if (lDepth > rDepth)
                    return lDepth + 1;
                return rDepth + 1;
            }
            return 0;


        }

        private static int Size(Node node)
        {
            if (node == null)
                return 0;
            return Size(node.Left) + 1 + Size(node.Right);
        }

        public void DeleteNode(int key)
        {
            DeleteFromBst(_root, key);
        }

        public override bool Equals(object obj)
        {
            var otherTree = obj as Bst;
            if(otherTree != null)
                return Equals(_root, otherTree._root);
            return false;
        }

        private static bool Equals(Node node, Node otherNode)
        {
            if (node == null && otherNode == null)
                return true;

            if (node != null && otherNode != null)
            {
                if (node.Key == otherNode.Key && Equals(node.Left, otherNode.Left) && Equals(node.Right, otherNode.Right))
                        return true;
                return false;
            }

            return false;

        }

        private static Node Insert(Node node, int key, Node parent)
        {
            if (node == null)
                return new Node(key, parent);

            if (key <= node.Key)
                node.Left = Insert(node.Left, key, node);

            if (key > node.Key)
                node.Right = Insert(node.Right, key, node);

            return node;
        }

        public void PreOrderPrint()
        {
            PreOrderPrint(_root);
        }

        public void InOrderPrint()
        {
            InOrderPrint(_root);
        }

        private static void InOrderPrint(Node node)
        {
            if (node == null)
                return;

            InOrderPrint(node.Left);

            Console.Write(node.Key);
            if (node.Parent != null)
                Console.Write("->" + node.Parent.Key);
            Console.WriteLine("");

            InOrderPrint(node.Right);
        }


        private static void PreOrderPrint(Node node)
        {
            if (node == null)
                return;

            Console.Write(node.Key);
            if (node.Parent != null)
                Console.Write("->" + node.Parent.Key);
            Console.WriteLine("");

            PreOrderPrint(node.Left);
            PreOrderPrint(node.Right);
        }

        private static void ReplaceNodeInParent(Node node, Node childNode)
        {
            if (node.Parent != null)
            {
                if (node.Parent.Left.Key == node.Key)
                {
                    node.Parent.Left = childNode;
                }
                else if (node.Parent.Right.Key == node.Key)
                {
                    node.Parent.Right = childNode;
                }
            }
            if (childNode != null)
                childNode.Parent = node.Parent;
        }

        private static Node FindMin(Node node)
        {
            if (node == null)
                return null;
            Node curNode = node;

            while (curNode.Left != null)
            {
                curNode = curNode.Left;
            }

            return curNode;

        }

        private static void DeleteFromBst(Node root, int key)
        {
            if (root == null)
                return;

            if (key < root.Key)
                DeleteFromBst(root.Left, key);
            else if (key > root.Key)
                DeleteFromBst(root.Right, key);
            else
            {
                if (root.Right != null && root.Left != null)
                {
                    Node successor = FindMin(root.Right);
                    root.Key = successor.Key;
                    DeleteFromBst(successor, successor.Key);
                }
                else if (root.Right != null)
                    ReplaceNodeInParent(root, root.Right);
                else if (root.Left != null)
                    ReplaceNodeInParent(root, root.Left);
                else
                    ReplaceNodeInParent(root, null);

            }
        }



    }
}
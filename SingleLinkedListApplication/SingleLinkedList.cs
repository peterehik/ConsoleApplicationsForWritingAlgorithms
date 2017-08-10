using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedListApplication
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }

        public Node(int data, Node next = null)
        {
            Data = data;
            Next = next;
        }
    }

    public class SingleLinkedList
    {
        private Node _head ;
        public Node Head { get { return _head;  }  set { _head = value; } }

        public int Length
        {
            get { return Count(_head); }
        }

        public SingleLinkedList() { }

        public SingleLinkedList(Node node)
        {
            _head = node;
        }

        public SingleLinkedList(int head)
        {
            _head = new Node(head);
        }

        public void MergeSort()
        {
            var result = MergeSort(this);
            Head = result.Head;
        }

        public static SingleLinkedList IntersectList(SingleLinkedList a, SingleLinkedList b)
        {
            var result = new SingleLinkedList();
            IntersectList(result, a.Head, b.Head);
            return result;
        }
         //a and b are both sorted
        private static void IntersectList(SingleLinkedList result, Node a, Node b)
        {
            if (a == null || b == null)
                return;

            if (a.Data == b.Data)
            {
                result.InsertAt(a.Data, result.Length);
                IntersectList(result, a.Next, b.Next);
            }
            else if(a.Data < b.Data)
                IntersectList(result, a.Next, b);
            else 
                IntersectList(result, a, b.Next);
        }

        public static SingleLinkedList MergeSort(SingleLinkedList list)
        {
           
            //if list has more than one element
            if (list.Head.Next != null)
            {
                var lists = list.FrontBackSplit();
                var first = MergeSort(lists[0]);
                var second = MergeSort(lists[1]);

                var result = new SingleLinkedList();
                MergeSortedList(result, first, second);
                return result;
            }
            else
            {
                return new SingleLinkedList(list.Head);
            }
        }

        public static SingleLinkedList MergeSortedList(SingleLinkedList a, SingleLinkedList b)
        {
            var result = new SingleLinkedList();
            MergeSortedList(result, a, b);

            return result;
        }

        private static void MergeSortedList(SingleLinkedList result, SingleLinkedList a, SingleLinkedList b)
        {
            if (a.Head != null && b.Head != null)
            {
                if (a.Head.Data <= b.Head.Data)
                {
                    result.MoveHeadToTail(a);
                }
                else
                {
                    result.MoveHeadToTail(b);
                }

                MergeSortedList(result, a, b);
            }
            else
            {

                if (a.Head != null)
                {
                    result.MoveHeadToTail(a);
                    MergeSortedList(result, a, b);
                }
                else if (b.Head != null)
                {
                    result.MoveHeadToTail(b);
                    MergeSortedList(result, a, b);
                }
            }
        }

        public static SingleLinkedList ShuffleMerge(SingleLinkedList a, SingleLinkedList b)
        {
            var result = new SingleLinkedList();
            while (a.Head != null)
            {
                result.MoveHeadToTail(a);
                if (b.Head != null)
                {
                    result.MoveHeadToTail(b);
                }
            }

            while(b.Head != null)
                result.MoveHeadToTail(b);

            return result;
        }

        public List<SingleLinkedList> AlternateSplit()
        {
            SingleLinkedList firstList = new SingleLinkedList(null), secondList = new SingleLinkedList(null);
            var source = this;
            while (source.Head != null)
            {
                firstList.MoveHeadFrom(source);
                if (source.Head != null)
                    secondList.MoveHeadFrom(source);
            }

            return new List<SingleLinkedList>() { firstList, secondList };
        }

        public void MoveHeadFrom(SingleLinkedList source)
        {
            MoveHeadFrom(this, source);
        }

        public void MoveHeadToTail(SingleLinkedList source)
        {
            MoveHeadToTail(this, source);
        }

        private static void MoveHeadToTail(SingleLinkedList dest, SingleLinkedList source)
        {
            var a = source._head;
            var aNext = a.Next;

            source._head = aNext;
            a.Next = null;

            var curNode = dest._head;
            if (curNode == null)
            {
                dest._head = a;
            }
            else
            {
                while (curNode.Next != null)
                {
                    curNode = curNode.Next;
                }

                curNode.Next = a;
            }
        }

        private static void MoveHeadFrom(SingleLinkedList dest, SingleLinkedList source)
        {
            var a = dest._head;
            var b = source.Head;
            var bNext = b.Next;
            var aNext = a;

            a = b;
            a.Next = aNext;
            b = bNext;

            dest._head = a;
            source._head = b;
        }

        public void RemoveDuplicates()
        {
            RemoveDuplicates(_head, _head.Next);
        }

        private static void RemoveDuplicates(Node prevNode, Node curNode)
        {
            if (curNode == null)
                return;

            if (prevNode.Data == curNode.Data)
            {
                prevNode.Next = curNode.Next;
                RemoveDuplicates(prevNode, curNode.Next);
            }
            else
            {
                RemoveDuplicates(curNode, curNode.Next);
            }
        }

        public List<SingleLinkedList> FrontBackSplit()
        {
            return FrontBackSplit(_head, Length).Select(r => new SingleLinkedList(r)).ToList();
        }

        private static List<Node> FrontBackSplit(Node head, int length)
        {
            Node firstHead = head;
            Node secondHead = null;

            var firstCutOff = (int) Math.Ceiling(length/2.0);

            var currentNode = head;
            int i = 1;
            while (currentNode != null)
            {
                if (i == firstCutOff)
                {
                    secondHead = currentNode.Next;
                    currentNode.Next = null;
                    break;
                }
                currentNode = currentNode.Next;
                i++;
            }

            return new List<Node>(){firstHead, secondHead};
        }

        public void AppendList(SingleLinkedList linkedList)
        {
            _head = Append(_head, linkedList.Head);
        }

        public void AppendList(Node head)
        {
            _head = Append(_head, head);
        }

        private static Node Append(Node a, Node b)
        {
            if( a== null && b == null)
                throw new InvalidOperationException("Both A and B are null");
            if (b == null)
                return a;
            if (a == null)
                return b;

            Node curNode = a;
            while (curNode.Next != null)
            {
                curNode = curNode.Next;
            }

            curNode.Next = b;

            return a;

        }


        
        //Deletes all nodes with Data = data
        public void DeleteNode(int data)
        {
            DeleteNode(null, _head, data);
        }

        public void DeleteNode(Node node)
        {
            DeleteNode(null, _head, node);
        }

        private void DeleteNode(Node prevNode, Node currentNode, Node nodeToDelete)
        {
            if (currentNode != null)
            {
                if (currentNode == nodeToDelete)
                {
                    if (prevNode != null)
                        prevNode.Next = currentNode.Next;
                    else
                        _head = currentNode.Next;
                    nodeToDelete.Next = null;
                }
                else
                {
                    DeleteNode(currentNode, currentNode.Next, nodeToDelete);
                }
            }
        }

        private void DeleteNode(Node prevNode, Node currentNode, int data)
        {
            if (currentNode != null)
            {
                if (currentNode.Data == data)
                {
                    if (prevNode != null)
                        prevNode.Next = currentNode.Next;
                    else
                        _head = currentNode.Next;
                    DeleteNode(prevNode, currentNode.Next, data);
                }
                else
                    DeleteNode(currentNode, currentNode.Next, data);
            }
        }

        public void Sort()
        {
            SortLinkedList(_head);
        }

        private void SortLinkedList(Node node)
        {
            if (node != null && node.Next != null)
            {
                if (node.Data > node.Next.Data)
                {
                    DeleteNode(node);
                    SortedInsert(node);
                    SortLinkedList(_head);
                }
                else
                {
                    SortLinkedList(node.Next);
                }
            }
            //var nextNode = node.Next;
            //DeleteNode(node);
            //SortedInsert(node);
            //SortLinkedList(nextNode);
        }

        public void SortedInsert(int data)
        {
            SortedInsert(null, _head, new Node(data));
        }

        public void SortedInsert(Node newNode)
        {
            SortedInsert(null, _head, newNode);
        }

        private void SortedInsert(Node prevNode, Node currentNode, Node newNode)
        {
            if (currentNode == null)
            {
                prevNode.Next = newNode;
            }
            else if(newNode.Data < currentNode.Data)
            {
                if (prevNode != null)
                    prevNode.Next = newNode;
                else
                    _head = newNode;
                newNode.Next = currentNode;
            }
            else 
                SortedInsert(currentNode, currentNode.Next, newNode);
        }


        private static int Count(Node node)
        {
            if (node == null)
                return 0;

            return 1 + Count(node.Next);
        }

        

        public void InsertAt(int data, int index)
        {
            if (index == 0)
            {
                var prevHead = _head;
                _head = new Node(data);
                _head.Next = prevHead;
            }
            else if(index <= Length )
            {
                InsertAt(_head, 0, data, index);
            }
            else 
                throw new IndexOutOfRangeException(index.ToString());
        }

        private static void InsertAt(Node node, int current, int data, int index)
        {
            if (current == index - 1 || node.Next == null)
            {
                var newNode = new Node(data);
                var nextNode = node.Next;
                node.Next = newNode;
                newNode.Next = nextNode;
            }
            else
            {
                InsertAt(node.Next, current + 1, data, index);
            }
        }

        public int Pop()
        {
            var data = _head.Data;
            var nextNode = _head.Next;
            _head = nextNode;
            return data;
        }

        //counts number of times data occurs in list
        public int Count(int data)
        {
            return Count(_head, data);
        }

        private static int Count(Node node, int data)
        {
            if (node == null)
                return 0;

            return (node.Data == data ? 1 : 0) + Count(node.Next, data);
        }

        public int GetNthNodeData(int index)
        {
            return GetNthNodeData(_head, 0, index);
        }

        private static int GetNthNodeData(Node node, int current, int index)
        {
            if(node == null)
                throw new IndexOutOfRangeException("Index out of range");

            if (current == index)
                return node.Data;

            return GetNthNodeData(node.Next, current + 1, index);
        }

        public void PrintList()
        {
            var curNode = _head;
            while (curNode != null)
            {
                Console.Write(curNode.Data + " -> ");
                curNode = curNode.Next;
            }
            Console.Write("NULL");
            Console.WriteLine("");
        }
    }
}

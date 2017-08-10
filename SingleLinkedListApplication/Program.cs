using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedListApplication
{
    class Program
    {
        private static Node ReverseListHelper(Node curNode, Node prevNode)
        {
            if (curNode != null)
            {
                var nextNode = curNode.Next;
                curNode.Next = prevNode;
                return ReverseListHelper(nextNode, curNode);
            }
            return prevNode;

        }

        private static Node SortedInsertNodeHelper(Node prevNode, Node curNode, Node newNode)
        {
            if(curNode == null)
            {
                if (prevNode != null)
                    prevNode.Next = newNode;

                return newNode;
            }
            if(newNode.Data <= curNode.Data)
            {
                if(prevNode != null)
                    prevNode.Next = newNode;
                newNode.Next = curNode;
                return newNode;
            }
            return SortedInsertNodeHelper(curNode, curNode.Next, newNode);
        }

        private static Node SortedInsertNode(Node head, int data)
        {
            return SortedInsertNodeHelper(null, head, new Node(data));
        }

        private static void ReverseListTest()
        {
            var head = new Node(1, new Node(2, new Node(2, new Node(4, new Node(5, null)))));
            var node2 = head.Next;
            var linkedList = new SingleLinkedList(head);
            linkedList.PrintList();

            var newhead =ReverseListHelper(head, null);

            new SingleLinkedList(newhead).PrintList();


            Console.ReadLine();
        }

        private static void SortedInsertNode()
        {
            var head = new Node(1, new Node(2, new Node(2, new Node(4, new Node(5, null)))));
            var linkedList = new SingleLinkedList(head);
            linkedList.PrintList();

            SortedInsertNode(head, 3);
            linkedList.PrintList();
            SortedInsertNode(head, 6);
            linkedList.PrintList();

            linkedList.Head = SortedInsertNode(head, 0);
            linkedList.PrintList();


            Console.ReadLine();
        }

        private static void OriginalTest()
        {
            var head = new Node(1, new Node(2, new Node(2, new Node(4, new Node(5, null)))));
            var node2 = head.Next;
            var linkedList = new SingleLinkedList(head);
            linkedList.PrintList();
            Console.WriteLine("Count (2) : " + linkedList.Count(2));
            Console.WriteLine("GetNthNodeData(1): " + linkedList.GetNthNodeData(3));
            linkedList.SortedInsert(3);
            linkedList.PrintList();

            linkedList.SortedInsert(6);
            linkedList.PrintList();

            linkedList.SortedInsert(0);
            linkedList.PrintList();

            linkedList.InsertAt(6, 5);
            linkedList.PrintList();

            linkedList.InsertAt(4, 0);
            linkedList.PrintList();

            linkedList.InsertAt(20, 2);
            linkedList.PrintList();

            var newList = new Node(10, new Node(5, new Node(15, new Node(25))));
            Console.WriteLine("Append Linked List (10->5->15->25->NULL");
            linkedList.AppendList(newList);
            linkedList.PrintList();

            //Console.WriteLine("Delete 2 Node");
            //linkedList.DeleteNode(node2);
            //linkedList.PrintList();

            //Console.WriteLine("Insert 2 Node");
            //linkedList.SortedInsert(node2);
            //linkedList.PrintList();

            Console.WriteLine("Sort Linked List");
            linkedList.MergeSort();
            linkedList.PrintList();

            Console.WriteLine("Intersect List");
            var a = new SingleLinkedList(new Node(1, new Node(2, new Node(3, new Node(5, new Node(8))))));
            var b = new SingleLinkedList(new Node(2,
                new Node(4, new Node(5, new Node(6, new Node(7, new Node(8, new Node(9))))))));

            Console.Write("A: ");
            a.PrintList();
            Console.Write("B: ");
            b.PrintList();
            var intersectedList = SingleLinkedList.IntersectList(a, b);
            intersectedList.PrintList();

            Console.WriteLine("Deleting Duplicates");
            linkedList.RemoveDuplicates();
            linkedList.PrintList();

            Console.WriteLine("Popping list: " + linkedList.Pop());
            linkedList.PrintList();

            Console.WriteLine("Alt Split List");
            var lists = linkedList.AlternateSplit();
            lists[0].PrintList();
            lists[1].PrintList();

            //Console.WriteLine("Shuffle Merge lists");
            //var shuffleList = SingleLinkedList.ShuffleMerge(lists[0], lists[1]);
            //shuffleList.PrintList();

            //Console.WriteLine("Merge sorted lists");
            //lists[0].Sort();
            //lists[1].Sort();
            //lists[0].PrintList();
            //lists[1].PrintList();
            //var mergedList = SingleLinkedList.MergeSortedList(lists[0], lists[1]);
            //mergedList.PrintList();

            //Console.WriteLine("Delete 2");
            //linkedList.DeleteNode(2);
            //linkedList.PrintList();

            //Console.WriteLine("Split List");
            //var lists = linkedList.FrontBackSplit();
            //lists[0].PrintList();
            //lists[1].PrintList();

            //Console.WriteLine("Move head from list 1 to list 0-");
            //lists[0].MoveHeadFrom(lists[1]);
            //lists[0].PrintList();
            //lists[1].PrintList();

            Console.ReadLine();

        }


        static void Main(string[] args)
        {
            SortedInsertNode();
            //ReverseListTest();
            //OriginalTest();
        }
    }
}

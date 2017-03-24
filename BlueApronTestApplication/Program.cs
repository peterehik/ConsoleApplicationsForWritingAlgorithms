using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BlueApronTestApplication
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }

        public Node(int key, Node next)
        {
            Data = key;
            Next = next;
        }
    }

    public class Program
    {

        public static void Main()
        {
            var array = new[] {'F', 'T', 'W', 'F', 'T', 'T', 'W', 'F', 'W', 'T'};
            GroupFTWArray(array);

            var head = new Node(1, new Node(2, new Node(3, new Node(4, new Node(5, null)))));
            Console.WriteLine("Before");
            PrintList(head);
            var newHead = RecursiveReverseList(head, null);
            Console.WriteLine("After");
            PrintList(newHead);
            Console.ReadLine();
        }

        public static void GroupFTWArray(char[] array)
        {
            Console.WriteLine("Before grouping");
            Console.WriteLine(array);
            int fEnds = GroupArray(array, 'F', 0, array.Length);
            int tEnds = GroupArray(array, 'T', fEnds, array.Length);
            GroupArray(array, 'W', tEnds, array.Length);

            Console.WriteLine("After Grouping");
            Console.WriteLine(array);
        }

        public static void PrintList(Node head)
        {
            var curNode = head;
            while (curNode != null)
            {
                Console.Write(curNode.Data + " -> ");
                curNode = curNode.Next;
            }
            Console.Write("NULL");
            Console.WriteLine("");
        }



        public static Node ReverseLinkedList(Node head)
        {
            Node curNode = head;
            Node prevNode = null;
            Node nextNode = null;

            while (curNode != null)
            {
                nextNode = curNode.Next;
                curNode.Next = prevNode;
                prevNode = curNode;
                curNode = nextNode;
            }

            head = prevNode; //because curNode == null, prevNode  was original last node
            return head;
        }

        public static Node RecursiveReverseList(Node curNode, Node prevNode)
        {
            if (curNode == null)
                return prevNode;

            Node nextNode = curNode.Next;
            curNode.Next = prevNode;

            return RecursiveReverseList(nextNode, curNode);

        }


        public static void Swap(ref char a, ref char b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static int GroupArray(char[] array, char current, int low, int high)
        {
            int j = low;
            for (int i = low; i < high; i++)
            {
                if (array[i] == current)
                {
                    Swap(ref array[i], ref array[j]);
                    j++;
                }
            }
            return j;

        }
    }
}


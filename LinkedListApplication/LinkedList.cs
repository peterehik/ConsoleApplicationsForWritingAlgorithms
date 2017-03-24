using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListApplication
{
    public class LinkedList
    {
        private readonly Node _head;

        public LinkedList(int head)
        {
            _head = new Node(head);
        }

        public Node FindNode(int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode != null)
            {
                if (curNode.Key == key)
                    return curNode;
                curNode = curNode.Next;
            }

            return curNode;
        }

        public void AppendBefore(Node node, int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode != null)
            {
                if (curNode.Key == node.Key)
                {
                    newNode.Next = curNode;
                    if (curNode.Parent != null)
                    {
                        curNode.Parent.Next = newNode;
                        newNode.Parent = curNode.Parent;
                    }
                    curNode.Parent = newNode;
                    return;
                }

                curNode = curNode.Next;
            }

           
        }

        public void AppendAfter(Node node, int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode.Next != null)
            {
                if (curNode.Key == node.Key)
                {
                    newNode.Next = curNode.Next;
                    curNode.Next.Parent = newNode;
                    curNode.Next = newNode;
                    newNode.Parent = curNode;
                    return;
                }
                
                curNode = curNode.Next;
            }

            if (curNode.Key == node.Key)
            {
                curNode.Next = newNode;
                newNode.Parent = curNode;
            }

            
        }

        public void Append(int key)
        {
            Node curNode = _head;
            var newNode = new Node(key);

            while (curNode.Next != null)
            {
                curNode = curNode.Next;
            }

            curNode.Next = newNode;
            newNode.Parent = curNode;
        }

        public void DeleteNode(Node node)
        {
            if (node.Parent != null)
            {
                node.Parent.Next = node.Next;
                
            }
            if (node.Next != null)
            {
                node.Next.Parent = node.Parent;
            }
        }

        public override string ToString()
        {
            string s = _head.ToString();
            Node curNode = _head.Next;
            while (curNode != null)
            {
                s += "->" + curNode;
                curNode = curNode.Next;
            }
            return s;
        }
    }
}

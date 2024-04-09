using System.Runtime.InteropServices;

namespace XORLinkedList
{
    public class LinkedList<T>
    {
        public unsafe struct Node<T>
        {
            public T Value;
            internal Node<T>* Link;

            public unsafe Node<T>* Next(Node<T>* prev)
            {
                return XOR(Link, prev);
            }

            public unsafe Node<T>* Previous(Node<T>* next)
            {
                return XOR(Link, next);
            }

            public Node(T value)
            {
                Value = value;
                Link = null;
            }

            public unsafe Node<T>* XOR(Node<T>* x, Node<T>* y)
            {
                return (Node<T>*)((UIntPtr)(x) ^ (UIntPtr)(y));
            }
        }

        public unsafe Node<T>* head { get; private set; }
        public unsafe Node<T>* tail { get; private set; }

        public unsafe LinkedList()
        {
            this.head = null;
            this.tail = null;
        }

        public unsafe void AddAtHead(T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            newNode->Link = XOR(null, head);

            if (head != null)
            {
                head->Link = XOR(newNode, XOR(head->Link, null));
            }
            else
            {
                tail = newNode;
            }

            head = newNode;
        }

        public unsafe void AddAtTail(T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            newNode->Link = XOR(tail, null);

            if (tail != null)
            {
                tail->Link = XOR(XOR(tail->Link, null), newNode);
            }
            else
            {
                head = newNode;
            }

            tail = newNode;
        }

        public unsafe void DeleteFromHead()
        {
            if (head == null)
            {
                return;
            }

            Node<T>* next = XOR(head->Link, null);
            Marshal.FreeHGlobal((IntPtr)head);
            head = next;


            if (head != null)
            {
                next->Link = XOR(next->Link, head);
            }
            else
            {
                tail = null;
            }
        }

        public unsafe void DeleteFromTail()
        {
            if (tail == null)
            {
                return;
            }

            Node<T>* prev = XOR(tail->Link, null);
            Marshal.FreeHGlobal((IntPtr)tail);
            tail = prev;

            if (tail != null)
            {
                prev->Link = XOR(prev->Link, tail);
            }
            else
            {
                head = null;
            }
        }

        private unsafe Node<T>* XOR(Node<T>* x, Node<T>* y)
        {
            return (Node<T>*)((UIntPtr)(x) ^ (UIntPtr)(y));
        }
    }
}

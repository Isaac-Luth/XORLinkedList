using System.Runtime.InteropServices;

namespace XORLinkedList
{
    public class LinkedList<T>
    {
        public unsafe struct Node<T>
        {
            public T Value;
            public Node<T>* Link;

            public Node(T value)
            {
                Value = value;
                Link = null;
            }
        }

        public unsafe Node<T>* head { get; private set; }

        public unsafe LinkedList()
        {
            this.head = null;
        }

        private unsafe Node<T>* XOR(Node<T>* x, Node<T>* y)
        {
            return (Node<T>*)((UIntPtr)(x) ^ (UIntPtr)(y));
        }

        public unsafe string Traverse()
        {
            Node<T>* curr = head;
            Node<T>* prev = null;
            Node<T>* next;

            string result = "";

            while (curr != null)
            {
                result += curr->Value + "\n";

                next = XOR(prev, curr->Link);


                prev = curr;
                curr = next;
            }
            
            result += "nullptr";

            return result;
        }

        public unsafe void Add(T value)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            *newNode = new Node<T>(value);

            newNode->Link = XOR(head, null);

            if (head != null)
            {
                head->Link = XOR(newNode, XOR(head->Link, null));
            }

            head = newNode;
        }

        public unsafe void Delete()
        {
            if (head == null)
            {
                return;
            }

            Node<T>* next = head->Link;

            if (next == null)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                return;
            }
            
            
            Node<T>* nextNext = XOR(head, next->Link);
            next->Link = XOR(null, nextNext);

            Marshal.FreeHGlobal((IntPtr)head);
            head = next;
        }

    }
}

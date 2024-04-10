using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace XORLinkedList
{
    public class LinkedList<T> : ICollection<T>, IEnumerable<T>, ISerializable, IDeserializationCallback
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

            private unsafe Node<T>* XOR(Node<T>* x, Node<T>* y)
            {
                return (Node<T>*)((UIntPtr)(x) ^ (UIntPtr)(y));
            }
        }



        public unsafe Node<T>* first { get; private set; }

        private unsafe Node<T> _first
        {
            get
            {
                Node<T> node = new Node<T>();
                node.Link = first;
                return node;
            }
            set { *first = value; }
        }
        public unsafe Node<T>* last { get; private set; }

        public int Count => _Count();

        private unsafe int _Count()
        {
            Node<T>* current = first;
            Node<T>* prev = null;

            int count = 0;

            while (current != null)
            {
                count++;

                if (current == last)
                {
                    break;
                }
                
                current = current->Next(prev);
            }

            return count;
        }

        public bool IsReadOnly => false;

        public unsafe LinkedList()
        {
            this.first = null;
            this.last = null;
        }


        private unsafe Node<T>* GetNodeBefore(Node<T>* node)
        {
            Node<T>* current = first;
            Node<T>* prev = null;

            while (current != null)
            {
                Node<T>* next = current->Next(prev);
                if (current == node)
                {
                    return prev;
                }

                if (current == last)
                {
                    break;
                }

                prev = current;
                current = next;
            }

            return null;
        }

        public unsafe void AddAfter(Node<T>* prevNode, T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            AddAfter(prevNode, newNode);
        }
        public unsafe void AddAfter(Node<T>* prevNode, Node<T>* newNode)
        {
            if (prevNode == null || newNode == null)
            {
                return;
            }

            Node<T>* prevPrev = GetNodeBefore(prevNode);

            Node<T>* next = prevNode->Next(prevPrev);

            prevNode->Link = XOR(newNode, XOR(prevNode->Link, next));

            if (next != null)
            {
                next->Link = XOR(newNode, XOR(next->Link, prevNode));
            }
            else
            {
                last = newNode;
            }

            newNode->Link = XOR(prevNode, next);
        }

        public unsafe void AddBefore(Node<T>* nextNode, T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            AddBefore(nextNode, newNode);
        }
        public unsafe void AddBefore(Node<T>* nextNode, Node<T>* newNode)
        {
            if (nextNode == null || newNode == null)
            {
                return;
            }

            Node<T>* prev = GetNodeBefore(nextNode);

            if (prev != null)
            {
                AddAfter(prev, newNode);
            }
            else
            {
                AddFirst(newNode);
            }
        }


        public unsafe void AddFirst(T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            AddFirst(newNode);
        }
        public unsafe void AddFirst(Node<T>* newNode)
        {
            newNode->Link = XOR(null, first);

            if (first != null)
            {
                first->Link = XOR(newNode, XOR(first->Link, null));
            }
            else
            {
                last = newNode;
            }

            first = newNode;
        }

        public unsafe void AddLast(T data)
        {
            Node<T>* newNode = (Node<T>*)Marshal.AllocHGlobal(sizeof(Node<T>));
            newNode->Value = data;

            AddLast(newNode);
        }
        public unsafe void AddLast(Node<T>* newNode)
        {
            newNode->Link = XOR(last, null);

            if (last != null)
            {
                last->Link = XOR(XOR(last->Link, null), newNode);
            }
            else
            {
                first = newNode;
            }

            last = newNode;
        }

        public unsafe void Clear()
        {
            Node<T>* current = first;
            Node<T>* prev = null;
            while (current != null)
            {
                Node<T>* next = current->Next(prev);
                Marshal.FreeHGlobal((IntPtr)current);
                prev = current;
                current = next;
            }

            first = null;
            last = null;
        }

        public unsafe bool Contains(T item)
        {
            Node<T>* current = first;
            Node<T>* prev = null;
            Node<T>* next = null;

            while (current != null)
            {
                if (current->Value.Equals(item))
                {
                    return true;
                }

                if (current == last)
                {
                    break;
                }

                next = current->Next(prev);
                prev = current;
                current = next;
            }

            return false;
        }

        public unsafe void CopyTo(T[] array, int arrayIndex)
        {
            Node<T>* current = first;
            Node<T>* prev = null;
            Node<T>* next = null;

            int index = arrayIndex;

            while (current != null)
            {

                array[index] = current->Value;

                if (current == last)
                {
                    break;
                }

                index++;

                if (index >= array.Length)
                {
                    break;
                }

                next = current->Next(prev);
                prev = current;
                current = next;
            }
        }

        public unsafe Node<T>* Find(T item)
        {
            Node<T>* current = first;
            Node<T>* prev = null;
            Node<T>* next = null;

            while (current != null)
            {
                if (current->Value.Equals(item))
                {
                    return current;
                }

                if (current == last)
                {
                    break;
                }

                next = current->Next(prev);
                prev = current;
                current = next;
            }

            return null;
        }
        public unsafe Node<T>* FindLast(T item)
        {
            Node<T>* current = last;
            Node<T>* prev = null;
            Node<T>* next = null;

            while (current != null)
            {
                if (current->Value.Equals(item))
                {
                    return current;
                }

                if (current == first)
                {
                    break;
                }

                next = current->Previous(prev);
                prev = current;
                current = next;
            }

            return null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> enumerator = new LinkedListEnumerator<T>(_first);
            enumerator.Reset();
            return enumerator;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
        public void OnDeserialization(object? sender)
        {
            throw new NotImplementedException();
        }

        public unsafe bool Remove(Node<T>* node)
        {
            Node<T>* current = first;
            Node<T>* prev = null;

            while (current != null)
            {
                Node<T>* next = current->Next(prev);
                if (current == node)
                {
                    if (prev != null)
                    {
                        prev->Link = XOR(next, XOR(prev->Link, current));
                    }
                    else
                    {
                        first = next;
                    }

                    if (next != null)
                    {
                        next->Link = XOR(prev, XOR(next->Link, current));
                    }
                    else
                    {
                        last = prev;
                    }

                    Marshal.FreeHGlobal((IntPtr)current);
                    return true;
                }

                if (current == last)
                {
                    break;
                }

                prev = current;
                current = next;
            }

            return false;
        }
        public unsafe bool Remove(T item)
        {
            return Remove(Find(item));
        }
        
        public unsafe void RemoveFirst()
        {
            if (first == null)
            {
                return;
            }

            Node<T>* next = XOR(first->Link, null);
            Marshal.FreeHGlobal((IntPtr)first);
            first = next;


            if (first != null)
            {
                next->Link = XOR(next->Link, first);
            }
            else
            {
                last = null;
            }
        }
        public unsafe void RemoveLast()
        {
            if (last == null)
            {
                return;
            }

            Node<T>* prev = XOR(last->Link, null);
            Marshal.FreeHGlobal((IntPtr)last);
            last = prev;

            if (last != null)
            {
                prev->Link = XOR(prev->Link, last);
            }
            else
            {
                first = null;
            }
        }



        private unsafe Node<T>* XOR(Node<T>* x, Node<T>* y)
        {
            return (Node<T>*)((UIntPtr)(x) ^ (UIntPtr)(y));
        }


        void ICollection<T>.Add(T item)
        {
            AddFirst(item);
        }
    }

    internal unsafe class LinkedListEnumerator<T> : IEnumerator<T>
    {
        private LinkedList<T>.Node<T> _first;

        private LinkedList<T>.Node<T>* _current;
        private LinkedList<T>.Node<T>* _previous;
        private LinkedList<T>.Node<T>* _next;

        internal LinkedListEnumerator(LinkedList<T>.Node<T> startNode)
        {
            _current = null;
            _previous = null;
            _first = startNode;
        }

        public T Current
        {
            get
            {
                if (_current == null)
                {
                    throw new InvalidOperationException();
                }
                return _current->Value;
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_next != null)
            {
                _current = _next;
                _next = _current->Next(_previous);
                _previous = _current;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _current = null;
            _previous = null;
            _next = _first.Link;
        }


        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}

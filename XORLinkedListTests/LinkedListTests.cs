using XLinkedList = XORLinkedList.LinkedList<int>;
using Node = XORLinkedList.LinkedList<int>.Node<int>;

namespace XORLinkedListTests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public unsafe void AddFirst_AddsToEmptyList_HeadAndTailPointToSameNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);

            Assert.IsNotNull((IntPtr)list.first);
            Assert.AreEqual((IntPtr)list.first, (IntPtr)list.last);
            Assert.AreEqual(5, list.first->Value);
        }

        [TestMethod]
        public unsafe void AddLast_AddsToEmptyList_HeadAndTailPointToSameNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddLast(5);

            Assert.IsNotNull((IntPtr)list.first);
            Assert.AreEqual((IntPtr)list.first, (IntPtr)list.last);
            Assert.AreEqual(5, list.first->Value);
        }

        [TestMethod]
        public unsafe void AddFirst_AddsMultipleNodes_HeadPointsToNewNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            Node* head = list.first;
            Node* next = head->Next(null);
            Node* nextNext = next->Next(head);

            Assert.AreEqual(15, head->Value);
            Assert.AreEqual(10, next->Value);
            Assert.AreEqual(5, nextNext->Value);
        }

        [TestMethod]
        public unsafe void AddLast_AddsMultipleNodes_TailPointsToNewNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddLast(5);
            list.AddLast(10);
            list.AddLast(15);

            Node* tail = list.last;
            Node* next = tail->Previous(null);
            Node* nextNext = next->Previous(tail);

            Assert.AreEqual(15, tail->Value);
            Assert.AreEqual(10, next->Value);
            Assert.AreEqual(5, nextNext->Value);
        }

        [TestMethod]
        public unsafe void DeleteFromHead_RemovesNode_HeadAndTailAreNull()
        {
            XLinkedList list = new XLinkedList();

            list.AddLast(5);
            list.RemoveFirst();

            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.first);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.last);
        }

        [TestMethod]
        public unsafe void DeleteFromTail_RemovesNode_HeadAndTailAreNull()
        {
            XLinkedList list = new XLinkedList();

            list.AddLast(5);
            list.RemoveLast();

            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.first);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.last);
        }

        [TestMethod]
        public unsafe void AddAfter_AddsNode_HeadAndTailNotChanged()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddAfter(list.first, 15);

            Assert.AreEqual(10, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void DeleteFromHead_RemovesNode_HeadPointsToNextNode()
        {
            XLinkedList list = new XLinkedList();


            list.AddFirst(5);
            list.AddFirst(10);
            list.RemoveFirst();

            Assert.AreEqual(5, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void DeleteFromTail_RemovesNode_TailPointsToPreviousNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.RemoveLast();

            Assert.AreEqual(10, list.last->Value);
            Assert.AreEqual(10, list.first->Value);
        }

        [TestMethod]
        public unsafe void Clear_RemovesAllNodes_HeadAndTailAreNull()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.Clear();

            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.first);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.last);
        }

        [TestMethod]
        public unsafe void Contains_ReturnsTrueIfItemExists()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            Assert.IsTrue(list.Contains(5));
        }

        [TestMethod]
        public unsafe void Contains_ReturnsFalseIfItemDoesNotExist()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            Assert.IsFalse(list.Contains(15));
        }

        [TestMethod]
        public unsafe void Count_ReturnsNumberOfNodes()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public unsafe void GetEnumerator_ReturnsEnumerator()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            IEnumerator<int> enumerator = list.GetEnumerator();

            Assert.IsNotNull(enumerator);
        }

        [TestMethod]
        public unsafe void GetEnumerator_ReturnsEnumeratorWithCorrectValues()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            IEnumerator<int> enumerator = list.GetEnumerator();

            enumerator.MoveNext();
            Assert.AreEqual(10, enumerator.Current);

            enumerator.MoveNext();
            Assert.AreEqual(5, enumerator.Current);
        }

        [TestMethod]
        public unsafe void GetEnumerator_ReturnsEnumeratorWithCorrectValuesAfterReset()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            IEnumerator<int> enumerator = list.GetEnumerator();

            enumerator.MoveNext();
            enumerator.MoveNext();
            enumerator.Reset();

            enumerator.MoveNext();
            Assert.AreEqual(10, enumerator.Current);
        }

        [TestMethod]
        public unsafe void GetEnumerator_ForEach()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);


            int num = 10;
            foreach (var node in list)
            {
                Assert.AreEqual(num, node);
                num -= 5;
            }
        }

        [TestMethod]
        public unsafe void GetEnumerator_Empty()
        {
            XLinkedList list = new XLinkedList();

            IEnumerator<int> enumerator = list.GetEnumerator();

            Assert.IsFalse(enumerator.MoveNext());

            Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        }


    }
}
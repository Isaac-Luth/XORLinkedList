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
        public void Contains_ReturnsTrueIfItemExists()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            Assert.IsTrue(list.Contains(5));
        }

        [TestMethod]
        public void Contains_ReturnsFalseIfItemDoesNotExist()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            Assert.IsFalse(list.Contains(20));
        }

        [TestMethod]
        public void Count_ReturnsNumberOfNodes()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void GetEnumerator_ReturnsEnumerator()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            IEnumerator<int> enumerator = list.GetEnumerator();

            Assert.IsNotNull(enumerator);
        }

        [TestMethod]
        public void GetEnumerator_ReturnsEnumeratorWithCorrectValues()
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
        public void GetEnumerator_ReturnsEnumeratorWithCorrectValuesAfterReset()
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
        public void GetEnumerator_ForEach()
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
        public void GetEnumerator_Empty()
        {
            XLinkedList list = new XLinkedList();

            IEnumerator<int> enumerator = list.GetEnumerator();

            Assert.IsFalse(enumerator.MoveNext());

            Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        }

        [TestMethod]
        public void GetEnumerator_EmptyAfterReset()
        {
            XLinkedList list = new XLinkedList();

            IEnumerator<int> enumerator = list.GetEnumerator();

            enumerator.Reset();

            Assert.IsFalse(enumerator.MoveNext());

            Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        }

        [TestMethod]
        public unsafe void Remove_RemovesNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);

            list.Remove(5);

            Assert.AreEqual(10, list.first->Value);
            Assert.AreEqual(10, list.last->Value);
        }

        [TestMethod]
        public unsafe void Remove_RemovesNodeFromMiddle()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            list.Remove(10);

            Assert.AreEqual(15, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void Remove_RemovesNodeFromEnd()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            list.Remove(5);

            Assert.AreEqual(15, list.first->Value);
            Assert.AreEqual(10, list.last->Value);
        }

        [TestMethod]
        public unsafe void Remove_RemovesFail()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            list.Remove(20);

            Assert.AreEqual(15, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void Remove_RemovesNodeUsingNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            list.Remove(list.first->Next(null));

            Assert.AreEqual(15, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public void CopyTo_CopiesValuesToArray()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            int[] array = new int[3];
            list.CopyTo(array, 0);

            Assert.AreEqual(15, array[0]);
            Assert.AreEqual(10, array[1]);
            Assert.AreEqual(5, array[2]);
        }

        [TestMethod]
        public void CopyTo_CopiesValuesToArrayWithOffset()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            int[] array = new int[4];
            list.CopyTo(array, 1);

            Assert.AreEqual(0, array[0]);
            Assert.AreEqual(15, array[1]);
            Assert.AreEqual(10, array[2]);
            Assert.AreEqual(5, array[3]);
        }

        [TestMethod]
        public void CopyTo_CopiesOutOfIndex()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddFirst(15);

            int[] array = new int[2];
            list.CopyTo(array, 0);

            Assert.AreEqual(15, array[0]);
            Assert.AreEqual(10, array[1]);
        }


        [TestMethod]
        public unsafe void AddBefore_AddsNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddBefore(list.first, 15);

            Assert.AreEqual(15, list.first->Value);
            Assert.AreEqual(10, list.first->Next(null)->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void AddAfter_AddsNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddFirst(5);
            list.AddFirst(10);
            list.AddAfter(list.first, 15);

            Assert.AreEqual(10, list.first->Value);
            Assert.AreEqual(15, list.first->Next(null)->Value);
            Assert.AreEqual(5, list.last->Value);
        }

        [TestMethod]
        public unsafe void ICollection_Add_AddsNode()
        {
            XLinkedList list = new XLinkedList();

            ((ICollection<int>)list).Add(5);

            Assert.AreEqual(5, list.first->Value);
            Assert.AreEqual(5, list.last->Value);
        }

    }
}
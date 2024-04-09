using XLinkedList = XORLinkedList.LinkedList<int>;
using Node = XORLinkedList.LinkedList<int>.Node<int>;

namespace XORLinkedListTests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public unsafe void AddAtHead_AddsToEmptyList_HeadAndTailPointToSameNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtHead(5);

            Assert.IsNotNull((IntPtr)list.head);
            Assert.AreEqual((IntPtr)list.head, (IntPtr)list.tail);
            Assert.AreEqual(5, list.head->Value);
        }

        [TestMethod]
        public unsafe void AddAtTail_AddsToEmptyList_HeadAndTailPointToSameNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtTail(5);

            Assert.IsNotNull((IntPtr)list.head);
            Assert.AreEqual((IntPtr)list.head, (IntPtr)list.tail);
            Assert.AreEqual(5, list.head->Value);
        }

        [TestMethod]
        public unsafe void AddAtHead_AddsMultipleNodes_HeadPointsToNewNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtHead(5);
            list.AddAtHead(10);
            list.AddAtHead(15);

            Node* head = list.head;
            Node* next = head->Next(null);
            Node* nextNext = next->Next(head);

            Assert.AreEqual(15, head->Value);
            Assert.AreEqual(10, next->Value);
            Assert.AreEqual(5, nextNext->Value);
        }

        [TestMethod]
        public unsafe void AddAtTail_AddsMultipleNodes_TailPointsToNewNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtTail(5);
            list.AddAtTail(10);
            list.AddAtTail(15);

            Node* tail = list.tail;
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

            list.AddAtTail(5);
            list.DeleteFromHead();

            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.head);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.tail);
        }

        [TestMethod]
        public unsafe void DeleteFromTail_RemovesNode_HeadAndTailAreNull()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtTail(5);
            list.DeleteFromTail();

            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.head);
            Assert.AreEqual(IntPtr.Zero, (IntPtr)list.tail);
        }

        [TestMethod]
        public unsafe void DeleteFromHead_RemovesNode_HeadPointsToNextNode()
        {
            XLinkedList list = new XLinkedList();


            list.AddAtHead(5);
            list.AddAtHead(10);
            list.DeleteFromHead();

            Assert.AreEqual(5, list.head->Value);
            Assert.AreEqual(5, list.tail->Value);
        }

        [TestMethod]
        public unsafe void DeleteFromTail_RemovesNode_TailPointsToPreviousNode()
        {
            XLinkedList list = new XLinkedList();

            list.AddAtTail(5);
            list.AddAtTail(10);
            list.DeleteFromTail();

            Assert.AreEqual(5, list.tail->Value);
            Assert.AreEqual(5, list.head->Value);
        }
    }
}
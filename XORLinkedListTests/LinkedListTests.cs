using XLinkedList = XORLinkedList.LinkedList<int>;

namespace XORLinkedListTests
{
    [TestClass]
    public class LinkedListTests
    {
        [TestMethod]
        public unsafe void AddSingleNode()
        {
            // Arrange
            int value = 5;
            XLinkedList linkedList = new XLinkedList();

            // Act
            linkedList.Add(value);
            var head = linkedList.head;

            // Assert
            Assert.IsTrue(head != null);
            Assert.AreEqual(value, head->Value);
            Assert.IsTrue(null == head->Link);
        }

        [TestMethod]
        public unsafe void TraverseLinkedList()
        {
            // Arrange
            XLinkedList linkedList = new XLinkedList();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);

            // Act
            string result = linkedList.Traverse();

            // Assert
            Assert.AreEqual("15\n10\n5\nnullptr", result);
        }

        [TestMethod]
        public unsafe void DeleteSingleNode()
        {
            // Arrange
            XLinkedList linkedList = new XLinkedList();
            linkedList.Add(5);

            // Act
            linkedList.Delete();

            // Assert
            Assert.IsTrue(linkedList.head == null);
        }

        [TestMethod]
        public unsafe void DeleteFromEmptyList()
        {
            // Arrange
            XLinkedList linkedList = new XLinkedList();

            // Act
            linkedList.Delete();

            // Assert
            Assert.IsTrue(linkedList.head == null);
        }

        [TestMethod]
        public unsafe void DeleteFromMultipleNodeList()
        {
            // Arrange
            XLinkedList linkedList = new XLinkedList();
            linkedList.Add(5);
            linkedList.Add(10);
            linkedList.Add(15);

            // Act
            linkedList.Delete();

            // Assert
            Assert.AreEqual("10\n5\nnullptr", linkedList.Traverse());
        }
    }
}
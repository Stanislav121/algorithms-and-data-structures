using System;
using DataStructures;
using NUnit.Framework;

namespace BinaryTreeTests
{
    [TestFixture]
    public class MyStackTests
    {
        [Test]
        public void PutAndGetTest()
        {
            var stack1 = new StackBasedOnLinks<int>();
            PutAndGetTest(stack1);

            var stack2 = new StackBasedOnArray<int>();
            PutAndGetTest(stack2);
        }

        private void PutAndGetTest(IStack<int> stack)
        {
            var integers = new Int32[15] { 3, 3, 8, -1000, 41, 0, 17, 5000, 34, -5, 34, 567, 0, 0, 32 };
            foreach (var i in integers)
            {
                stack.Push(i);
            }

            for (int i = integers.Length - 1; i >= 0; i--)
            {
                var integer = stack.Pop();
                if (integer != integers[i])
                    Assert.IsTrue(false);
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void PeekTest()
        {
            var stack1 = new StackBasedOnLinks<int>();
            PeekTest(stack1);

            var stack2 = new StackBasedOnArray<int>();
            PeekTest(stack2);
        }

        private void PeekTest(IStack<int> stack)
        {
            var integers = new Int32[7] { 3, 3, 8, -1000, 41, 0, 17 };
            foreach (var i in integers)
            {
                stack.Push(i);
            }

            Assert.AreEqual(stack.Peek(), 17);
        }
    }
}

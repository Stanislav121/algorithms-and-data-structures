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
            var stack = new MyStack<int>();
            var integers = new Int32[7]{3, 3, 8, -1000, 41, 0, 17};
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
            var stack = new MyStack<int>();
            var integers = new Int32[7] { 3, 3, 8, -1000, 41, 0, 17 };
            foreach (var i in integers)
            {
                stack.Push(i);
            }
            
            Assert.AreEqual(stack.Peek(), 17);
        }
    }
}

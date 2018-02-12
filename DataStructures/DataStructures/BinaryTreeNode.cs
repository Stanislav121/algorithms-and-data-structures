using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinaryTreeNode<T>
    {
        public T Value;

        public BinaryTreeNode<T> LeftNode { get; private set; }
        public BinaryTreeNode<T> RightNode{ get; private set; }

        public BinaryTreeNode<T> HeadNode { get; private set; }

        public BinaryTreeNode(T value)
        {
            Value = value;
        }

        public void AddLeftNode(T value)
        {
            LeftNode = new BinaryTreeNode<T>(value);
            LeftNode.HeadNode = this;
        }

        public void AddLeftNode(BinaryTreeNode<T> node)
        {
            LeftNode = node;
            LeftNode.HeadNode = this;
        }

        public void AddRightNode(T value)
        {
            RightNode = new BinaryTreeNode<T>(value);
            RightNode.HeadNode = this;
        }

        public void AddRightNode(BinaryTreeNode<T> node)
        {
            RightNode = node;
            RightNode.HeadNode = this;
        }

        public void AddHeadtNode(BinaryTreeNode<T> headNode)
        {
            HeadNode = headNode;
        }

        public void DiscardHeadtNode()
        {
            HeadNode = null;
        }
    }
}

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

        public void SetLeftNode(T value)
        {
            LeftNode = new BinaryTreeNode<T>(value) {HeadNode = this};
        }

        public void SetLeftNode(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                LeftNode = null;
                return;
            }
            LeftNode = node;
            LeftNode.HeadNode = this;
        }

        public void SetRightNode(T value)
        {
            RightNode = new BinaryTreeNode<T>(value) {HeadNode = this};
        }

        public void SetRightNode(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                RightNode = null;
                return;
            }
            RightNode = node;
            RightNode.HeadNode = this;
        }

        public void SetHeadtNode(BinaryTreeNode<T> headNode)
        {
            HeadNode = headNode;
        }

        public void DiscardHeadtNode()
        {
            HeadNode = null;
        }
    }
}

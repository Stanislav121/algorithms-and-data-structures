namespace DataStructures
{
    internal class TwoWayNode<T>
    {
        public TwoWayNode<T> Previous;

        public T Value;

        public TwoWayNode<T> Next;

        public TwoWayNode(T value)
        {
            Value = value;
        }
    }
}

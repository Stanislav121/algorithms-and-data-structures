namespace DataStructures
{
    public class MyStack<T>
    {
        private TwoWayNode<T> _currentValue;

        public void Push(T value)
        {
            var node = new TwoWayNode<T>(value);
            if (_currentValue == null)
            {
                _currentValue = node;
            }
            else
            {
                _currentValue.Next = node;
                var previous = _currentValue;
                _currentValue = _currentValue.Next;
                _currentValue.Previous = previous;
            }
        }

        public T Pop()
        {
            if (_currentValue == null)
            {
                return default(T);
            }
            else
            {
                var valueToReturn = _currentValue.Value;
                var previous = _currentValue.Previous;
                if (previous == null)
                {
                    return valueToReturn;
                }
                _currentValue = previous;
                _currentValue.Next = null;
                return valueToReturn;
            }
        }

        public T Peek()
        {
            return _currentValue.Value;
        }
    }
}

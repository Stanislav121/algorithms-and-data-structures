using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class StackBasedOnArray<T> : IStack<T>
    {
        private T[] _array;
        private long _currentEmptyPosition;

        public StackBasedOnArray()
        {
            _array = new T[5];
            _currentEmptyPosition = 0;
        }

        public void Push(T value)
        {
            if (_currentEmptyPosition >= _array.Length)
            {
                var newArray = new T[_array.Length * 2];
                _array.CopyTo(newArray, 0);
                _array = newArray;
                _array[_currentEmptyPosition] = value;
                _currentEmptyPosition++;
            }
            else
            {
                _array[_currentEmptyPosition] = value;
                _currentEmptyPosition++;
            }
        }

        public T Pop()
        {
            var value = _array[_currentEmptyPosition - 1];
            _array[_currentEmptyPosition - 1] = default(T);
            _currentEmptyPosition--;
            return value;
        }

        public T Peek()
        {
            return _array[_currentEmptyPosition - 1];
        }
    }
}

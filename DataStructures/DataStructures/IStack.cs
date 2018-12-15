using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public interface IStack<T>
    {
        void Push(T value);

        T Pop();

        T Peek();
    }
}

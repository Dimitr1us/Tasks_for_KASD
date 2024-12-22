using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIteratorLib
{
    internal interface iMyIterator<T>
    {
        bool HasNext();
        T Next();
        bool HasPrevious();
        T Previous();
        int NextIndex();
        int PreviousIndex();
        void Remove();
        void Set(T element);
        void Add(T element);
    }

    internal interface iMyIterator2<T>
    {
        bool HasNext();
        T Next();
        void Remove();
    }
}

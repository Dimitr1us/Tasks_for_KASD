using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIteratorLib
{

    internal interface iMyIter<T>
    {
        bool HasNext();
        T Next();
        void Remove();
    }
    internal interface iMyIterator<T>:iMyIter<T>
    {
        bool HasPrevious();
        T Previous();
        int NextIndex();
        int PreviousIndex();
        void Set(T element);
        void Add(T element);
    }


}

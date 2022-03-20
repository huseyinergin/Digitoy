using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Digitoy
{
    internal  class SortByNumber : IComparer<Tas>
    {

        public  int Compare(Tas x, Tas y)
        {
            return x.Sayi.CompareTo(y.Sayi);
        }
    }
}

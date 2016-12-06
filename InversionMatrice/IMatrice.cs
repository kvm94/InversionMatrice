using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    interface IMatrice<T>
    {
        int nbrCol { get; }
        int nbrLn { get; }
        double[,] values { get; }

        bool isSquare();
        void print();
        T Product(T m);    
    }
}

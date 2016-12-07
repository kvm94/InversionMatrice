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

        void InitData(double[,] data);
        bool isSquare();
        void print();
        Matrice Product(T m);
        void swapLn(int ln1, int ln2);
    }
}

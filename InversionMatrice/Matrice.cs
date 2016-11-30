using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class Matrice
    {
        #region Attributs

        public int nbrCol { get; }
        public int nbrLn { get; }
        private double[,] values;

        #endregion

        #region Constructeurs

        public Matrice():this(0, 0)
        {
            values = null;
        }

        public Matrice(int ln, int col)
        {
            nbrCol = col;
            nbrLn = ln;
            values = new double[ln, col];
        }

        #endregion

        #region Méthodes
       
        public bool isSquare()
        {
            return nbrCol == nbrLn;
        }

        public void print()
        {
            for (int i = 0; i < nbrLn; i++)
            {
                Console.Write('(');
                for (int j = 0; j < nbrCol; j++)
                {
   
                    Console.Write(String.Format("{0,6:##.00} ", values[i, j]));
                }
                Console.WriteLine(")");
            }
        }

        public double this[int iLn, int iCol] {
            get { return values[iLn, iCol]; }
            set { values[iLn, iCol] = value; }
        }
        #endregion

    }
}

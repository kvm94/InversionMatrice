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

        public Matrice() : this(0, 0)
        {
            values = null;
        }

        public Matrice(int ln, int col)
        {
            nbrCol = col;
            nbrLn = ln;
            values = new double[ln, col];
        }

        public Matrice(double[,] data)
        {
            values = data;
            nbrCol = data.GetLength(1);
            nbrLn = data.GetLength(0);
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

        public Matrice Product(Matrice m)
        {
            if(nbrLn == m.nbrCol)
            {
                Matrice temp = new Matrice(nbrLn, m.nbrCol);

                for (int i = 0; i < nbrLn; i++)
                {
                    for (int j = 0; j < m.nbrCol; j++)
                    {
                        for (int k = 0; k < m.nbrCol; k++)
                        {
                            temp[i, j] += this[i,k]*m[k,j];
                        }
                    }
                }

                return temp;
            }
            else
            {
                return null;
            }
           
        }
        #endregion

    }
}

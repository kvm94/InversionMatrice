﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class SousMatrice:IMatrice<SousMatrice>
    {
        #region Attributs

        public int nbrCol { get; }
        public int nbrLn { get; }
        public double[,] values { get; }

        #endregion

        #region Constructeurs

        public SousMatrice() : this(0, 0)
        {
            values = null;
        }

        public SousMatrice(int ln, int col)
        {
            nbrCol = col;
            nbrLn = ln;
            values = new double[ln, col];
        }

        public SousMatrice(double[,] data)
        {
            values = data;
            nbrCol = data.GetLength(1);
            nbrLn = data.GetLength(0);
        }

        #endregion

        #region Méthodes

        //Vérifie si la matrice est une matrice carré.
        public bool isSquare()
        {
            return nbrCol == nbrLn;
        }

        //Affiche la matrice.
        public void print()
        {
            for (int i = 0; i < nbrLn; i++)
            {
                Console.Write('(');
                for (int j = 0; j < nbrCol; j++)
                {

                    Console.Write(String.Format("{0,6:#0.00} ", values[i, j]));
                }
                Console.WriteLine(")");
            }
        }

        //Indexeur pour accéder à une valeur de la matrice.
        public double this[int iLn, int iCol]
        {
            get { return values[iLn, iCol]; }
            set { values[iLn, iCol] = value; }
        }

        //Calcul le produit de deux matrices.
        public SousMatrice Product(SousMatrice m)
        {
            //Vérifie si l'on peut multiplier les matrice entre elles.
            if (nbrLn == m.nbrCol)
            {
                SousMatrice temp = new SousMatrice(nbrLn, m.nbrCol);

                for (int i = 0; i < nbrLn; i++)
                {
                    for (int j = 0; j < m.nbrCol; j++)
                    {
                        for (int k = 0; k < m.nbrCol; k++)
                        {
                            //Calcul le produit matricielle de [i,j]
                            temp[i, j] += this[i, k] * m[k, j];
                        }
                    }
                }

                return temp;
            }
            else
            {
                throw new Exception("Impossible de multiplier ces matrice");
            }

        }

        //Echange deux colonnes de place.
        public void swapCol(int col1, int col2)
        {
            double tmp;

            for (int i = 0; i < nbrLn; i++)
            {
                tmp = values[i, col1];
                values[i, col1] = values[i, col2];
                values[i, col2] = tmp;
            }
        }
        #endregion
    }
}
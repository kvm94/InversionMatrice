using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class Matrice:IMatrice<Matrice>
    {
        #region Attributs

        public int nbrCol { get; }
        public int nbrLn { get; }
        public double[,] values { get; }

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
        public double this[int iLn, int iCol] {
            get { return values[iLn, iCol]; }
            set { values[iLn, iCol] = value; }
        }

        //Vérifie si une matrice est inversible.
        public bool isInversible()
        {
            double determinant = Determinant();

            if (determinant == 0)
                return false;
            else
                return true;
        }

        //Calcul le produit de deux matrices.
        public Matrice Product(Matrice m)
        {
            //Vérifie si l'on peut multiplier les matrice entre elles.
            if(nbrLn == m.nbrCol)
            {
                Matrice temp = new Matrice(nbrLn, m.nbrCol);

                for (int i = 0; i < nbrLn; i++)
                {
                    for (int j = 0; j < m.nbrCol; j++)
                    {
                        for (int k = 0; k < m.nbrCol; k++)
                        {
                            //Calcul le produit matricielle de [i,j]
                            temp[i, j] += this[i,k]*m[k,j];
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

        //Echange deux lignes de place.
        public void swapLn(int ln1, int ln2)
        {
            double tmp;

            for (int i = 0; i < nbrCol; i++)
            {
                tmp = values[ln1, i];
                values[ln1, i] = values[ln2, i];
                values[ln2, i] = tmp;
            }
        }

        //Calcul le déterminant de la matrice.
        public double Determinant() // Défectueuse. A refaire après LU
        {
            int i, j, k;
            double det = 0;
            for (i = 0; i < nbrCol - 1; i++)
            {
                for (j = i + 1; j < nbrCol+1; j++)
                {
                    det = values[j, i] / values[i, i];
                    for (k = i; k < nbrCol; k++)
                        values[j, k] = values[j, k] - det * values[i, k];
                }
            }
            det = 1;
            for (i = 0; i < nbrCol; i++)
                det = det * values[i, i];
            return det;
        }

        //Triangularise la matrice par la méthode de Gauss.
        public SousMatrice Gauss()
        {
            SousMatrice temp = new SousMatrice(values);
            double m;
            double pivot;

            //Pour chaque étape. (k-1 étapes)
            for (int k = 0; k < nbrLn -1; k++)
            {
                Console.WriteLine("Etape : " + k);
                Console.WriteLine("-------------");

                pivot = temp[k, k];

                Console.WriteLine(String.Format("Pivot = {0,6:#0.00} ", pivot));

                //Si le pivot vaut 0 il faut permuter les lignes.
                if (pivot == 0)
                    swapLn(k, k + 1);

                //Pour chaque ligne de la matrice. 
                //Calcul du coefficient de l'étape k.
                for (int i = k+1; i < nbrLn; i++)
                {
                    m = temp[i, k] / pivot;
                    Console.WriteLine(String.Format("m" +(i+1)+(k+1)+ " = {0,6:#0.00}", m));

                    //Met les 0.
                    for (int j = k; j < nbrLn; j++)
                    {
                        temp[i, j] -= m * temp[k, j];
                    }
                }

                //Affiche le résultat à la fin de l'étape.
                print();
                Console.WriteLine();

            }



            return temp;
        }

        #endregion

    }
}

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
            //Besoin pour ne pas copier la référence.
            InitData(data);
            nbrCol = data.GetLength(1);
            nbrLn = data.GetLength(0);
        }

        #endregion

        #region Méthodes

        #region Private

        //Initialise les valeurs de la matrice.
        private void InitData(double[,] data)
        {
            values = new double[data.GetLength(0), data.GetLength(1)];
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    values[i, j] = data[i, j];
                }
            }
        }

        //Vérifie si une matrice est inversible.
        private bool IsInversible()
        {
            double determinant = Determinant();

            if (determinant == 0)
                return false;
            else
                return true;
        }

        #endregion

        #region Public

        //Indexeur pour accéder à une valeur de la matrice.
        public double this[int iLn, int iCol]
        {
            get { return values[iLn, iCol]; }
            set { values[iLn, iCol] = value; }
        }

        //Vérifie si la matrice est une matrice carré.
        public bool IsSquare()
        {
            return nbrCol == nbrLn;
        }

        //Affiche la matrice.
        public void Print()
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
        public void SwapLn(int ln1, int ln2)
        {
            double tmp;

            for (int i = 0; i < nbrCol; i++)
            {
                tmp = values[ln1, i];
                values[ln1, i] = values[ln2, i];
                values[ln2, i] = tmp;
            }
        }

        //Triangularise la matrice par la méthode de Gauss.
        public Matrice Gauss(out int[,] swaps, out double[,] m)
        {
            Matrice temp = new Matrice(values);
            double pivot;
            
            //Initialise la sortie.
            swaps = new int[nbrLn-1,2];
            m = new double[nbrLn, nbrCol];

            //Pour chaque étape. (k-1 étapes)
            for (int k = 0; k < nbrLn -1; k++)
            {
                Console.WriteLine("Etape : " + (k+1));
                Console.WriteLine("---------");

                pivot = temp[k, k];

                //Si le pivot vaut 0 il faut permuter les lignes.
                if (pivot == 0)
                {
                    //Récupére les numéros des lignes qui permutent.
                    swaps[k, 0] = k;
                    swaps[k, 1] = k + 1;

                    //Permute les lignes et recalcul le pivot.
                    temp.SwapLn(k, k + 1);
                    pivot = temp[k, k];
                }
                else
                {
                    //Indique que les lignes n'ont pas été permutés.
                    swaps[k, 0] = -1;
                    swaps[k, 1] = -1;
                }
                Console.WriteLine(String.Format("Pivot = {0,6:#0.00} ", pivot));


                //Pour chaque ligne de la matrice. 
                //Calcul du coefficient de l'étape k.
                for (int i = k+1; i < nbrLn; i++)
                {
                    m[i, k] = temp[i, k] / pivot;
                    Console.WriteLine(String.Format("m" +(i+1)+(k+1)+ " = {0,6:#0.00}", m[i,k]));

                    //Met les 0.
                    for (int j = k; j < nbrLn; j++)
                    {
                        temp[i, j] -= m[i,k] * temp[k, j];
                    }
                }

                //Affiche le résultat à la fin de l'étape.
                Print();

                //Affiche les lignes permutés pour chaque étapes.
                if (swaps[k, 0] != -1)
                    Console.WriteLine("Lignes permutés : " + (swaps[k, 0] + 1) + " <-> " + (swaps[k, 1] + 1));

                Console.WriteLine();
            }
            return temp;
        }

        //Initialise la sous-matrice L
        public Matrice InitL(double[,] m)
        {
            Matrice L = new Matrice(m);

            for (int i = 0; i < nbrLn; i++)
            {
                L[i, i] = 1;
            }
            return L;
        }

        //Calcul le déterminant de la matrice.
        public double Determinant() // Défectueuse. A refaire après LU
        {
            return 0;
        }


        #endregion

        #endregion

    }
}

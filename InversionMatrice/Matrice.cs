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
        public static int Precision { get; set; } = 2;

        private int nbrCol;
        private int nbrLn;
        private double ?determinant;
        private double[,] values;
        private Matrice L;
        private Matrice U;
        private int[,] swaps; //Historique des permutations. (-1 si pas de permutation)

        #endregion

        #region Constructeurs

        public Matrice() : this(0, 0)
        {
            values = null;
            determinant = null;
            L = null;
            U = null;
        }

        public Matrice(int ln, int col)
        {
            nbrCol = col;
            nbrLn = ln;
            values = new double[ln, col];
            determinant = null;
            L = null;
            U = null;
        }

        public Matrice(double[,] data)
        {
            //Besoin pour ne pas copier la référence.
            InitData(data);
            nbrCol = data.GetLength(1);
            nbrLn = data.GetLength(0);
            determinant = null;
            L = null;
            U = null;
        }

        public Matrice(Matrice m)
        {
            nbrCol = m.NbrCol;
            nbrLn = m.NbrLn;
            values = new double[nbrLn, nbrCol];

            //Besoin pour ne pas copier la référence.
            for (int i = 0; i < nbrLn; i++)
            {
                for (int j = 0; j < nbrCol; j++)
                {
                    values[i, j] = m[i, j];
                }
            }


            determinant = null;
            L = null;
            U = null;
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

        //Initialise la sous-matrice L à partir des coeficients récupéré par Gauss.
        private Matrice InitL(double[,] m)
        {
            Matrice L = new Matrice(m);

            for (int i = 0; i < nbrLn; i++)
            {
                L[i, i] = 1;
            }
            return L;
        }

        //Calcul le déterminant de la matrice.
        private double InitDeterminant()
        {
            //Se base sur la décomposition LU et ne peut donc pas être initialiser avant.
            if (L != null && U != null)
            {
                double det;
                double detU = 1;
                double detL = 1;
                int p=0;

                //Compte le nombre de permutations.
                for (int i = 0; i < swaps.GetLength(0); i++)
                {
                    if (swaps[i, 0] != -1)
                        p++;
                }

                for (int i = 0; i < NbrLn; i++)
                {
                    detU *= U[i, i];
                    detL *= L[i, i];
                }

                detU *= Math.Pow(-1, p);
                detL *= Math.Pow(-1, p);

                det = detU * detL;


                return det;
            }
            else
            {
                DecompositionLU();
                //Récursivité.
                return InitDeterminant();
            }
        }

        //Vérifie si une matrice est inversible.
        private bool IsInversible()
        {
            double determinant = InitDeterminant();

            if (determinant == 0)
                return false;
            else
                return true;
        }

        #endregion

        #region Public

        //Initialise la matrice identité.
        public static Matrice InitIdentite(int n)
        {
            Matrice m;
            m = new Matrice(n, n);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        m[i, j] = 1;
                    else
                        m[i, j] = 0;
                }
            }
            return m;
        }

        //Vérifie si la matrice est une matrice carré.
        public bool IsSquare()
        {
            return nbrCol == nbrLn;
        }

        //Affiche la matrice.
        public void Display()
        {
            //initialise le format d'affichage par rapport à la précision.
            String format = "{0," + (8+Precision) + ":#0.";
            for (int i = 0; i < Precision; i++)
                format += "0";
            format += "}";

            for (int i = 0; i < nbrLn; i++)
            {
                Console.Write('(');
                for (int j = 0; j < nbrCol; j++)
                {
   
                    Console.Write(String.Format(format, values[i, j]));
                }
                Console.WriteLine(")");
            }
        }

        //Renvoi l'affichage de la matrice.
        public List<String> Print()
        {
            //initialise le format par rapport à la précision.
            String format = "{0," + (8 + Precision) + ":#0.";
            for (int i = 0; i < Precision; i++)
                format += "0";
            format += "}";

            List<String> display = new List<string>();
            for (int i = 0; i < nbrLn; i++)
            {
                display.Add("(");
                for (int j = 0; j < nbrCol; j++)
                {

                    display[i] += String.Format(format, values[i, j]);
                }
                display[i] += ")";
            }
            display.Add("");
            return display;
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
                        temp[i, j] = 0;
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

        //Echange deux colonnes de place.
        public void SwapCol(int col1, int col2)
        {
            double tmp;

            for (int i = 0; i < nbrLn; i++)
            {
                tmp = values[i, col1];
                values[i, col1] = values[i, col2];
                values[i, col2] = tmp;
            }
        }

        //Triangularise la matrice par la méthode de Gauss.
        public Matrice Gauss(out int[,] swaps, out double[,] m)
        {
            if (IsSquare())
            {
                Matrice temp = new Matrice(values);
                double pivot;

                //Initialise la sortie.
                swaps = new int[nbrLn - 1, 2];
                m = new double[nbrLn, nbrCol];

                //Pour chaque étape. (k-1 étapes)
                for (int k = 0; k < nbrLn - 1; k++)
                {
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

                        if (pivot == 0)
                            throw new Exception("La matrice n'est pas inversible !");
                    }
                    else
                    {
                        //Indique que les lignes n'ont pas été permutés.
                        swaps[k, 0] = -1;
                        swaps[k, 1] = -1;
                    }

                    //Pour chaque ligne de la matrice. 
                    //Calcul du coefficient de l'étape k.
                    for (int i = k + 1; i < nbrLn; i++)
                    {
                        m[i, k] = temp[i, k] / pivot;

                        //Met les 0.
                        for (int j = k; j < nbrLn; j++)
                        {
                            temp[i, j] -= m[i, k] * temp[k, j];
                        }
                    }
                }
                return temp;
            }
            else
                throw new Exception("La matrice n'est pas carrée : impossible d'utiliser Gauss!");
            
        }

        //Triangularise la matrice par la méthode de Gauss avec une sortie String pour l'affichage.
        public Matrice Gauss(out int[,] swaps, out double[,] m, out List<String> display)
        {
            if (IsSquare())
            {
                Matrice temp = new Matrice(values);
                double pivot;

                //Initialise la sortie.
                display = new List<string>();
                swaps = new int[nbrLn - 1, 2];
                m = new double[nbrLn, nbrCol];

                //Pour chaque étape. (k-1 étapes)
                for (int k = 0; k < nbrLn - 1; k++)
                {
                    display.Add("Etape : " + (k + 1));
                    display.Add("---------");
                    display.Add("");

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

                        if (pivot == 0)
                            throw new Exception("La matrice n'est pas inversible !");
                    }
                    else
                    {
                        //Indique que les lignes n'ont pas été permutés.
                        swaps[k, 0] = -1;
                        swaps[k, 1] = -1;
                    }
                    display.Add(String.Format("Pivot = {0,6:#0.##}", pivot));


                    //Pour chaque ligne de la matrice. 
                    //Calcul du coefficient de l'étape k.
                    for (int i = k + 1; i < nbrLn; i++)
                    {
                        m[i, k] = temp[i, k] / pivot;
                        display.Add(String.Format("{0,5} = {1,6:#0.##}", "m"+(i + 1) + (k + 1), m[i, k]));

                        //Met les 0.
                        for (int j = k; j < nbrLn; j++)
                        {
                            temp[i, j] -= m[i, k] * temp[k, j];
                        }
                    }
                    display.Add("");

                    //Affiche le résultat à la fin de l'étape.
                    foreach (var item in temp.Print())
                    {
                        display.Add(item);
                    }
                    display.Add("");
                    //Affiche les lignes permutés pour chaque étapes.
                    if (swaps[k, 0] != -1)
                    {
                        display.Add("Lignes permutés : " + (swaps[k, 0] + 1) + " <-> " + (swaps[k, 1] + 1));
                        display.Add("");
                    }

                }
                return temp;
            }
            else
                throw new Exception("La matrice n'est pas carrée : impossible d'utiliser Gauss!");            
        }

        //Vérifie si deux matrices sont egales.
        public bool Equals(Matrice B)
        {
            bool check = true;
            int i=0, j=0;

            if (nbrLn == B.nbrLn && nbrCol == B.nbrCol)
            {
                while(i < B.nbrLn  && check == true)
                {
                    while (j < B.nbrCol && check == true)
                    {
                        check = Math.Round(values[i,j]) == B[i, j];
                        j++;
                    }
                    i++;
                }
            }
            else
                check = false;

            return check;
        }

        //Décompose la matrice en deux sous matrice L et U.
        public void DecompositionLU()
        {
            double[,] m;
            U = Gauss(out swaps, out m);
            L = InitL(m);
        }

        //Décompose la matrice en deux sous matrice L et U avec une sortie de l'affichage.
        public void DecompositionLU(out List<String> display)
        {
            double[,] m;
            U = Gauss(out swaps, out m, out display);
            L = InitL(m);
        }

        //Inverse la matrice.
        public void Inverse()
        {
            if (IsInversible())
            {
                Matrice LPrime = InverseL();
                Matrice UPrime = InverseU();

                this.values = (UPrime.Product(LPrime)).values;

                //Permute les colonnes si il y a eu des permutation de lignes.
                for (int i = 0; i < Swaps.GetLength(0); i++)
                {
                    if (Swaps[i, 0] != -1)
                    {
                        SwapCol(Swaps[i,0], Swaps[i,1]);
                    } 
                }

            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        //Inverse la matrice avec l'affichage en sortie.
        public Matrice Inverse(out List<String> display)
        {
            if (IsInversible())
            {
                Matrice inverse;
                display = new List<string>();
                Matrice LPrime = InverseL();
                Matrice UPrime = InverseU();

                inverse = UPrime.Product(LPrime);

                //Permute les colonnes si il y a eu des permutation de lignes.
                for (int i = Swaps.GetLength(0)-1; i >= 0; i--)
                {
                    if (Swaps[i, 0] != -1)
                    {
                        inverse.SwapCol(Swaps[i, 0], Swaps[i, 1]);
                        display.Add("Permutation de colonne : " + (Swaps[i, 0]+1) + " <->" +  (Swaps[i, 1]+1));
                    }
                }
                return inverse;
            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        //Inverse la matrice L.
        public Matrice InverseL()
        {
            if (IsInversible())
            {
                Matrice ident = InitIdentite(NbrLn);
                Matrice LPrime = new Matrice(NbrLn, NbrCol);
                double somme =0;

                //La première ligne est la même que l'identité.
                for (int j = 0; j < nbrCol; j++)
                    LPrime[0, j] = ident[0, j];

                //Pour chaque ligne.
                for (int i = 1; i < NbrLn; i++)
                {
                    //Pour chaque colonne.
                    for (int j = 0; j < NbrCol; j++)
                    {
                        somme = 0;

                        //Nombre d'élément de la somme.
                        for (int k = 0; k < i; k++)
                        {
                            somme += L[i, k] * LPrime[k, j];
                        }
                        
                        //Applique la formule.
                        LPrime[i, j] = ident[i, j]-somme;
                    }
                }
                return LPrime;
            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        //Inverse la matrice L avec sortie pour l'affichage.
        public Matrice InverseL(out List<String> display)
        {
            if (IsInversible())
            {
                Matrice ident = InitIdentite(NbrLn);
                Matrice LPrime = new Matrice(NbrLn, NbrCol);
                double somme = 0;
                display = new List<string>();
                display.Add("Etape 1: ");
                display.Add("--------");
                display.Add("");

                //La première ligne est la même que l'identité.

                for (int j = 0; j < nbrCol; j++)
                {
                    LPrime[0, j] = ident[0, j];
                    display.Add(String.Format("x1" + j + " = {0,8:#0.##} = &1" + j, LPrime[0, j]));
                }

                //Pour chaque ligne.
                for (int i = 1; i < NbrLn; i++)
                {
                    display.Add("");
                    display.Add("Etape " + (i+1) + ":");
                    display.Add("--------");
                    display.Add("");

                    //Pour chaque colonne.
                    for (int j = 0; j < NbrCol; j++)
                    {
                        somme = 0;
                        
                        String tmp = "";
                        //Nombre d'élément de la somme.
                        for (int k = 0; k < i; k++)
                        {
                            tmp += " - m" + (i+1) + (k+1) + " * " + "x" + (k+1) + (j+1); 
                            somme += L[i, k] * LPrime[k, j];
                        }

                        //Applique la formule.
                        LPrime[i, j] = ident[i, j] - somme;

                        display.Add(String.Format("x" + (i+1) + (j+1) + " = {0,8:#0.##} = &" + (i+1) + (j+1) + tmp, LPrime[i, j]));
                    }
                }
                display.Add("");
                display.Add("Matrice L inversé : ");
                foreach (var item in LPrime.Print())
                {
                    display.Add(item);
                }
                return LPrime;
            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        //Inverse la matrice U
        public Matrice InverseU()
        {
            if (IsInversible())
            {
                Matrice ident = InitIdentite(NbrLn);
                Matrice UPrime = new Matrice(NbrLn, NbrCol);
                double somme = 0;

                //La dernière ligne.
                for (int j = 0; j < nbrCol; j++)
                    UPrime[NbrLn-1, j] = ident[NbrLn-1, j]/U[NbrLn-1, NbrLn-1];

                //Pour chaque ligne.
                for (int i = NbrLn -2; i >= 0; i--)
                {
                    //Pour chaque colonne.
                    for (int j = 0; j < NbrCol; j++)
                    {
                        somme = 0;

                        //Nombre d'élément de la somme.
                        for (int k = i+1; k < NbrLn; k++)
                        {
                            somme += U[i, k] * UPrime[k, j];
                        }

                        //Applique la formule.
                        UPrime[i, j] = (ident[i, j] - somme)/U[i,i];
                    }
                }
                return UPrime;
            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        //Inverse la matrice U avec sortie pour l'affichage.
        public Matrice InverseU(out List<String> display)
        {
            if (IsInversible())
            {
                Matrice ident = InitIdentite(NbrLn);
                Matrice UPrime = new Matrice(NbrLn, NbrCol);
                double somme = 0;

                display = new List<string>();
                display.Add("Etape 1: ");
                display.Add("--------");
                display.Add("");

                //La dernière ligne.
                for (int j = 0; j < nbrCol; j++)
                {
                    UPrime[NbrLn - 1, j] = ident[NbrLn - 1, j] / U[NbrLn - 1, NbrLn - 1];
                    display.Add(String.Format("x"+ NbrLn + (j+1) + " = {0,8:#0.##} = &"+ NbrLn + (j+1) + "/u"+NbrLn+NbrLn , UPrime[NbrLn-1, j]));
                }

                //Pour chaque ligne.
                for (int i = NbrLn - 2; i >= 0; i--)
                {
                    display.Add("");
                    display.Add("Etape " + (NbrLn-i) + ":");
                    display.Add("--------");
                    display.Add("");
                    //Pour chaque colonne.
                    for (int j = 0; j < NbrCol; j++)
                    {
                        somme = 0;
                        String tmp = "";
                        //Nombre d'élément de la somme.
                        for (int k = i + 1; k < NbrLn; k++)
                        {
                            tmp += " - u" + (i + 1) + (k + 1) + " * " + "y" + (k + 1) + (j + 1);
                            somme += U[i, k] * UPrime[k, j];
                        }

                        //Applique la formule.
                        UPrime[i, j] = (ident[i, j] - somme) / U[i, i];
                        display.Add(String.Format("x" + (i + 1) + (j + 1) + " = {0,8:#0.##} = (&" + (i + 1) + (j + 1) + tmp + ")/u"+ (i+1)+(i+1), UPrime[i, j]));

                    }
                }

                display.Add("");
                display.Add("Matrice U inversé : ");
                foreach (var item in UPrime.Print())
                {
                    display.Add(item);
                }
                return UPrime;
            }
            else
                throw new Exception("La matrice n'est pas inversible : déterminant = 0");
        }

        #endregion

        #region Accesseurs

        public int NbrCol
        {
            get { return nbrCol; }
        }

        public int NbrLn
        {
            get { return nbrLn; }
        }

        //Indexeur pour accéder à une valeur de la matrice.
        public double this[int iLn, int iCol]
        {
            get { return values[iLn, iCol]; }
            set { values[iLn, iCol] = value; }
        }

        public int[,] Swaps
        {
            get { return swaps; }
        }

        public double Determinant
        {
            get
            {
                //Le determinant est null car la décomposition LU n'a pas encore été faite.
                if (determinant != null)
                    return (double)determinant;
                else
                {
                    determinant = InitDeterminant();
                    return (double)determinant;
                }
            }
        }

        public Matrice MatriceL
        {
            get { return L; }
        }

        public Matrice MatriceU
        {
            get { return U; }
        }



        #endregion

        #endregion

    }
}

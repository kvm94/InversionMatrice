using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Matrice mat;

                InputFile input = new InputFile();
                mat = input.ReadFile();

                if (mat != null)
                {
                    //Affiche la matrice initiale.
                    Console.WriteLine("Matrice");
                    Console.WriteLine("=======");
                    Console.WriteLine();
                    mat.print();
                    Console.WriteLine();

                    //Affiche la triangulisation par Gauss
                    Console.WriteLine("Gauss:");
                    Console.WriteLine("======");
                    Console.WriteLine();

                    int[,] swaps;
                    double[,] m;
                    SousMatrice U = mat.Gauss(out swaps, out m);

                    //Affichage des permutations
                    Console.WriteLine("Permutations");
                    Console.WriteLine("============");
                    Console.WriteLine();
                    for (int i = 0; i < swaps.GetLength(0); i++)
                    {
                       if (swaps[i,0] != -1)
                            Console.WriteLine("Lignes : " + (swaps[i,0]+1) + " <-> " + (swaps[i,1]+1));
                    }
                    Console.WriteLine();

                    //Affiche la matrice U
                    Console.WriteLine("Matrice U:");
                    Console.WriteLine("==========");
                    Console.WriteLine();

                    U.print();
                    Console.WriteLine();

                    //Affiche la matrice L
                    Console.WriteLine("Matrice L:");
                    Console.WriteLine("==========");
                    Console.WriteLine();
                    SousMatrice L = mat.InitL(m);
                    L.print();
                    Console.WriteLine();

                    //Affiche la vérification.
                    Console.WriteLine("Vérification A=L*U:");
                    Console.WriteLine("===================");
                    Console.WriteLine();
                    Matrice A = L.Product(U);
                    Console.WriteLine("Matrice A :");
                    //On refait les permutation pour vérifier si c'est la meme matrice.
                    for (int i = 0; i < swaps.GetLength(0); i++)
                    {
                        if (swaps[i, 0] != -1)
                            A.swapLn(swaps[i, 0], swaps[i, 1]);
                    }
                    A.print();
                    Console.WriteLine("Après permutations.");
                    Console.WriteLine();
                    Console.WriteLine("Matrice Initiale :");
                    mat.print();
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

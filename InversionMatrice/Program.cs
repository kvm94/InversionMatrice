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
                String display;
                bool flagSwaps;

                InputFile input = new InputFile();
                mat = input.ReadFile();

                if (mat != null)
                {
                    //Affiche la matrice initiale.
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=            Matrice à inverser               =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    mat.Display();
                    Console.WriteLine();

                    //Affiche la triangulisation par Gauss
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=                   Gauss:                    =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();

                    mat.DecompositionLU(out display);

                    //Affiche les étapes pour Gauss
                    Console.Write(display);

                    //Affichage des permutations
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=               Permutations                  =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    flagSwaps = false;
                    for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                    {
                       if (mat.Swaps[i,0] != -1)
                        {
                            flagSwaps = true;
                            Console.WriteLine("Lignes : " + (mat.Swaps[i, 0] + 1) + " <-> " + (mat.Swaps[i, 1] + 1));
                        }
                    }
                    if (!flagSwaps)
                        Console.WriteLine("Pas de permutations.");
                    Console.WriteLine();

                    //Affiche la matrice U
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=                  Matrice U                  =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();

                    mat.MatriceU.Display();
                    Console.WriteLine();

                    //Affiche la matrice L
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=                   Matrice L                 =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    mat.MatriceL.Display();
                    Console.WriteLine();

                    //Affiche la vérification.
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=              Vérification A=L*U             =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    Matrice A = mat.MatriceL.Product(mat.MatriceU);
                    Console.WriteLine("Matrice A :");
                    //On refait les permutation pour vérifier si c'est la meme matrice.
                    for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                    {
                        if (mat.Swaps[i, 0] != -1)
                            A.SwapLn(mat.Swaps[i, 0], mat.Swaps[i, 1]);
                    }
                    A.Display();
                    flagSwaps = false;
                    for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                    {
                        if (mat.Swaps[i, 0] != -1)
                            flagSwaps = true;
                    }
                    if(flagSwaps)
                        Console.WriteLine("Après permutations.");

                    Console.WriteLine();

                    if (mat.Equals(A))
                    {
                        Console.WriteLine("Matrices identiques ! ");
                        Console.WriteLine("");
                    }
                    else
                        throw new Exception("Les matrices ne sont pas identiques !");

                    //Affiche l'inverse des matrices.
                    Console.WriteLine("===============================================");
                    Console.WriteLine("=                     L'                      =");
                    Console.WriteLine("===============================================");
                    Console.WriteLine();
                    Matrice LPrime = mat.InverseL(out display);
                    Console.WriteLine(display);
                    Console.WriteLine();

                    //mat.Inverse();
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

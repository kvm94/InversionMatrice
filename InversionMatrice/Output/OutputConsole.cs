using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class OutputConsole:IOutput
    {
        public void Head()
        {
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("=                                      PROJET DE MATH                                             =");
            Console.WriteLine("=                       INVERSION D'UNE MATRICE PAR DECOMPOSITION LU                              =");
            Console.WriteLine("=                                     MARRA KEVIN 3IN                                             =");
            Console.WriteLine("=                                      HEPH CONDORCET                                             =");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine();
        }


        public void Menu()
        {
            Console.WriteLine("    ==========================================================================================");
            Console.WriteLine("    =                                          MENU                                          =");
            Console.WriteLine("    ==========================================================================================");
            Console.WriteLine();
            Console.WriteLine("              1)Inverser une matrice à partir d'un fichier texte -> Sortie .txt .");
            Console.WriteLine("              2)Inverser une matrice à partir d'un fichier texte -> Sortie console .");
            Console.WriteLine("              3)Inverser une matrice à partir de la console -> Sortie .txt");
            Console.WriteLine("              4)Inverser une matrice à partir de la console -> Sortie console");
            Console.WriteLine("              5)Quitter");
        }

        public void AfficherMatrice(Matrice mat)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=            Matrice à inverser               =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            mat.Display();
            Console.WriteLine();
        }

        public void AfficherGauss(List<String> display)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=                   Gauss:                    =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            //Affiche les étapes pour Gauss
            foreach (var item in display)
            {
                Console.WriteLine(item);
            };
            Console.WriteLine();
        }

        public void AfficherPermutations(Matrice mat, out bool flagSwaps)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=               Permutations                  =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            flagSwaps = false;
            for (int i = 0; i < mat.Swaps.GetLength(0); i++)
            {
                if (mat.Swaps[i, 0] != -1)
                {
                    flagSwaps = true;
                    Console.WriteLine("Lignes : " + (mat.Swaps[i, 0] + 1) + " <-> " + (mat.Swaps[i, 1] + 1));
                }
            }
            if (!flagSwaps)
                Console.WriteLine("Pas de permutations.");
            Console.WriteLine();
        }

        public void AfficherMatriceU(Matrice mat)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=                  Matrice U                  =");
            Console.WriteLine("===============================================");
            Console.WriteLine();

            mat.MatriceU.Display();
            Console.WriteLine();
        }

        public void AfficherMatriceL(Matrice mat)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=                   Matrice L                 =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            mat.MatriceL.Display();
            Console.WriteLine();
        }

        public void AfficherVérification(Matrice mat, Matrice A, out bool flagSwaps)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=              Vérification A=L*U             =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("Matrice A :");
            A.Display();

            flagSwaps = false;
            for (int i = 0; i < mat.Swaps.GetLength(0); i++)
            {
                if (mat.Swaps[i, 0] != -1)
                    flagSwaps = true;
            }
            if (flagSwaps)
                Console.WriteLine("Après permutations.");

            Console.WriteLine();
        }

        public void AfficherLInverse(ref List<String> display)
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=               Inversion de L                =");
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("& étant la matrice identité.");
            Console.WriteLine();

            foreach (var item in display)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.IO.StreamWriter;

namespace InversionMatrice
{
    class OutputFile:IOutput
    {
        public System.IO.StreamWriter Flux { get; }
        private String path;

        public OutputFile()
        {
            path = "output.txt";
            Flux = new System.IO.StreamWriter(path);
        }

        public OutputFile(String path)
        {
            this.path = path;
            Flux = new System.IO.StreamWriter(path);
        }

        public void WriteLine(String s)
        {
            Flux.WriteLine(s);
        } 

        public void Head()
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                       PROJET DE MATH                                            =");
            Flux.WriteLine("=                        INVERSION D'UNE MATRICE PAR DECOMPOSITION LU                             =");
            Flux.WriteLine("=                                      MARRA KEVIN 3IN                                            =");
            Flux.WriteLine("=                                       HEPH CONDORCET                                            =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
        }

        public void AfficherMatrice(Matrice mat)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                      Matrice à inverser                                         =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            foreach (var item in mat.Print())
            {
                Flux.WriteLine(item);
            }

            Flux.WriteLine();
        }

        public void AfficherGauss(List<String> display)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                            Gauss:                                               =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            //Affiche les étapes pour Gauss
            foreach (var item in display)
            {
                Flux.WriteLine(item);
            };
            Flux.WriteLine();
        }

        public void AfficherPermutations(Matrice mat, out bool flagSwaps)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                           Permutations                                          =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            flagSwaps = false;
            for (int i = 0; i < mat.Swaps.GetLength(0); i++)
            {
                if (mat.Swaps[i, 0] != -1)
                {
                    flagSwaps = true;
                    Flux.WriteLine("Lignes : " + (mat.Swaps[i, 0] + 1) + " <-> " + (mat.Swaps[i, 1] + 1));
                }
            }
            if (!flagSwaps)
                Flux.WriteLine("Pas de permutations.");
            Flux.WriteLine();
        }

        public void AfficherMatriceU(Matrice mat)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                             Matrice U                                           =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            foreach (var item in mat.MatriceU.Print())
            {
                Flux.WriteLine(item);
            }
            ;
            Flux.WriteLine();
        }

        public void AfficherMatriceL(Matrice mat)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                             Matrice L                                           =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            foreach (var item in mat.MatriceU.Print())
            {
                Flux.WriteLine(item);
            }
            ;
            Flux.WriteLine();
        }

        public void AfficherVerificationDecomp(Matrice mat, Matrice A, out bool flagSwaps)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                          Vérification A=L*U                                     =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            Flux.WriteLine("Matrice A :");
            foreach (var item in A.Print())
            {
                Flux.WriteLine(item);
            }
            ;

            flagSwaps = false;
            for (int i = 0; i < mat.Swaps.GetLength(0); i++)
            {
                if (mat.Swaps[i, 0] != -1)
                    flagSwaps = true;
            }
            if (flagSwaps)
                Flux.WriteLine("Après permutations.");

            Flux.WriteLine();
        }

        public void AfficherLInverse(ref List<String> display)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                           Inversion de L                                        =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            Flux.WriteLine("& étant la matrice identité.");
            Flux.WriteLine();


            foreach (var item in display)
            {
                Flux.WriteLine(item);
            }
            Flux.WriteLine();
        }

        public void AfficherUInverse(ref List<String> display)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                          Inversion de U                                         =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            Flux.WriteLine("& étant la matrice identité.");
            Flux.WriteLine();


            foreach (var item in display)
            {
                Flux.WriteLine(item);
            }
            Flux.WriteLine();
        }

        public void AfficherInverse(Matrice mat, ref List<String> display)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                       Résultat de l'inversion                                   =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();
            foreach (var item in display)
            {
                Flux.WriteLine(item);
            }

            foreach (var item in mat.Print())
            {
                Flux.WriteLine(item);
            }
        }

        public void AfficherVerification(Matrice A, Matrice B, Matrice res, bool check)
        {
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine("=                                     Vérification A*A(-1) = &                                    =");
            Flux.WriteLine("===================================================================================================");
            Flux.WriteLine();

            foreach (var item in A.Print())
            {
                Flux.WriteLine(item);
            }
            Flux.WriteLine("");
            Flux.WriteLine("     *");
            Flux.WriteLine("");
            foreach (var item in B.Print())
            {
                Flux.WriteLine(item);
            }
            Flux.WriteLine("");
            Flux.WriteLine("     =");
            Flux.WriteLine("");
            foreach (var item in res.Print())
            {
                Flux.WriteLine(item);
            }

            Flux.WriteLine();

            if (check)
                Flux.WriteLine("L'inversion de la matrice c'est bien déroulé!");
            else
                Flux.WriteLine("Erreur :  le résultat est faux!");
        }
    }
}

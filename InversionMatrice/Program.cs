using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class Program
    {
        public static OutputFile    OutputF { get; private set; }
        public static OutputConsole OutputC { get; private set; }

        //Programme principal.
        static void Main(string[] args)
        {
            
            //Boucle sur le Menu.
            do
            {
                try
                {
                    int choice;
                    OutputConsole o;

                    o = new OutputConsole();

                    //Affiche le menu principal.
                    do
                    {
                        Console.Clear();
                        o.Head();
                        o.Menu();
                        Console.WriteLine("Choix : ");

                        try
                        {
                            //Récupère le choix du menu.
                            choice = int.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        Console.WriteLine();
                    }
                    while (choice != 5 && choice != 4 && choice != 3 && choice != 2 && choice != 1);

                    //Effectue le choix demandé.
                    String path = "";

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Nom du fichier d'entré : ");
                            try
                            {
                                path = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);

                            }
                            Console.Clear();

                            OutputF = new OutputFile();
                            using (OutputF.Flux)
                            {
                                Output(LectureFichier(path), OutputF);
                            }
                            Console.WriteLine("Le fichier à bien été enregistré!");
                            Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Nom du fichier d'entré : ");
                            try
                            {
                                path = Console.ReadLine();
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);
                            }
                            Console.Clear();

                            OutputC = new OutputConsole();
                            Output(LectureFichier(path), OutputC);
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.Clear();
                            OutputF = new OutputFile();
                            using (OutputF.Flux)
                            {
                                Output(Saisie(), OutputF);
                            }
                            Console.WriteLine("Le fichier à bien été enregistré!");
                            Console.ReadLine();
                            break;
                        case 4:
                            Console.Clear();
                            OutputC = new OutputConsole();
                            Output(Saisie(), OutputC);
                            Console.ReadLine();
                            break;

                        case 5: Environment.Exit(0); break;
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                Console.Clear();
            }
            while (true);
        }

        //Lit la matrice à partir d'un fichier.
        public static Matrice LectureFichier(String path)
        {
            try
            {
                Matrice mat;
                InputFile input = new InputFile(path);
                mat = input.ReadFile();
                return mat;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //Demande à l'utilisateur de saisir la matrice dans la console.
        public static Matrice Saisie()
        {
            Matrice mat = null;
            double[,] data;
            int ln, col;
            bool flag = false; //Détermine si la saisie se déroule correctement.

            //Recommence tant que l'utilisateur n'a pas saisi une matrice. 
            //(Permet de ne pas retourner au menu principale si il y a une faute de frappe)
            do
            {
                try
                {
                    Console.WriteLine("Taille de la matrice : ");
                    Console.Write("Lignes ? ");
                    ln = int.Parse(Console.ReadLine());
                    Console.Write("Colonnes ? ");
                    col = int.Parse(Console.ReadLine());

                    data = new double[ln, col];

                    for (int i = 0; i < ln; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            Console.WriteLine("m" + i + j + ": ");
                            data[i, j] = double.Parse(Console.ReadLine());
                        }
                    }

                    mat = new Matrice(data);
                    flag = true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            while (!flag);


            Console.Clear();
            return mat;
        }

        //Effectue les calculs et écrit les résultats.
        public static void Output(Matrice mat, IOutput output)
        {
            List<String> display;
            bool flagSwaps;

            if (mat != null)
            {
                try
                {
                    //Affiche l'entête.
                    output.Head();

                    //Affiche la matrice initiale.
                    output.AfficherMatrice(mat);

                    //Affiche la triangulisation par Gauss après avoir fait la décomposition LU.
                    mat.DecompositionLU(out display);
                    output.AfficherGauss(display);

                    //Affichage des permutations
                    output.AfficherPermutations(mat, out flagSwaps);

                    //Affiche la matrice U
                    output.AfficherMatriceU(mat);

                    //Affiche la matrice L
                    output.AfficherMatriceL(mat);

                    //Affiche la vérification.
                    Matrice A = mat.MatriceL.Product(mat.MatriceU);

                    //On refait les permutation pour vérifier si c'est la même matrice.
                    for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                    {
                        if (mat.Swaps[i, 0] != -1)
                            A.SwapLn(mat.Swaps[i, 0], mat.Swaps[i, 1]);
                    }

                    //Affiche la vérification de la décomposition.
                    output.AfficherVerificationDecomp(mat, A, out flagSwaps);

                    if (mat.Equals(A))
                    {
                        output.WriteLine("Matrices identiques ! ");
                        output.WriteLine("Le determinant de la matrice = " + mat.Determinant);
                        output.WriteLine("");
                    }
                    else
                        throw new Exception("Les matrices ne sont pas identiques !");

                    //Affiche l'inverse des matrices.
                    bool check;
                    Matrice LPrime = mat.InverseL(out display);
                    output.AfficherLInverse(ref display);

                    /*Test*/
                    output.WriteLine("Vérification inverse L*L(-1)");
                    foreach (var item in (mat.MatriceL.Product(LPrime)).Print())
                    {
                        output.WriteLine(item);
                    }
                    /*Test*/

                    Matrice UPrime = mat.InverseU(out display);
                    output.AfficherUInverse(ref display);

                    /*Test*/
                    output.WriteLine("Vérification inverse U*U(-1)");
                    foreach (var item in (mat.MatriceU.Product(UPrime)).Print())
                    {
                        output.WriteLine(item);
                    }
                    /*Test*/

                    //Affiche la matrice inverse.
                    Matrice mInverse = mat.Inverse(out display);
                    output.AfficherInverse(mInverse, ref display);

                    //Vérification
                    Matrice res = mInverse.Product(mat);

                    if (res.Equals(Matrice.InitIdentite(mat.NbrLn)))
                        check = true;
                    else
                        check = false;

                    output.AfficherVerification(mInverse, mat, res, check);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }
        }
    }
}

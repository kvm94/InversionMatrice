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
                int choice;
                OutputConsole outputC;

                outputC = new OutputConsole();

                //Affiche le menu principal.
                do
                {
                    Console.Clear();
                    //Affiche l'entête.
                    outputC.Head();
                    outputC.Menu();

                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }                
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                        choice = -1;
                    }
                }
                while (choice != 5 && choice != 4 && choice != 3 && choice != 2 && choice != 1);

                //Effectue l'action en fonction du choix du menu principal.
                String path="";
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Nom du fichier d'entré : ");
                        try
                        {
                            path = Console.ReadLine();
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.Clear();
                        SortieTexte(LectureFichier(path));
                        break;
                    case 2:
                        Console.WriteLine("Nom du fichier d'entré : ");
                        try
                        {
                            path = Console.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.Clear();
                        SortieConsole(LectureFichier(path));
                        break;
                    case 3:
                        Console.Clear();
                        SortieTexte(Saisie());
                        break;
                    case 4:
                        Console.Clear();
                        SortieConsole(Saisie());
                        break;

                    case 5: Environment.Exit(0); break;               
                }

                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public static Matrice LectureFichier(String path)
        {
            Matrice mat;
            InputFile input = new InputFile(path);
            mat = input.ReadFile();
            return mat;
        }

        public static Matrice Saisie()
        {
            Matrice mat = null;
            double[,] data;
            int ln, col;
            bool flag =false;

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
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            while (!flag);
            

            Console.Clear();
            return mat;
        }

        public static void SortieTexte(Matrice mat)
        {
            OutputFile output = new OutputFile();
            List<String> display;
            bool flagSwaps;

            if (mat != null)
            {
                try
                {
                    using (output.Flux)
                    {
                        output.Head();
                        //Affiche la matrice initiale.
                        output.AfficherMatrice(mat);

                        //Affiche la triangulisation par Gauss
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

                        //On refait les permutation pour vérifier si c'est la meme matrice.
                        for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                        {
                            if (mat.Swaps[i, 0] != -1)
                                A.SwapLn(mat.Swaps[i, 0], mat.Swaps[i, 1]);
                        }

                        output.AfficherVérification(mat, A, out flagSwaps);

                        if (mat.Equals(A))
                        {
                            output.WriteLine("Matrices identiques ! ");
                            output.WriteLine("");
                        }
                        else
                            throw new Exception("Les matrices ne sont pas identiques !");

                        //Affiche l'inverse des matrices.

                        Matrice LPrime = mat.InverseL(out display);
                        output.AfficherLInverse(ref display);
                    }

                    Console.WriteLine("Le fichier à bien été enregistré!");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Erreur, le programme va se fermer.");
                    Console.ReadLine();
                }

            }
        }

        public static void SortieConsole(Matrice mat)
        {
            OutputConsole output = new OutputConsole();
            List<String> display;
            bool flagSwaps;

            if (mat != null)
            {
                //Affiche la matrice initiale.
                output.AfficherMatrice(mat);

                //Affiche la triangulisation par Gauss
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

                //On refait les permutation pour vérifier si c'est la meme matrice.
                for (int i = 0; i < mat.Swaps.GetLength(0); i++)
                {
                    if (mat.Swaps[i, 0] != -1)
                        A.SwapLn(mat.Swaps[i, 0], mat.Swaps[i, 1]);
                }

                output.AfficherVérification(mat, A, out flagSwaps);

                if (mat.Equals(A))
                {
                    Console.WriteLine("Matrices identiques ! ");
                    Console.WriteLine("");
                }
                else
                    throw new Exception("Les matrices ne sont pas identiques !");

                //Affiche l'inverse des matrices.
                
                Matrice LPrime = mat.InverseL(out display);
                output.AfficherLInverse(ref display);

                //mat.Inverse();

                Console.ReadLine();
            }

        }
    }
}

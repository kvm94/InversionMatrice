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
            do
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
                        Console.WriteLine("Choix : ");

                        try
                        {
                            choice = int.Parse(Console.ReadLine());
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        Console.WriteLine();
                    }
                    while (choice != 5 && choice != 4 && choice != 3 && choice != 2 && choice != 1);

                    //Effectue l'action en fonction du choix du menu principal.
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
                                throw new Exception(ex.Message);
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                Console.Clear();
            }
            while (true);
        }

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
                    throw new Exception(ex.Message);
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

                        Matrice mInverse = mat.Inverse(out display);

                        output.AfficherInverse(mInverse, ref display);

                        Matrice res = mInverse.Product(mat);


                        //Vérification
                        if (res.Equals(Matrice.InitIdentite(mat.NbrLn)))
                            check = true;
                        else
                            check = false;

                        output.AfficherVerification(mInverse, mat, res, check);
                    }

                    Console.WriteLine("Le fichier à bien été enregistré!");
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }

            }
        }

        public static void SortieConsole(Matrice mat)
        {
            OutputConsole output = new OutputConsole();
            List<String> display;
            bool flagSwaps;

            try
            {
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

                    output.AfficherVerificationDecomp(mat, A, out flagSwaps);

                    if (mat.Equals(A))
                    {
                        Console.WriteLine("Matrices identiques ! ");
                        Console.WriteLine("Le determinant de la matrice = " + mat.Determinant);
                        Console.WriteLine();
                    }
                    else
                        throw new Exception("Les matrices ne sont pas identiques !");

                    //Affiche l'inverse des matrices.
                    bool check;
                    Matrice LPrime = mat.InverseL(out display);
                    output.AfficherLInverse(ref display);

                    /*Test*/

                    Console.WriteLine("Vérification inverse L*L(-1)");
                    (mat.MatriceL.Product(LPrime)).Display();

                    /*Test*/

                    Matrice UPrime = mat.InverseU(out display);
                    output.AfficherUInverse(ref display);

                    /*Test*/

                    Console.WriteLine("Vérification inverse U*U(-1)");
                    (mat.MatriceU.Product(UPrime)).Display();

                    /*Test*/

                    Matrice mInverse = mat.Inverse(out display);

                    output.AfficherInverse(mInverse, ref display);

                    Matrice res = mInverse.Product(mat);


                    //Vérification
                    if (res.Equals(Matrice.InitIdentite(mat.NbrLn)))
                        check = true;
                    else
                        check = false;

                    output.AfficherVerification(mInverse, mat, res, check);

                    Console.ReadLine();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }
    }
}

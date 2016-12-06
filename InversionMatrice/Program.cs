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
                Matrice m;

                InputFile input = new InputFile();
                m = input.ReadFile();

                if (m != null)
                {
                    m.print();
                    Console.WriteLine();
                    Console.WriteLine("Gauss:");
                    Console.WriteLine();

                    SousMatrice U = m.Gauss();


                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}

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
            Matrice m;

            InputFile input = new InputFile();
            m = input.ReadFile();

            if (m != null)
                m.print();
        }
    }
}

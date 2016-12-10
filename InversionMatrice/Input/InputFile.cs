using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    class InputFile
    {
        private string[] lines;
        private string path;

        public InputFile()
        {
            path = "input.txt";
            lines = null;
        }

        public InputFile(String path)
        {
            this.path = path;
            lines = null;
        }

        public Matrice ReadFile()
        {
            try
            {
                lines = System.IO.File.ReadAllLines(path);
                if (lines.Length == 0)
                    throw new Exception("Le fichier est vide!");
                return LoadMatrice();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }

        private Matrice LoadMatrice()
        {
            Matrice m = null;
            int ln, col;
            String[] temp;

            temp = lines[0].Split(' ');

            ln = lines.Length;
            col = temp.Length;
            m = new Matrice(ln, col);

            for (int i = 0; i < ln; i++)
            {
                temp = lines[i].Split(' ');

                for (int j = 0; j < col ; j++)
                {
                    m[i, j] = double.Parse(temp[j]);
                }
            }

            return m;
        }
    }
}

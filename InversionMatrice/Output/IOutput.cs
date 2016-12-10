using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionMatrice
{
    interface IOutput
    {
        void WriteLine(String s);
        void Head();
        void AfficherMatrice(Matrice mat);
        void AfficherGauss(List<String> display);
        void AfficherPermutations(Matrice mat, out bool flagSwaps);
        void AfficherMatriceU(Matrice mat);
        void AfficherMatriceL(Matrice mat);
        void AfficherVerificationDecomp(Matrice mat, Matrice A, out bool flagSwaps);
        void AfficherLInverse(ref List<String> display);
        void AfficherUInverse(ref List<String> display);
        void AfficherInverse(Matrice mat, ref List<String>display);
        void AfficherVerification(Matrice A, Matrice B, Matrice res, bool check);

    }
}

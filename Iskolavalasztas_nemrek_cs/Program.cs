using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iskolavalasztas_nemrek_cs
{
    class Program
    {

        static Func<List<List<int>>, List<int>, int, int, bool> jó(List<int> K) => ((L, C, j, i) => jó(K, L, C, j, i));

        static bool jó(List<int> K, List<List<int>> L, List<int> c, int J, int i)
        {
            List<int> kapacitások = new List<int>(K);
            for (int j = 0; j < J; j++)
            {
                kapacitások[L[j][c[j]] - 1]--;
            }

            kapacitások[L[J][i]-1]--;
            return kapacitások.Count(x => x < 0) == 0;
        }



        static int Helye(List<List<int>> L, List<int> c, int j, Func<List<List<int>>, List<int>, int, int, bool> P)
        {
            int i = c[j]+1;
            while (i<L[j].Count && !P(L, c, j, i)) { i++; }
            return i;
        }

        static List<int> Keres(List<List<int>> L, Func<List<List<int>>, List<int>, int, int, bool> P)
        {
            int j = 0;
            List<int> c = new List<int>(L.Count);  for (int i = 0; i < L.Count; i++) { c.Add(-1); } //inicializálás
            while (0 <= j && j < L.Count)
            {
                int h = Helye(L, c, j, P);
                if (h < L[j].Count)
                    c[j++] = h;
                else
                    c[j--] = -1;
            }
            return c;
        }

        static void Main(string[] args)
        {
            // Beolvasás

            string[] sortomb = Console.ReadLine().Split(' ');
            int N = int.Parse(sortomb[0]);
            int M = int.Parse(sortomb[1]);

            List<List<int>> L = new List<List<int>>();
            List<int> K = new List<int>();

            for (int i = 0; i < N; i++)
            {
                sortomb = Console.ReadLine().Split(' ');
                if (sortomb[1] != "0")
                    L.Add(new List<int> { int.Parse(sortomb[0]), int.Parse(sortomb[1]) });
                else
                    L.Add(new List<int> { int.Parse(sortomb[0]) });
            }

            for (int i = 0; i < M; i++)
                K.Add(int.Parse(Console.ReadLine()));

            //Console.Error.WriteLine("beolvastam!");

            // Keresés

            List<int> mo = Keres(L, jó(K));

            for (int j = 0; j < L.Count; j++)
            {
                Console.Write(L[j][mo[j]]+" ");
            }


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace EditDistance
{
    class Program
    {
        static List<string> path = new List<string>();
        static void Main(string[] args)
        {
            
            string w1 = "drive";
            string w2 = "drivers";
            int[,] res = Compute(w1, w2);

            Console.WriteLine(w1 + " " + w2);
            Console.WriteLine();

            for (int i = 0; i <= w1.Length + 1; i++)
            {
                for (int j = 0; j <= w2.Length + 1; j++)
                {
                    Console.Write(String.Format("{0,3}", res[i, j]));
                }
                    
                Console.WriteLine();
            }

            Console.WriteLine();
            GoBack();
        }

        public static int[,] Compute(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            int[,] d = new int[n + 2, m + 2]; // Приводил к виду из примера в презентации, чтобы были пустые строки
            d[1, 1] = 0;

            for (int i = 2; i <= n + 1; i++)
            {
                d[i, 1] = d[i - 1, 1] + 1;
            }   

            for (int j = 2; j <= m + 1; j++)
            {
                d[1, j] = d[1, j - 1] + 1;
            }

            for (int i = 2; i <= n + 1; i++)
            {
                for (int j = 2; j <= m + 1; j++)
                {
                    int cost = (t[j-2] == s[i-2]) ? 0 : 2;

                    int[] temp = new int[] { d[i - 1, j - 1] + cost, d[i - 1, j] + 1, d[i, j - 1] + 1};
                    d[i, j] = temp.Min();

                    switch (Array.IndexOf(temp, d[i, j]))
                    {
                        case 1:
                            path.Add(i + " " + j + ";deletion;" + (i - 1) + " " + j);
                            break;
                        case 2:
                            path.Add(i + " " + j + ";insertion;" + i + " " + (j - 1));
                            break;
                        case 0:
                            path.Add(i + " " + j + ";substitution;" + (i - 1) + " " + (j - 1));
                            break;
                    }
                }
            }

            return d;
        }

        public static void GoBack()
        {
            List<string[]> parsed = new List<string[]>();
            foreach (var p in path)
                parsed.Add(p.Split(';'));

            Console.WriteLine(parsed.Last()[0] + " " + parsed.Last()[1] + " from " + parsed.Last()[2]);
            var current = parsed.Last()[2];
            var prev = "";

            while (prev != current)
            {
                prev = current;
                foreach (var p in parsed)
                {
                    if (p[0] == current)
                    {
                        current = p[2];
                        Console.WriteLine(p[0] + " " + p[1] + " from " + p[2]);
                    }
                }
            }

        }
    }
}

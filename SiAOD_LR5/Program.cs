using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAOD_LR5
{
    public class Program
    {
        private static List<int[]> A = new List<int[]>();

        static void Main(string[] args)
        {
            const int n = 9;
            int[,] matrix = new int[n, n]
                {
                    { 0,3,2,0,0,0,0,0,0 },
                    { 3,0,1,1,0,0,0,0,0 },
                    { 2,1,0,1,1,0,0,0,0 },
                    { 0,1,1,0,0,1,1,0,0 },
                    { 0,0,1,0,0,5,0,0,0 },
                    { 0,0,0,1,5,0,4,1,0 },
                    { 0,0,0,1,0,4,0,0,1 },
                    { 0,0,0,0,0,1,0,0,2 },
                    { 0,0,0,0,0,0,1,2,0 }
                };
            //{
            //    { 0,3,0,1,0,0 },
            //    { 3,0,2,1,0,1 },
            //    { 0,2,0,0,0,2 },
            //    { 1,1,0,0,3,0 },
            //    { 0,0,0,3,0,2 },
            //    { 0,1,2,0,2,0 }
            //};
            Selection select = new Selection();
            for (int i = 2; i < n; i++)
            {
                do
                {
                    A.Clear();
                    A = select.GetRefactorA(i, select.GetA(matrix, i, A, null));
                    if (A.Count != 0)
                    {
                        matrix = select.GetFactorized(matrix, A);
                        Print(matrix, A);
                    }
                }
                while (A.Count != 0);
            }
            Console.ReadKey();
        }

        public static void Print(int[,] matrix, List<int[]> A)
        {
            Console.Write("A:");
            int k = 0;
            foreach (var item in A)
            {
                Console.Write("\nH{0} ( ", k++);
                for (int i = 0; i < item.Length; i++)
                    Console.Write("{0} ", item[i]);
                Console.Write(")");
            }
            Console.Write("\n");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                    Console.Write("{0} ", matrix[i, j]);
                Console.WriteLine();
            }
            Console.Write("\n");
        }
    }
}
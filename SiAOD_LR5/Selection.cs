using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiAOD_LR5
{
    public class Selection
    {
        public List<int[]> GetA(int[,] matrix, int n, List<int[]> A, int[] arrayIndex)
        {
            arrayIndex = arrayIndex ?? (new int[n]);
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                arrayIndex = SetArrayIndex(i, n, arrayIndex);
                if (n != 1)
                    A = GetA(matrix, n - 1, A, arrayIndex);
                if (n == 1 && IsUniqueIndex(arrayIndex) && IsL(matrix, arrayIndex) && UniqueArray(A, arrayIndex))
                    A.Add(CopyArray(arrayIndex));
            }
            return A;
        }

        public int[,] GetFactorized(int[,] matrix, List<int[]> A)
        {
            int n = matrix.GetLength(0) - (A.Count * (A[0].Length - 1)), l = 0;
            int[,] outMatrix = new int[n, n];
            int[] arrayLine = new int[n];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (IsA(i, A))
                {
                    arrayLine = GetFactorizedLine(i, n, matrix, A);
                    i += A[0].Length - 1 + i;
                }
                else
                    arrayLine = GetOldLine(i, n, matrix, A);
                for (int j = 0; j < n; j++)
                    outMatrix[l, j] = arrayLine[j];
                l++;
            }
            int indexSpan = 0;
            foreach (var item in A)
            {
                arrayLine = GetFactorizedLine(item[0], n, matrix, A);
                for (int j = 0; j < n; j++)
                    outMatrix[j, item[0] - indexSpan] = arrayLine[j];
                indexSpan++;
            }
            return outMatrix;
        }

        public List<int[]> GetRefactorA(int n, List<int[]> A)
        {
            List<int[]> outA = new List<int[]>();
            int count = Factorial(n);
            for (int i = 0; i < A.Count; i += count)
                outA.Add(A[i]);
            return outA;
        }

        private int Factorial(int n)
        {
            int result = 1;
            while (n != 0)
                result *= n--;
            return result;
        }

        private int[] CopyArray(int[] arrayIndex)
        {
            int[] outArray = new int[arrayIndex.Length];
            for (int i = 0; i < arrayIndex.Length; i++)
                outArray[i] = arrayIndex[i];
            return outArray;
        }

        private int[] SetArrayIndex(int i, int n, int[] array)
        {
            array[array.Length - n] = i;
            return array;
        }

        private int[] GetFactorizedLine(int i, int n, int[,] matrix, List<int[]> A)
        {
            int[] outArray = new int[n];
            int k = 0;
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                outArray[k++] = IsA(i, A) && IsA(j, A) ? 0 : GetFactorizedElement(i, j, matrix, A);
                j = IsA(j, A) ? A[0].Length - 1 + j : j;
            }
            return outArray;
        }

        private int[] GetOldLine(int i, int n, int[,] matrix, List<int[]> A)
        {
            int[] outArray = new int[n];
            int k = 0;
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                outArray[k++] = IsA(j, A) ? 0 : matrix[i, j];
                j = IsA(j, A) ? A[0].Length - 1 + j : j;
            }
            return outArray;
        }

        private int GetFactorizedElement(int i, int j, int[,] matrix, List<int[]> A)
        {
            int size = A[0].Length, result = 0;
            for (int k = i; k < size + i; k++)
                result += matrix[k, j];
            return result;
        }

        private bool UniqueArray(List<int[]> A, int[] array)
        {
            bool result = true;
            int[] copyArray = CopyArray(array);
            Array.Sort(copyArray);
            foreach (var item in A)
            {
                int[] copyArrayA = CopyArray(item);
                Array.Sort(copyArrayA);
                for (int i = 0; i < copyArray.Length; i++)
                    for (int j = 0; j < copyArrayA.Length; j++)
                        if (i != j && copyArray[i] == copyArrayA[j])
                            result = false;
            }
            return result;
        }

        private bool IsUniqueIndex(int[] indexArray)
        {
            bool result = true;
            for (int i = 0; i < indexArray.Length; i++)
                for (int j = i + 1; j < indexArray.Length; j++)
                    if (indexArray[i] == indexArray[j])
                        result = false;
            return result;
        }

        private bool IsA(int i, List<int[]> A)
        {
            bool isA = false;
            foreach (var item in A)
                if (item[0] == i)
                    isA = true;
            return isA;
        }

        private bool IsL(int[,] matrix, int[] array)
        {
            int L = 0;
            for (int i = 0; i < array.Length; i++)
                L += P(array[i], matrix);
            for (int i = 0; i < array.Length; i++)
                for (int j = i; j < array.Length; j++)
                    L -= 2 * R(array[i], array[j], matrix);
            bool result = true;
            for (int i = 0; i < array.Length; i++)
                if (L >= P(array[i], matrix))
                    result = false;
            return result;
        }

        private int P(int index, int[,] matrix)
        {
            int result = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
                result += matrix[index, i];
            return result;
        }

        private int R(int i, int j, int[,] matrix)
        {
            return matrix[i, j];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Task3
    {
        static int[,] CreateMatrix(int rows, int columns)
        {
            Random rand = new Random();
            int x, y;
            int[,] newMatrix = new int[rows, columns];
            for (x = 0; x < rows; x++)
            {
                for (y = 0; y < columns; y++)
                {
                    newMatrix[x, y] = rand.Next(0,100);
                }
            }
            return newMatrix;
        }

        public static void MultiplyMatrices()
        {
            int[,] firstMatrix = CreateMatrix(5, 6);
            PrintMatrix(firstMatrix);

            int[,] secondMatrix = CreateMatrix(6, 3);
            PrintMatrix(secondMatrix);

            int firstMatrixRows = firstMatrix.GetLength(0);
            int firstMatrixCols = firstMatrix.GetLength(1);
            int secondMatrixRows = secondMatrix.GetLength(0);
            int secondMatrixCols = secondMatrix.GetLength(1);

            if (firstMatrixCols != secondMatrixRows)
                throw new Exception("Matrices can not be multiplied");

            int[,] resultMatrix = new int[firstMatrixRows, secondMatrixCols];

            Parallel.For(0, firstMatrixRows, i =>
                {
                    for (int x = 0; x < secondMatrixCols; ++x)
                    for (int y = 0; y < firstMatrixCols; ++y)
                        resultMatrix[i,x] += firstMatrix[i,y] * secondMatrix[y,x];
                }
            );
            PrintMatrix(resultMatrix);
        }

        static void PrintMatrix(int[,] matrixToPrint)
        {
            int x, y;
            for (x = 0; x < matrixToPrint.GetLength(0); x++)
            {
                for (y = 0; y < matrixToPrint.GetLength(1); y++)
                {
                    Console.Write(matrixToPrint[x, y] + "\t");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}

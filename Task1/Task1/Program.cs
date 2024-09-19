using System;
using System.IO;

class Program
{
    static void Main()
    {
        int n;
        double[,] G;
        double[] x;

        using (StreamReader reader = new StreamReader("input.txt"))
        {
            // Чтение размерности пространства
            if (!int.TryParse(reader.ReadLine(), out n) || n <= 0)
            {
                Console.WriteLine("Ошибка: некорректная размерность.");
                return;
            }

            G = new double[n, n];
            x = new double[n];

            // Чтение матрицы G
            for (int i = 0; i < n; i++)
            {
                string[] values = reader.ReadLine().Split();
                if (values.Length != n)
                {
                    Console.WriteLine("Ошибка: количество элементов в строке матрицы G не соответствует размерности.");
                    return;
                }
                for (int j = 0; j < n; j++)
                {
                    if (!double.TryParse(values[j], out G[i, j]))
                    {
                        Console.WriteLine("Ошибка: некорректное значение в матрице G.");
                        return;
                    }
                }
            }

            // Чтение вектора x
            string[] vectorValues = reader.ReadLine().Split();
            if (vectorValues.Length != n)
            {
                Console.WriteLine("Ошибка: количество элементов в векторе x не соответствует размерности.");
                return;
            }
            for (int i = 0; i < n; i++)
            {
                if (!double.TryParse(vectorValues[i], out x[i]))
                {
                    Console.WriteLine("Ошибка: некорректное значение в векторе x.");
                    return;
                }
            }
        }

        // Проверка симметричности матрицы G
        if (!IsSymmetric(G, n))
        {
            Console.WriteLine("Матрица G не является симметричной.");
            return;
        }

        // Нахождение длины вектора
        double length = CalculateLength(x, G, n);
        Console.WriteLine($"Длина вектора: {length}");
    }

    static bool IsSymmetric(double[,] matrix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i, j] != matrix[j, i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    static double CalculateLength(double[] x, double[,] G, int n)
    {
        double[] temp = new double[n];

        // Умножение G на x
        for (int i = 0; i < n; i++)
        {
            temp[i] = 0;
            for (int j = 0; j < n; j++)
            {
                temp[i] += G[i, j] * x[j];
            }
        }

        // Умножение результата на x^T
        double lengthSquared = 0;
        for (int i = 0; i < n; i++)
        {
            lengthSquared += temp[i] * x[i];
        }

        return Math.Sqrt(lengthSquared);
    }
}

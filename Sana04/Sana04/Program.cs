using System;

class Program
{
    static void Main()
    {
        int rows = 3;
        int cols = 3;

        Random random = new Random();

        int[,] matrix = generateRandomMatrix(rows, cols, random);

        printMatrix(matrix);

        int positiveCount = countPositiveElements(matrix);
        Console.WriteLine($"Кількість додатних елементів у матриці: {positiveCount}");

        int maxRepeating = findMaxRepeating(matrix);
        Console.WriteLine($"Максимальне число, яке зустрічається більше одного разу: {maxRepeating}");

        int rowsWithoutZerosCount = countRowsWithoutZeros(matrix);
        Console.WriteLine($"Кількість рядків без нульових елементів: {rowsWithoutZerosCount}");

        int columnsWithZerosCount = countColumnsWithZeros(matrix);
        Console.WriteLine($"Кількість стовпців з хоча б одним нульовим елементом: {columnsWithZerosCount}");

        int longestSeriesRow = findRowWithLongestSeries(matrix);
        Console.WriteLine($"Номер рядка з найдовшою серією однакових елементів: {longestSeriesRow}");

        var productWithoutNegatives = calculateProductWithoutNegatives(matrix);
        foreach (var entry in productWithoutNegatives)
        {
            Console.WriteLine($"Добуток елементів у рядку {entry.Key}: {entry.Value}");
        }

        int maxSumParallelDiagonals = findMaxSumParallelDiagonals(matrix);
        Console.WriteLine($"Максимум серед сум елементів діагоналей, паралельних головній діагоналі: {maxSumParallelDiagonals}");

        Dictionary<int, int> sumWithoutNegatives = calculateSumWithoutNegatives(matrix);
        foreach (var entry in sumWithoutNegatives)
        {
            Console.WriteLine($"Сума елементів у стовпці {entry.Key}, який не містить від'ємних елементів: {entry.Value}");
        }

        int minSumParallelSecondaryDiagonals = findMinSumParallelSecondaryDiagonals(matrix);
        Console.WriteLine($"Мінімум серед сум модулів елементів діагоналей, паралельних побічній діагоналі: {minSumParallelSecondaryDiagonals}");

        int sumColumnsWithNegatives = calculateSumColumnsWithNegatives(matrix);
        Console.WriteLine($"Сума елементів у стовпцях з хоча б одним від'ємним елементом: {sumColumnsWithNegatives}");

        int[,] transposedMatrix = transposeMatrix(matrix);
        Console.WriteLine("Транспонована матриця:");
        printMatrix(transposedMatrix);
    }

    static int[,] generateRandomMatrix(int rows, int cols, Random random)
    {
        int[,] matrix = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = random.Next(-10, 10);
            }
        }

        return matrix;
    }

    static void printMatrix(int[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{matrix[i, j]} \t");
            }
            Console.WriteLine();
        }
    }

    static int countPositiveElements(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int positiveCount = 0;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                if (matrix[i, j] > 0)
                {
                    positiveCount++;
                }
            }
        }

        return positiveCount;
    }

    static int findMaxRepeating(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        Dictionary<int, int> occurrences = new Dictionary<int, int>();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                int currentNumber = matrix[i, j];
                if (occurrences.ContainsKey(currentNumber))
                {
                    occurrences[currentNumber]++;
                }
                else
                {
                    occurrences[currentNumber] = 1;
                }
            }
        }

        int maxRepeating = occurrences
            .Where(pair => pair.Value > 1)
            .Select(pair => pair.Key)
            .DefaultIfEmpty(0)
            .Max();

        return maxRepeating;
    }

    static int countRowsWithoutZeros(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int rowsWithoutZerosCount = 0;

        for (int i = 0; i < rowCount; i++)
        {
            bool hasZero = false;
            for (int j = 0; j < colCount; j++)
            {
                if (matrix[i, j] == 0)
                {
                    hasZero = true;
                    break;
                }
            }

            if (!hasZero)
            {
                rowsWithoutZerosCount++;
            }
        }

        return rowsWithoutZerosCount;
    }

    static int countColumnsWithZeros(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int columnsWithZerosCount = 0;

        for (int j = 0; j < colCount; j++)
        {
            for (int i = 0; i < rowCount; i++)
            {
                if (matrix[i, j] == 0)
                {
                    columnsWithZerosCount++;
                    break;
                }
            }
        }

        return columnsWithZerosCount;
    }

    static int findRowWithLongestSeries(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int longestSeriesRow = -1;
        int maxSeriesLength = 0;

        for (int i = 0; i < rowCount; i++)
        {
            int currentSeriesLength = 1;

            for (int j = 1; j < colCount; j++)
            {
                if (matrix[i, j] == matrix[i, j - 1])
                {
                    currentSeriesLength++;
                }
                else
                {
                    currentSeriesLength = 1;
                }

                if (currentSeriesLength > maxSeriesLength)
                {
                    maxSeriesLength = currentSeriesLength;
                    longestSeriesRow = i;
                }
            }
        }

        return longestSeriesRow;
    }

    static Dictionary<int, int> calculateProductWithoutNegatives(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        Dictionary<int, int> productWithoutNegatives = new Dictionary<int, int>();

        for (int i = 0; i < rowCount; i++)
        {
            bool hasNegative = false;
            int product = 1;

            for (int j = 0; j < colCount; j++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true;
                    break;
                }

                product *= matrix[i, j];
            }

            if (!hasNegative)
            {
                productWithoutNegatives[i] = product;
            }
        }

        return productWithoutNegatives;
    }

    static int findMaxSumParallelDiagonals(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int maxSum = int.MinValue;

        for (int startRow = 0; startRow < rowCount; startRow++)
        {
            int sum = 0;
            int row = startRow;
            int col = 0;

            while (row < rowCount && col < colCount)
            {
                sum += matrix[row, col];
                row++;
                col++;
            }

            maxSum = Math.Max(maxSum, sum);
        }
        for (int startCol = 1; startCol < colCount; startCol++)
        {
            int sum = 0;
            int row = 0;
            int col = startCol;

            while (row < rowCount && col < colCount)
            {
                sum += matrix[row, col];
                row++;
                col++;
            }

            maxSum = Math.Max(maxSum, sum);
        }

        return maxSum;
    }

    static Dictionary<int, int> calculateSumWithoutNegatives(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        Dictionary<int, int> sumWithoutNegatives = new Dictionary<int, int>();

        for (int j = 0; j < colCount; j++)
        {
            bool hasNegative = false;
            int sum = 0;

            for (int i = 0; i < rowCount; i++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true;
                    break;
                }

                sum += matrix[i, j];
            }

            if (!hasNegative)
            {
                sumWithoutNegatives[j] = sum;
            }
        }



        return sumWithoutNegatives;
    }

    static int findMinSumParallelSecondaryDiagonals(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int minSum = int.MaxValue;

        for (int startRow = rowCount - 1; startRow >= 0; startRow--)
        {
            int sum = 0;
            int row = startRow;
            int col = 0;

            while (row >= 0 && col < colCount)
            {
                sum += Math.Abs(matrix[row, col]);
                row--;
                col++;
            }

            minSum = Math.Min(minSum, sum);
        }

        for (int startCol = 1; startCol < colCount; startCol++)
        {
            int sum = 0;
            int row = rowCount - 1;
            int col = startCol;

            while (row >= 0 && col < colCount)
            {
                sum += Math.Abs(matrix[row, col]);
                row--;
                col++;
            }

            minSum = Math.Min(minSum, sum);
        }

        return minSum;
    }

    static int calculateSumColumnsWithNegatives(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int sumColumnsWithNegatives = 0;

        for (int j = 0; j < colCount; j++)
        {
            bool hasNegative = false;
            int sum = 0;

            for (int i = 0; i < rowCount; i++)
            {
                if (matrix[i, j] < 0)
                {
                    hasNegative = true;
                    break;
                }

                sum += matrix[i, j];
            }

            if (hasNegative)
            {
                sumColumnsWithNegatives += sum;
            }
        }

        return sumColumnsWithNegatives;
    }

    static int[,] transposeMatrix(int[,] matrix)
    {
        int rowCount = matrix.GetLength(0);
        int colCount = matrix.GetLength(1);
        int[,] transposedMatrix = new int[colCount, rowCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                transposedMatrix[j, i] = matrix[i, j];
            }
        }

        return transposedMatrix;
    }
}
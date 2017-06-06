using System;
using System.Collections.Generic;
using System.Linq;

namespace TargetPractice
{
    class TargetPractice
    {
        static void Main()
        {
            var input = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var snake = Console.ReadLine();
            char[][] matrix = new char[input[0]][];
            FillUpMatrix(input, snake, matrix);
            var shotParameters = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var impactRow = shotParameters[0];
            var impactColumn = shotParameters[1];
            var radius = shotParameters[2];
            TurnSymbolIntoSpace(matrix, impactRow, impactColumn, radius);

            MoveSymbols(input, matrix);
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join("", row));
            }
        }

        private static void MoveSymbols(int[] input, char[][] matrix)
        {
            for (int colIndex = 0; colIndex < input[1]; colIndex++)
            {
                var movedSymbols = new Queue<char>();
                for (int rowIndex = matrix.Length - 1; rowIndex >= 0; rowIndex--)
                {
                    if (matrix[rowIndex][colIndex] != ' ')
                    {
                        movedSymbols.Enqueue(matrix[rowIndex][colIndex]);
                    }

                }
                for (int rowIndex = matrix.Length - 1; rowIndex >= 0; rowIndex--)
                {
                    matrix[rowIndex][colIndex] = (movedSymbols.Count > 0) ? movedSymbols.Dequeue() : ' ';
                }
            }
        }

        private static void TurnSymbolIntoSpace(char[][] matrix, int impactRow, int impactColumn, int radius)
        {
            for (int rowIndex = 0; rowIndex < matrix.Length; rowIndex++)
            {
                for (int colIndex = 0; colIndex < matrix[rowIndex].Length; colIndex++)
                {
                    bool isWithinRange = Math.Sqrt(Math.Pow(Math.Abs(impactRow - rowIndex), 2)
                                                   + Math.Pow(Math.Abs(impactColumn - colIndex), 2)) <= radius;
                    if(isWithinRange)
                    {
                        matrix[rowIndex][colIndex] = ' ';
                    }
                }
            }
        }

        private static void FillUpMatrix(int[] input, string snake, char[][] matrix)
        {
            var snakeIndex = 0;
            for (int rowIndex = matrix.Length - 1; rowIndex >= 0; rowIndex -= 2)
            {
                matrix[rowIndex] = new char[input[1]];

                for (int colIndex = input[1] - 1; colIndex >= 0; colIndex--)
                {
                    if (snakeIndex < snake.Length - 1)
                    {
                        matrix[rowIndex][colIndex] = snake[snakeIndex];
                        snakeIndex++;
                    }
                    else if (snakeIndex == snake.Length - 1)
                    {
                        matrix[rowIndex][colIndex] = snake[snakeIndex];
                        snakeIndex = 0;
                    }

                }
                if (rowIndex > 0)
                {
                    matrix[rowIndex - 1] = new char[input[1]];
                    for (int colIndex = 0; colIndex < input[1]; colIndex++)
                    {
                        if (snakeIndex < snake.Length - 1)
                        {
                            matrix[rowIndex - 1][colIndex] = snake[snakeIndex];
                            snakeIndex++;
                        }
                        else if (snakeIndex == snake.Length - 1)
                        {
                            matrix[rowIndex - 1][colIndex] = snake[snakeIndex];
                            snakeIndex = 0;
                        }
                    }
                }
                else
                {
                    break;
                }

            }
        }
    }
}

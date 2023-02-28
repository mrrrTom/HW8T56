// Задача 56: Задайте прямоугольный двумерный массив. Напишите программу, которая будет находить строку с наименьшей суммой элементов.
// Например, задан массив:
// 1 4 7 2
// 5 9 2 3
// 8 4 2 4
// 5 2 6 7
// Программа считает сумму элементов в каждой строке и выдаёт номер строки с наименьшей суммой элементов: 1 строка

namespace HW56
{
    class ConsoleApp
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the array row orderer!");
            var arr = new ArrayBuilder(3, 4);
            Console.WriteLine("Here is you array:");
            Console.WriteLine(arr.ToString());
            Console.WriteLine($"{arr.GetMinRow()} row");
        }
    }

    public class ArrayBuilder
    {
        private double[,] _arr;
        private double[][] _rows;
        private int _minRow;
        private bool _isInitialized = false;

        public ArrayBuilder(int row, int col)
        {
            _minRow = 0;
            _rows = new double[row][];
            for (int i = 0; i < row; i++)
            {
                _rows[i] = new double[col];
            }

            _arr = InitializeRadomArray(row, col);
            _isInitialized = true;
        }

        public override string ToString()
        {
            return _arr.ToArrString();
        }

        public void SortRows()
        {
            if (_arr == null)
            {
                return;
            }
            
            for (int i = 0; i < _arr.GetLength(0); i++)
            {
                var row = _rows[i];
                row = row.OrderDescending().ToArray();
                _rows[i] = row;
                for (var j = 0; j < row.Length; j++)
                {
                    _arr[i, j] = row[j];
                }
            }
        }

        public string GetMinRow()
        {
            var result = string.Empty;
            if ( !_isInitialized)
            {
                return result;
            }

            return (_minRow + 1).ToString();
        }

        double[,] InitializeRadomArray(int row, int col)
        {
            var result = new double[row, col];
            var rnd = new Random();
            var minRowSumm = double.MaxValue;
            for (int i = 0; i < row; i++)
            {
                var rowSumm = default(double);
                for (int j = 0; j < col; j++)
                {
                    var signPow = rnd.Next(1, 3);
                    var tenPow = rnd.Next(0, 3);
                    var doubleValue = rnd.NextDouble();
                    var sign = ((double)Math.Pow(-1, signPow));
                    var tens = ((double)Math.Pow(10, tenPow));
                    var roundCount = rnd.Next(0, 3);
                    var arrInput =  Math.Round(doubleValue * sign * tens, roundCount);
                    rowSumm += arrInput;
                    AddValue(arrInput, i, j);
                    result[i, j] = arrInput;
                }

                if (rowSumm < minRowSumm)
                {
                    minRowSumm = rowSumm;
                    _minRow = i;
                }
            }

            return result;
        }

        void AddValue(double value, int row, int col)
        {
            _rows[row][col] = value;
        }
    }

    public static class ArrExtension
    {
        public static string ToArrString(this double[,] arr)
        {
            var result = string.Empty;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result += arr[i, j] + "\t";
                }

                result += "\n";
            }

            return result;
        }
    }
}
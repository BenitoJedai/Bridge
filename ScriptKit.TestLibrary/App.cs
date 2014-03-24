using ScriptKit.CLR.Html;
using System;

namespace TestProject 
{
    public static class App 
    {
        public static void CalculateMetrics(int[] numbers)
        {
            Console.info("Average: %d", App.Average(numbers));
            Console.info("Sum: %d", App.Sum(numbers));
            Console.info("Max: %d", App.Max(numbers));
            Console.info("Min: %d", App.Min(numbers));
        }

        public static float Average(int[] numbers)
        {
            float result = 0;
            int len = numbers.length;

            foreach (var number in numbers)
            {
                result += number;
            }

            return result / len;
        }

        public static int Sum(int[] numbers)
        {
            int result = 0;

            foreach (var number in numbers)
            {
                result += number;
            }

            return result;
        }

        public static int Max(int[] numbers)
        {
            int result = int.MIN_VALUE;

            foreach (var number in numbers)
            {
                if (number > result)
                {
                    result = number;
                }
            }

            return result;
        }

        public static int Min(int[] numbers)
        {
            int result = int.MAX_VALUE;

            foreach (var number in numbers)
            {
                if (number < result)
                {
                    result = number;
                }
            }

            return result;
        }
    }
}

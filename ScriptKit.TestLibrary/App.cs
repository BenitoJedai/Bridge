using ScriptKit.CLR.Html;
using System;

namespace TestProject 
{
    public class Person
    {
        public string Name { get; set; }
        //public DateTime DateOfBirth { get; set; }
    }

    public class Customer : Person
    {
        public Customer(string name)
        {
            this.Name = name;
        }

        public int CustomerId { get; set; }
    }

    public static class App 
    {
        public static void CalculateMetrics(int[] numbers)
        {
            //var customer = new Customer { Name = "Geoff" };

            var customer = new Customer("Geoff");

            customer.Name = "Geoff";

            customer.CustomerId = 1234;

            Console.Log(customer.Name);

            Date date = new Date();

            Console.Log(date.getDay());
            Console.Info("Max: %d", App.Max(numbers));
            Console.Info("Min: %d", App.Min(numbers));
            Console.Info("Average: %d", App.Average(numbers));
            Console.Info("Sum: %d", App.Sum(numbers));
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

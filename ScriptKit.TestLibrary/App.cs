using ScriptKit.CLR.Html;
using System;

namespace TestProject 
{
    //public class Class1
    //{
    //    [ScriptKit.CLR.Name("m1")]
    //    public virtual Class1 Method1()
    //    {
    //        Action a = this.Method2;
    //        a();
    //        return this;
    //    }

    //    [ScriptKit.CLR.Name("m2")]
    //    public void Method2()
    //    {
    //        this.Method1().Method2();
    //    }

    //    public Class1 Property1
    //    {
    //        get;
    //        set;
    //    }

    //    public Class1 Property2
    //    {
    //        get
    //        {
    //            return this.Property1.Method1();
    //        }
    //    }
    //}

    //public class Class2 : Class1
    //{
    //    public override Class1 Method1()
    //    {
    //        base.Method2();
    //        return this;
    //    } 
    //}
    
    public static class App 
    {
        public static void CalculateMetrics(int[] numbers)
        {
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

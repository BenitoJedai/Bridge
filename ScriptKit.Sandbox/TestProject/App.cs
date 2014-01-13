using System;

namespace TestProject 
{
    public class Class1
    {
        public int f1;
        public bool b1;
        public string s1;

        public Class1()
        {
            // comment here
            Func<string, string> fn = delegate(string s) 
            {
                return s;
            };
        }

        public static int Property0
        {
            get;
            set;
        }
        
        public int Property1
        {
            get;
            set;
        }

        public int Property2
        {
            get
            {
                return this.Property1++;
            }
            set
            {
                int iii = 0;
                this.Property1 = iii;
            }
        }

        public void Run()
        {
            int sum = this.Sum(5, 6);
        }

        public virtual int Sum(int x1, int x2)
        {
            return x1 + x2;
        }
    }

    public class Class2 : Class1
    {
        public override int Sum(int x1, int x2)
        {
            int i = Class1.Property0;
            Class1 c1 = new Class1();
            int i2 = c1.Property1;
            return base.Sum(x1, x2) + x2 + x1;
        }
    }

    public static class App 
    {
        static int i = 5;

        public static void Run() 
        {
            int sum = App.Sum(5, 6);
        }

        public static int Sum(int x1, int x2)
        {
            return x1 + x2;
        }
    }
}

using System;

namespace TestProject 
{
    public class ClassOne
    {
        public int f1;
        public bool b1;
        public string s1;

        public ClassOne()
        {
            // test comment
            Func<string, string> fn = delegate(string s) 
            {
                return s;
            };
        }

        public static int PropertyZero
        {
            get;
            set;
        }
        
        public int PropertyOne
        {
            get;
            set;
        }

        public int PropertyTwo
        {
            get
            {
                return this.PropertyOne++;
                // TODO: Getter is emitted with extra newline after end block
            }
            set
            {
                int iii = 0;
                this.PropertyOne = iii;
                // TODO: Setter is emitted with extra newline after end block
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

    public class ClassTwo : ClassOne
    {
        public override int Sum(int x1, int x2)
        {
            int i = ClassOne.PropertyZero;

            // TODO: this "new" instance is not getting emitted properly.
            // missing class name
            ClassOne c1 = new ClassOne();
            int i2 = c1.PropertyOne;

            return base.Sum(x1, x2) + x2 + x1;
        }

        /// TODO: implement DateTime type properties
        //public DateTime Now
        //{
        //    get
        //    {
        //        return new DateTime();
        //    }
        //}
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

        public static void DoSomething(int x1)
        {
            var temp = "test";

            if (x1 > 6)
            {
                // test comment
                x1 = 1;
            }

            var val = 6;

            App.Sum(App.i, val);

            var temp2 = "another string";
        }
    }
}

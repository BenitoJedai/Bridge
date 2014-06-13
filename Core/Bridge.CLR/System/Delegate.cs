using Bridge.CLR;

namespace System 
{
    [Ignore]
    [Name("Function")]
    public class Delegate 
    {
        public readonly int Length = 0;

        protected Delegate() 
        { 
        }

        public object Apply(object thisArg) 
        { 
            return null; 
        }

        public object Apply(object thisArg, Array args) 
        { 
            return null; 
        }

        public object Call(object thisArg, params object[] args) 
        { 
            return null; 
        }
        
        [Template("Bridge.fn.combine({0});")]
        public static Delegate Combine(Delegate a, Delegate b)
        {
            return null;
        }

        [Template("Bridge.fn.remove({0});")]
        public static Delegate Remove(Delegate source, Delegate value)
        {
            return null;
        }
    }

    [Ignore]
    [Name("Function")]
    public class MulticastDelegate : Delegate 
    {
        protected MulticastDelegate() { }
    }
}

namespace System 
{

    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Function")]
    public class Delegate 
    {
        public readonly int length = 0;

        protected Delegate() 
        { 
        }

        public object apply(object thisArg) 
        { 
            return null; 
        }

        public object apply(object thisArg, Array args) 
        { 
            return null; 
        }

        public object call(object thisArg, params object[] args) 
        { 
            return null; 
        }
    }

    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Function")]
    public class MulticastDelegate : Delegate 
    {
        protected MulticastDelegate() { }
    }
}

namespace System 
{

    [ScriptKit.Core.Ignore]
    [ScriptKit.Core.TypeName("Function")]
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

    [ScriptKit.Core.Ignore]
    [ScriptKit.Core.TypeName("Function")]
    public class MulticastDelegate : Delegate 
    {
        protected MulticastDelegate() { }
    }
    
    public delegate void Action();
    public delegate void Action<A>(A a);
    public delegate void Action<A, B>(A a, B b);
    public delegate void Action<A, B, C>(A a, B b, C c);
    public delegate void Action<A, B, C, D>(A a, B b, C c, D d);
    public delegate void Action<A, B, C, D, E>(A a, B b, C c, D d, E e);
    public delegate void Action<A, B, C, D, E, F>(A a, B b, C c, D d, E e, F f);
    
    public delegate TResult Func<TResult>();
    public delegate TResult Func<A, TResult>(A a);
    public delegate TResult Func<A, B, TResult>(A a, B b);
    public delegate TResult Func<A, B, C, TResult>(A a, B b, C c);
    public delegate TResult Func<A, B, C, D, TResult>(A a, B b, C c, D d);
    public delegate TResult Func<A, B, C, D, E, TResult>(A a, B b, C c, D d, E e);
    public delegate TResult Func<A, B, C, D, E, F, TResult>(A a, B b, C c, D d, E e, F f);
}

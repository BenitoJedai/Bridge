using Bridge.CLR;

namespace System
{
    [Ignore]
    [Name("Object")]
    [Constructor("{ }")]
    public class Object
    {
        public object this[string name] 
        { 
            get 
            { 
                return null; 
            } 
            set 
            { 
            } 
        }

        public T GetProperty<T>(string name)
        {
            return default(T);
        }

        [Template("{this}")]
        public readonly dynamic Instance;

        public virtual string ToString() 
        { 
            return null; 
        }

        public virtual string ToLocaleString() 
        { 
            return null; 
        }

        public virtual object ValueOf() 
        { 
            return null; 
        }

        public bool HasOwnProperty(object v) 
        { 
            return false; 
        }

        public bool IsPrototypeOf(object v) 
        { 
            return false; 
        }

        public bool PropertyIsEnumerable(object v) 
        { 
            return false; 
        }        
    }

    [Ignore]
    public static class ObjectExtensions
    {
        [Template("Bridge.fn.call({obj}, {name}, {args})")]
        public static void CallFn(this object obj, string name, params object[] args)
        {
        }

        [Template("{0}")]
        public static T As<T>(this object obj)
        {
            return default(T);
        }

        [Template("Bridge.cast({obj}, {T})")]
        public static T Cast<T>(this object obj)
        {
            return default(T);
        }
    }
}
using Bridge.Foundation;

namespace System
{
    [Ignore]
    [Name("Object")]
    [Constructor("{ }")]
    public class Object
    {
        public virtual object this[string name] 
        { 
            get 
            { 
                return null; 
            } 
            set 
            { 
            } 
        }

        //[Template("{this}")]
        //public readonly dynamic This;

        public readonly Type Constructor;

        [Template("{this}")]
        public virtual dynamic ToDynamic()
        {
            return null;
        }

        [Template("$$name")]
        public virtual string GetClassName()
        {
            return null;
        }

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

        public virtual bool HasOwnProperty(object v) 
        { 
            return false; 
        }

        public virtual bool IsPrototypeOf(object v) 
        { 
            return false; 
        }

        public virtual bool PropertyIsEnumerable(object v) 
        { 
            return false; 
        }

        [Template("Bridge.getType({this})")]
        public Type GetType()
        {
            return null;
        }

        [Template("{a} === {b}")]
        public static bool ReferenceEquals(object a, object b)
        {
            return false;
        }

        [Template("Bridge.equals({this}, {o})")]
        public virtual bool Equals(object o)
        {
            return false;
        }        

        [Template("Bridge.equals({a}, {b})")]
        public static bool Equals(object a, object b)
        {
            return false;
        }

        [Template("Bridge.getHashCode({this})")]
        public virtual int GetHashCode()
        {
            return 0;
        }

        public static string[] Keys(object obj)
        {
            return null;
        }

        public static string[] GetOwnPropertyNames(object obj)
        {
            return null;
        }
    }

    [Ignore]
    public static class ObjectExtensions
    {
        [Template("{obj}[{name}] = {args}")]
        public static void AddMember(this object obj, string name, object member)
        {
        }

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

        [Template("Bridge.as({obj}, {T})")]
        public static T TryCast<T>(this object obj) where T:class
        {
            return default(T);
        }

        [Template("Bridge.is({obj}, {T})")]
        public static bool Is<T>(this object obj)
        {
            return false;
        }
    }
}
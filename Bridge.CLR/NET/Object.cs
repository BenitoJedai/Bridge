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

        [Bridge.CLR.Inline("$callfn({0}, {1})")]
        public void CallFn(string name, params object[] args)
        {
        }
    }
}
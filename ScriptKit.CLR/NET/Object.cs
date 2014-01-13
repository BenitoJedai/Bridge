namespace System
{
    [ScriptKit.Core.Ignore]
    [ScriptKit.Core.TypeName("Object")]
    [ScriptKit.Core.Constructor("{ }")]
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

        public virtual string toString() 
        { 
            return null; 
        }

        public virtual string toLocaleString() 
        { 
            return null; 
        }

        public virtual object valueOf() 
        { 
            return null; 
        }

        public bool hasOwnProperty(object v) 
        { 
            return false; 
        }

        public bool isPrototypeOf(object v) 
        { 
            return false; 
        }

        public bool propertyIsEnumerable(object v) 
        { 
            return false; 
        }
    }
}
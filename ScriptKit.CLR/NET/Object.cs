namespace System
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.TypeName("Object")]
    [ScriptKit.CLR.Constructor("{ }")]
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
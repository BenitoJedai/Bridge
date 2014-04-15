namespace System 
{
    [ScriptKit.CLR.Ignore]
    [ScriptKit.CLR.Name("Array")]
    public sealed class Array 
    {
        public readonly int Length = 0;

        private Array() 
        { 
        }

        public object this[int index]
        {
            get
            {
                return null;
            }
            set
            {
            }
        } 

        public Array Concat(params object[] items) 
        { 
            return null; 
        }

        public string Join(string separator) 
        { 
            return null; 
        }

        public object Pop() 
        { 
            return null; 
        }

        public void Push(params object[] items) 
        {
        }

        public void Reverse() 
        { 
        }

        public object Shift() 
        { 
            return null; 
        }

        public Array Slice(int start) 
        { 
            return null; 
        }

        public Array Slice(int start, int end) 
        { 
            return null; 
        }

        public void Sort() 
        { 
        }

        public void Sort(object compareFunction) 
        { 
        }

        public Array Splice(int start, int deleteCount, params object[] newItems) 
        { 
            return null; 
        }

        public void Unshift(params object[] items) 
        { 
        }
    }
}
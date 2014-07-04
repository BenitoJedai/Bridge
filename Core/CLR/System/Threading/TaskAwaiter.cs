using System;
using System.Runtime.CompilerServices;
using Bridge.CLR;

namespace System.Threading.Tasks 
{
	[Ignore]
	[Name("Bridge.Task")]
	public class TaskAwaiter : INotifyCompletion 
    {
		internal TaskAwaiter() 
        {
        }

        public virtual bool IsCompleted
        {
            get
            {
                return false;
            }
        }

		public virtual void OnCompleted(Action continuation) 
        {
        }

		public virtual void GetResult() 
        {
        }
	}

	[Ignore]
	[Name("Bridge.Task")]
	public class TaskAwaiter<TResult> : INotifyCompletion 
    {
		internal TaskAwaiter() 
        {
        }

        public virtual bool IsCompleted
        {
            get
            {
                return false;
            }
        }

		public virtual void OnCompleted(Action continuation) 
        {
        }

		public virtual TResult GetResult() 
        { 
            return default(TResult); 
        }
	}
}

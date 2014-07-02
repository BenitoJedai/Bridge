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

        public bool IsCompleted
        {
            get
            {
                return false;
            }
        }

		public void OnCompleted(Action continuation) 
        {
        }

		public void GetResult() 
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

        public bool IsCompleted
        {
            get
            {
                return false;
            }
        }

		public void OnCompleted(Action continuation) 
        {
        }

		public TResult GetResult() 
        { 
            return default(TResult); 
        }
	}
}

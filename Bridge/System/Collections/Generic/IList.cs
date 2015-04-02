using System;
using System.Collections.Generic;
using Bridge;

namespace System.Collections.Generic 
{
	[Name("Bridge.IList")]
	public interface IList<T> : ICollection<T> 
    {
		T this[int index] 
        {
            [Template("get({0})")]
            get;
            [Template("set({0})")]
            set;
		}

		int IndexOf(T item);
		void Insert(int index, T item);
		void RemoveAt(int index);
	}
}

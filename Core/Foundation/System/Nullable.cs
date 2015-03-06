using Bridge.Foundation;
namespace System 
{
	[Ignore]
    [Constructor("")]
	public struct Nullable<T> where T : struct 
    {
		[Template("{0}")]
		public Nullable(T value) 
        {
		}
        
        public bool HasValue
        {
            [Template("Bridge.nullable.hasValue({this})")]
            get
            {
                return false;
            }
        }
        
        public T Value
        {
            [Template("Bridge.nullable.getValue({this})")]
            get
            {
                return default(T);
            }
        }

        [Template("Bridge.nullable.getValueOrDefault({this})")]
		public T GetValueOrDefault() 
        {
			return default(T);
		}

        [Template("Bridge.nullable.getValueOrDefault({this}, {0})")]
		public T GetValueOrDefault(T defaultValue) 
        {
			return default(T);
		}

		public static implicit operator T?(T value) 
        {
			return null;
		}

        [Template("Bridge.nullable.getValue({this})")]
		public static explicit operator T(T? value) 
        {
			return default(T);
		}
	}
}

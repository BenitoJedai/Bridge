using Bridge.CLR;
using System.Collections;

namespace Bridge.jQuery2
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Constructor("$")]
    [Name("$")]
    public partial class jQuery : IEnumerable
    {
        [Template("$(this)")]
        public static readonly jQuery This;

        [Template("$(window)")]
        public static readonly jQuery Window;

        [Template("$(document)")]
        public static readonly jQuery Document;

        [Template("$()")]
        public static readonly jQuery Empty;

        IEnumerator IEnumerable.GetEnumerator() 
        { 
            return null; 
        }
    }
}

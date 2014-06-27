using Bridge.CLR;

namespace Bridge.Html5
{
    [Ignore]
    [Name("BarProp")]
    public class BarProp
    {
        private BarProp()
        {
        }

        /// <summary>
        /// False to hide location bar
        /// </summary>
        public static bool Visible;
    }
}

using Bridge.CLR;
using Bridge.CLR.Html;
using System;

namespace Moment
{
    [Namespace(false)]
    [Name("moment")]
    [Constructor("moment")]
    public class Moment
    {
        public Moment()
        {
        }

        public Moment AddYears(int value)
        {
            return new Moment();
        }
    }
}
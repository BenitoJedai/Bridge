using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [Name("Bridge.AspectAttribute")]
    public abstract class MulticastAspectAttribute : AspectAttribute
    {
        public bool Exclude
        {
            get;
            set;
        }

        public bool Inheritance
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }

        public bool Replace
        {
            get;
            set;
        }

        public string TargetMembers
        {
            get;
            set;
        }

        public string TargetParameters
        {
            get;
            set;
        }

        public string TargetTypes
        {
            get;
            set;
        }
    }
}
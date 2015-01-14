using System;

namespace Bridge.CLR
{
    /// <summary>
    /// 
    /// </summary>
    [Ignore]
    [AttributeUsage(AttributeTargets.Class)]
    public class MulticastOptionsAttribute : Attribute
    {
        public bool Exclude
        {
            get;
            set;
        }

        public bool Multiple
        {
            get;
            set;
        }

        public int Inheritance
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

        public int TargetMembersAttributes
        {
            get;
            set;
        }

        public string TargetMembers
        {
            get;
            set;
        }

        public int TargetParametersAttributes
        {
            get;
            set;
        }

        public string TargetParameters
        {
            get;
            set;
        }

        public int TargetTypesAttributes
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
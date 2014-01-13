using Mono.Cecil;
using System.Collections.Generic;

namespace ScriptKit.NET
{
    public partial class Translator
    {
        public Validator Validator
        {
            get;
            private set;
        }

        public string CLRLocation
        {
            get;
            set;
        }
        
        public string Location 
        { 
            get; 
            protected set; 
        }

        public string AssemblyLocation 
        { 
            get; 
            protected set; 
        }

        private string msbuildVersion="3.5";

        public string MSBuildVersion
        {
            get
            {
                return this.msbuildVersion;
            }
            set
            {
                this.msbuildVersion = value;
            }
        }

        public IList<string> SourceFiles 
        { 
            get; 
            protected set; 
        }

        private bool rebuild = true;

        public bool Rebuild 
        {
            get
            {
                return this.rebuild;
            }
            set
            {
                this.rebuild = value;
            }
        }

        protected Dictionary<string, TypeDefinition> TypeDefinitions
        {
            get;
            set;
        }

        public List<TypeInfo> Types 
        { 
            get; 
            protected set; 
        }
    }
}

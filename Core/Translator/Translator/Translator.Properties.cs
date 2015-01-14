using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public partial class Translator
    {
        public AssemblyInfo AssemblyInfo
        {
            get;
            set;
        }

        public AssemblyDefinition AssemblyDefinition
        {
            get;
            set;
        }

        public Action<string, string> Log
        {
            get;
            set;
        }

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

        public string Configuration
        {
            get;
            set;
        }

        private bool changeCase = true;

        public bool ChangeCase
        {
            get
            {
                return this.changeCase;
            }
            set
            {
                this.changeCase = value;
            }
        }

        private string msbuildVersion = "4.0.30319";

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

        public Dictionary<string, TypeInfo> TypeInfoDefinitions
        {
            get;
            set;
        }

        public List<TypeInfo> Types 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, string> Outputs
        {
            get;
            protected set;
        }
    }
}

using Bridge.Contract;
using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public partial class Translator
    {
        public IAssemblyInfo AssemblyInfo
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

        public IValidator Validator
        {
            get;
            private set;
        }

        public string BridgeLocation
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

        public Dictionary<string, ITypeInfo> TypeInfoDefinitions
        {
            get;
            set;
        }

        public List<ITypeInfo> Types 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, string> Outputs
        {
            get;
            protected set;
        }

        public IPlugins Plugins
        {
            get;
            set;
        }
    }
}

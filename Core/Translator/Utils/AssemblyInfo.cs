using Bridge.Contract;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class AssemblyInfo : IAssemblyInfo
    {
        public const string DEFAULT_FILENAME = "---";
        
        public AssemblyInfo()
        {
            this.Dependencies = new List<IModuleDependency>();                        
        }

        public string FileName
        {
            get;
            set;
        }

        public string OutputDir
        {
            get;
            set;
        }

        private TypesSplit filesHierrarchy = TypesSplit.None;
        public TypesSplit FilesHierrarchy
        {
            get
            {
                return this.filesHierrarchy;
            }
            set
            {
                this.filesHierrarchy = value;
            }
        }

        public int StartIndexInName
        {
            get;
            set;
        }

        public string Module
        {
            get;
            set;
        }

        public List<IModuleDependency> Dependencies
        {
            get;
            set;
        }


        public string BeforeEvent
        {
            get;
            set;
        }

        public string AfterEvent
        {
            get;
            set;
        }
    }
}

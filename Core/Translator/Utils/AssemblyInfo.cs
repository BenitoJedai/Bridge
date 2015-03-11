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

        private TypesSplit filesHierarchy = TypesSplit.None;
        public TypesSplit FilesHierarchy
        {
            get
            {
                return this.filesHierarchy;
            }
            set
            {
                this.filesHierarchy = value;
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


        public string BeforeBuild
        {
            get;
            set;
        }

        public string AfterBuild
        {
            get;
            set;
        }
    }
}

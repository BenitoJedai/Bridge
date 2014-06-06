using System.Collections.Generic;

namespace Bridge.NET
{
    public enum TypesSplit
    {
        None = 0,
        ByFullName = 1,
        ByName = 2,
        ByModule = 3,
        ByNamespace = 4
    }

    public class AssemblyInfo
    {
        public const string DEFAULT_FILENAME = "---";
        
        public AssemblyInfo()
        {
            this.Dependencies = new List<ModuleDependency>();
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

        public List<ModuleDependency> Dependencies
        {
            get;
            set;
        }
    }
}

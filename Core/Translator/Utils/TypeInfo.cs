using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;

namespace Bridge.NET
{
    public class TypeInfo : ITypeInfo
    {
        public TypeInfo() 
        {
            this.StaticFields = new Dictionary<string, Expression>();
            this.Consts = new Dictionary<string, Expression>();
            this.InstanceFields = new Dictionary<string, Expression>();
            this.StaticMethods = new Dictionary<string, List<MethodDeclaration>>();
            this.InstanceMethods = new Dictionary<string, List<MethodDeclaration>>();
            this.StaticProperties = new Dictionary<string, EntityDeclaration>();
            this.InstanceProperties = new Dictionary<string, EntityDeclaration>();
            this.FieldsDeclarations = new Dictionary<string, FieldDeclaration>();
            this.Events = new List<EventDeclaration>();
            this.StaticEvents = new List<EventDeclaration>();
            this.Dependencies = new List<IPluginDependency>();
            this.Ctors = new List<ConstructorDeclaration>();
            this.Operators = new Dictionary<OperatorType, List<OperatorDeclaration>>();
            this.AutoProperties = new List<string>();
        }

        public Dictionary<OperatorType, List<OperatorDeclaration>> Operators
        {
            get;
            protected set;
        }

        public TypeDeclaration TypeDeclaration
        {
            get;
            set;
        }

        public List<EventDeclaration> StaticEvents
        {
            get;
            set;
        }

        public List<EventDeclaration> Events
        {
            get;
            set;
        }

        public bool IsStatic 
        { 
            get; 
            set; 
        }

        public ClassType ClassType 
        { 
            get; 
            set; 
        }
        
        public string Namespace 
        { 
            get; 
            set; 
        }

        public string Name 
        { 
            get; 
            set; 
        }

        public string ParentName
        {
            get
            {
                if (this.ParentType == null)
                {
                    return this.Name;
                }

                return this.ParentType.Name + "." + this.Name;
            }
        }

        public string ParentGenericName
        {
            get
            {
                if (this.ParentType == null)
                {
                    return this.GenericName;
                }

                return (this.ParentType.GenericName ?? this.ParentType.Name) + "." + this.GenericName;
            }
        }

        public HashSet<string> Usings 
        { 
            get; 
            set; 
        }
        
        public List<ConstructorDeclaration> Ctors
        { 
            get; 
            set; 
        }

        public ConstructorDeclaration StaticCtor
        {
            get;
            set;
        }

        public Dictionary<string, FieldDeclaration> FieldsDeclarations
        {
            get;
            protected set;
        }

        public Dictionary<string, Expression> StaticFields 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, Expression> Consts
        {
            get;
            protected set;
        }

        public Dictionary<string, Expression> InstanceFields 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, List<MethodDeclaration>> StaticMethods 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, List<MethodDeclaration>> InstanceMethods 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, EntityDeclaration> StaticProperties
        {
            get;
            protected set;
        }

        public Dictionary<string, EntityDeclaration> InstanceProperties
        {
            get;
            protected set;
        }

        public bool HasStatic
        {
            get
            {
                return this.StaticFields.Count > 0 
                       || this.StaticMethods.Count > 0 
                       || this.StaticProperties.Count > 0
                       || this.Consts.Count > 0
                       || this.StaticEvents.Count > 0
                       || this.StaticCtor != null
                       || this.Operators.Count > 0;
            }
        }

        public bool HasInstantiable
        {
            get
            {
                return this.InstanceFields.Count > 0 
                       || this.InstanceMethods.Count > 0
                       || this.InstanceProperties.Count > 0 
                       || this.Events.Count > 0
                       || this.Ctors.Count > 0;
            }
        }

        public string FullName
        {
            get 
            {
                if (String.IsNullOrEmpty(this.Namespace))
                {
                    return this.ParentName;
                }
                return this.Namespace + "." + this.ParentName;
            }
        }

        public string GenericFullName
        {
            get
            {
                if (String.IsNullOrEmpty(this.Namespace))
                {
                    return this.ParentGenericName;
                }
                return this.Namespace + "." + this.ParentGenericName;
            }
        }

        private int enumValue = 0;

        public virtual int LastEnumValue
        {
            get
            {
                return enumValue++;
            }
        }

        public bool IsEnum 
        { 
            get; 
            set; 
        }

        public string GenericName 
        { 
            get; set; 
        }

        public string FileName
        {
            get;
            set;
        }

        public string Module
        {
            get;
            set;
        }

        public List<IPluginDependency> Dependencies
        {
            get;
            set;
        }

        public ITypeInfo ParentType
        {
            get;
            set;
        }


        public List<string> AutoProperties
        {
            get;
            set;
        }
    }
}

using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;

namespace ScriptKit.NET
{
    public class TypeInfo
    {
        public TypeInfo() 
        {
            this.StaticFields = new Dictionary<string, Expression>();
            this.Consts = new Dictionary<string, Expression>();
            this.InstanceFields = new Dictionary<string, Expression>();
            this.StaticMethods = new Dictionary<string, MethodDeclaration>();
            this.InstanceMethods = new Dictionary<string, MethodDeclaration>();
            this.StaticProperties = new Dictionary<string, PropertyDeclaration>();
            this.InstanceProperties = new Dictionary<string, PropertyDeclaration>();
            this.FieldsDeclarations = new Dictionary<string, FieldDeclaration>();
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

        public HashSet<string> Usings 
        { 
            get; 
            set; 
        }
        
        public ConstructorDeclaration Ctor 
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

        public Dictionary<string, MethodDeclaration> StaticMethods 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, MethodDeclaration> InstanceMethods 
        { 
            get; 
            protected set; 
        }

        public Dictionary<string, PropertyDeclaration> StaticProperties
        {
            get;
            protected set;
        }

        public Dictionary<string, PropertyDeclaration> InstanceProperties
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
                       || this.StaticCtor != null;
            }
        }

        public bool HasInstantiable
        {
            get
            {
                return this.InstanceFields.Count > 0 
                       || this.InstanceMethods.Count > 0
                       || this.InstanceProperties.Count > 0 
                       || this.Ctor != null;
            }
        }

        public string FullName
        {
            get 
            {
                if (String.IsNullOrEmpty(this.Namespace))
                {
                    return this.Name;
                }
                return this.Namespace + "." + this.Name;
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

        public string GenericName { get; set; }
    }
}

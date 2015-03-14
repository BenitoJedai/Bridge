using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;

namespace Bridge.Contract
{
    public interface ITypeInfo
    {
        TypeDeclaration TypeDeclaration
        {
            get;
            set;
        }

        List<EventDeclaration> StaticEvents
        {
            get;
            set;
        }

        List<EventDeclaration> Events
        {
            get;
            set;
        }

        bool IsStatic 
        { 
            get; 
            set; 
        }

        ClassType ClassType 
        { 
            get; 
            set; 
        }
        
        string Namespace 
        { 
            get; 
            set; 
        }

        string Name 
        { 
            get; 
            set; 
        }

        string ParentName
        {
            get;
        }

        string ParentGenericName
        {
            get;
        }

        HashSet<string> Usings 
        { 
            get; 
            set; 
        }
        
        List<ConstructorDeclaration> Ctors
        { 
            get; 
            set; 
        }

        ConstructorDeclaration StaticCtor
        {
            get;
            set;
        }

        Dictionary<string, FieldDeclaration> FieldsDeclarations
        {
            get;
        }

        List<string> AutoProperties
        {
            get;
        }

        Dictionary<string, Expression> StaticFields 
        { 
            get; 
        }

        Dictionary<string, Expression> Consts
        {
            get;
        }

        Dictionary<string, Expression> InstanceFields 
        { 
            get; 
        }

        Dictionary<OperatorType, List<OperatorDeclaration>> Operators
        {
            get;
        }

        Dictionary<string, List<MethodDeclaration>> StaticMethods 
        { 
            get; 
        }

        Dictionary<string, List<MethodDeclaration>> InstanceMethods 
        { 
            get; 
        }

        Dictionary<string, EntityDeclaration> StaticProperties
        {
            get;
        }

        Dictionary<string, EntityDeclaration> InstanceProperties
        {
            get;
        }

        bool HasStatic
        {
            get;
        }

        bool HasInstantiable
        {
            get;
        }

        string FullName
        {
            get;
        }

        string GenericFullName
        {
            get;
        }

        int LastEnumValue
        {
            get;
        }

        bool IsEnum 
        { 
            get; 
            set; 
        }

        string GenericName 
        { 
            get; 
            set; 
        }

        string FileName
        {
            get;
            set;
        }

        string Module
        {
            get;
            set;
        }

        List<IPluginDependency> Dependencies
        {
            get;
            set;
        }
        
        ITypeInfo ParentType
        {
            get;
            set;
        }
    }
}

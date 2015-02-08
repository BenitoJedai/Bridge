using System;
namespace Bridge.Plugin
{
    public interface IWriterInfo
    {
        bool Comma { get; set; }
        bool IsNewLine { get; set; }
        int Level { get; set; }
        System.Text.StringBuilder Output { get; set; }
    }
}

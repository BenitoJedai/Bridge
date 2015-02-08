using System;
using System.Text;
namespace Bridge.Plugin
{
    public interface IAsyncStep
    {
        int FromTaskNumber { get; set; }
        int JumpToStep { get; set; }
        StringBuilder Output { get; set; }
        int Step { get; set; }
    }
}

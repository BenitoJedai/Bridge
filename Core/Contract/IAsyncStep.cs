using System;
using System.Text;
namespace Bridge.Contract
{
    public interface IAsyncStep
    {
        int FromTaskNumber { get; set; }
        int JumpToStep { get; set; }
        StringBuilder Output { get; set; }
        int Step { get; set; }
    }
}

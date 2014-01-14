using System;

namespace TestProject 
{
    public class ClassThree
    {
        public bool IsSet { get; set; }

        private string msg = "Hello World";

        public string Msg
        {
            get
            {
                return this.msg;
            }
        }
    }
}

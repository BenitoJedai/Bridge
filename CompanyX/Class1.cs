using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyX
{
    public class Person
    {
        public string Name { get; set; }
        //public DateTime DateOfBirth { get; set; }
    }

    public class Customer : Person
    {
        public Customer(string name)
        {
            this.Name = name;
        }

        public int CustomerId { get; set; }
    }
}

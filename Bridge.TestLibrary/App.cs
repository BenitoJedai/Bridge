using Bridge.CLR;
using Bridge.CLR.Html;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TestProject
{    
    public partial class Class1
    {
        public static void Method1(string s = "")
        {
            Console.Log("%d", 1);
        }

        public static void Method1(int i, string s = "")
        {
        }

        public static void Method1(params string[] args)
        {
            var l = args.Length;
        }

        public static void Method1(int i, params string[] args)
        {
            var l = args.Length;
        }

        public static int CalculateBMI(int weight, int height)
        {
            return (weight * 703) / (height * height);
        }
        
        public static void Start()
        {
            Method1();
            Method1(0);
            Method1("str");
            Method1("1", "2", "3", "4");
            Method1(0, "str");
            Method1(0, "1", "2", "3", "4");

            CalculateBMI(height: 64, weight: 123);
        }
    }
}
using Bridge.Html5;
using Bridge.CLR;
using System;

namespace CompanyX
{
    // [FileName(filename)]
    // [Namespace] // exclude namespace from JavaScript filename
    // [Module] // define a class as an AMD
    // [assembly:FilesHierrarchy(TypesSplit.ByModule)]
    // [assembly: OutputDir] // define output directory for .js files

    // partial classes supported
    // remove unimplemented partial Methods from .js. Same functionality as C# CLR

    [ObjectLiteral]
    public class Company
    {
        public string Name { get; set; }
    }

    public static class App
    {
        public static string Name = "Geoff";

        public static void LoadInit()
        {
            Console.Log("LoadInit");

            var div = Document.GetElementById("test");
            div.InnerHTML = "Hello World";
            div.ClassName = "box";
            div.Style.Height = "50px";
            div.Style.Width = "150px";
            div.Style.BorderRadius = "10px";
            div.Style.BoxShadow = "2px 2px 2px #808080";

            Document.Body.OnLoad += delegate
            {
                //var div2 = new Element();
                //div2.ClassName = "box";
                //div2.Style.Height = "100px";
                //div2.Style.Width = "100px";

                //Document.Body.AppendChild(div2);

                Console.Log("+=OnLoad");
            };

            var input = Document.GetElementById("input1") as InputElement;
            input.Value = "test2";

            var div2 = new Element { ClassName = "box" };

            div2.AppendChild(input);
            div.AppendChild(div2);
            
            //input.Value = "MyValue";
        }

        public static void Start()
        {
            Document.AddEventListener("DOMContentLoaded", LoadInit, false);

            //Window.Inline<string>("console.log('Window.Inline');");

            var temp1 = "Hello World";
            
            /*@
                * console.log('temp1', temp1);
                * 
                * var temp2 = 'Hello World2';
                * 
                * console.log('temp2', temp2);            
                @*/

            /*@
                console.log('temp1', temp1);
            
                var temp2 = 'Hello World2';
            
                console.log('temp2', temp2);            
                @*/

            var test = "test";
            
            /**
             * console.log('temp1', temp1);
             * 
             * var temp2 = 'Hello World2";
             * 
             * console.log('temp2', temp2);
             */
        
            //window[ addEventListener ? 'addEventListener' : 'attachEvent' ]( addEventListener ? 'load' : 'onload', load )

            Console.Log("Default DateTime", new DateTime().DateData);

            var date1 = new DateTime();
            var date2 = new DateTime(2000, 11, 05);

            Console.Log("date1", date1.DateData);
            Console.Log("date2", date2.DateData);

            var person = new Person (Name);

            person.DoSomething(person);
            person.DoSomething("Hello Person");
            person.DoSomething("Person.Name", person);

            var company = new Company { Name = "Object.NET" };

            //company.DoSomething();

            //Person.DoSomething(person);

            // Initalize int array fails
            var items = new int[6] { 1, 1, 2, 3, 5, 8 };

            Console.Log(items[5]);

            Console.Log(DateTime.Today.DayOfYear);

            var d1 = DateTime.Now;
            var d2 = d1.Clone().ClearTime();

            Console.Log("DateTime", d1.Hour);

            Console.Log("d2.ClearTime", d2.Hour);
            Console.Log("d2.ResetTime", d2.ResetTime().Hour);
            Console.Log(d2.Minute);

            var config = new DateTimeConfig { 
                Year = 1973,
                Month = 2,
                Day = 20,
                Hour = 5,
                Minute = 45,
                Second = 18,
                Millisecond = 333
            };

            Console.Log("Year", config.Year);
            Console.Log(d2.Set(config));
            Console.Log(d2.Minute);
            Console.Log(d2.DateData);

            var config2 = new DateTimeConfig {
                Year = 2,
                //Month = 2,
                //Day = 2,
                //Hour = 2,
                //Minute = 2,
                //Second = 2,
                //Millisecond = 2
            };

            Console.Log(d2.Add(config2).DateData);
        }
    }
}
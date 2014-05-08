using Bridge.CLR.Html;
using Bridge.CLR;

namespace CompanyX
{
    public static class App
    {
        public static string Name = "Geoff";

        public static void Start()
        {
            var person = new Person { Name = Name };

            // after compile, see body of doSomething function
            // The person.Name call should compile to person.getName()

            // doSomething: function (person) {
            //     console.log(person.name);
            //     // should be...
            //     // console.log(person.getName());
            // }

            Person.DoSomething(person);


            // Initalize int array fails
            var items = new int[6] { 1, 1, 2, 3, 5, 8 };

            Console.Log(items[5]);

            Console.Log(DateTime.Today.DayOfYear);
        }
    }
}
Bridge.Class.extend('CompanyX.App', {
    $statics: {
        $init: function () {
            this.$name = "Geoff";
        },
        loadInit: function () {
            console.log("LoadInit");
            var div = document.getElementById("test");
            div.innerHTML = "Hello World";
            div.className = "box";
            div.style.height = "50px";
            div.style.width = "150px";
            div.style.borderRadius = "10px";
            div.style.boxShadow = "2px 2px 2px #808080";
            document.body.onload = Bridge.fn.combine(document.body.onload, function () {
                /* var div2 = new Element(); */
                /* div2.ClassName = "box"; */
                /* div2.Style.Height = "100px"; */
                /* div2.Style.Width = "100px"; */
                /* Document.Body.AppendChild(div2); */
                console.log("+=OnLoad");
            });
            var input = Bridge.as(document.getElementById("input1"), HTMLInputElement);
            input.value = "test2";
            /* input.Value = "MyValue"; */
        },
        start: function () {
            document.addEventListener("DOMContentLoaded", Bridge.fn.bind(CompanyX.App, CompanyX.App.loadInit), false);
            /* Window.Inline<string>("console.log('Window.Inline');"); */
            var temp1 = "Hello World";
            
                console.log('temp1', temp1);
                
                var temp2 = 'Hello World2';
                
                console.log('temp2', temp2);            
                
            
                console.log('temp1', temp1);
            
                var temp2 = 'Hello World2';
            
                console.log('temp2', temp2);            
                
            var test = "test";
            /* window[ addEventListener ? 'addEventListener' : 'attachEvent' ]( addEventListener ? 'load' : 'onload', load ) */
            console.log("Default DateTime", new Bridge.DateTime("$init").dateData);
            var date1 = new Bridge.DateTime("$init");
            var date2 = new Bridge.DateTime("$init$Int32$Int32$Int32", 2000, 11, 5);
            console.log("date1", date1.dateData);
            console.log("date2", date2.dateData);
            var person = new CompanyX.Person("$init$String", CompanyX.App.$name);
            person.doSomething$Person(person);
            person.doSomething$String("Hello Person");
            person.doSomething$String$Person("Person.Name", person);
            var company = { name: "Object.NET" };
            /* company.DoSomething(); */
            /* Person.DoSomething(person); */
            /*  Initalize int array fails */
            var items = [1, 1, 2, 3, 5, 8];
            console.log(items[5]);
            console.log(Bridge.DateTime.getToday().getDayOfYear());
            var d1 = Bridge.DateTime.getNow();
            var d2 = Bridge.DateTime.UX.clearTime(Bridge.DateTime.UX.clone(d1));
            console.log("DateTime", d1.getHour());
            console.log("d2.ClearTime", d2.getHour());
            console.log("d2.ResetTime", Bridge.DateTime.UX.resetTime(d2).getHour());
            console.log(d2.getMinute());
            var config = { year: 1973, month: 2, day: 20, hour: 5, minute: 45, second: 18, millisecond: 333 };
            console.log("Year", config.year);
            console.log(Bridge.DateTime.UX.set(d2, config));
            console.log(d2.getMinute());
            console.log(d2.dateData);
            var config2 = { year: 2 };
            console.log(Bridge.DateTime.UX.add(d2, config2).dateData);
        }
    }
});

Bridge.Class.extend('CompanyX.Person', {
    $multipleCtors: true,
    $ctorDetector: function () {
        if (arguments.length == 0) {
            this.$init.apply(this, arguments);
        }
        else if (arguments.length == 1) {
            if (Bridge.is(arguments[0], String)) {
                this.$init$String.apply(this, arguments);
            }
        }
    },
    $init: function () {
        this.name = null;

    },
    $init$String: function (name) {
        this.name = null;

        this.setName(name);
    },
    getName: function () {
        return this.name;
    },
    setName: function (value) {
        this.name = value;
    },
    doSomething$Person: function (person) {
        console.log("DoSomething1", person.getName());
    },
    doSomething$String: function (message) {
        console.log("DoSomething2", message);
    },
    doSomething$String$Person: function (title, person) {
        console.log(title, person.getName());
    },
    doSomething: function () {
        if (arguments.length == 1) {
            if (Bridge.is(arguments[0], CompanyX.Person)) {
                this.doSomething$Person.apply(this, arguments);
            }
            else if (Bridge.is(arguments[0], String)) {
                this.doSomething$String.apply(this, arguments);
            }
        }
        else if (arguments.length == 2) {
            if (Bridge.is(arguments[0], String) && Bridge.is(arguments[1], CompanyX.Person)) {
                this.doSomething$String$Person.apply(this, arguments);
            }
        }
    }
});

Bridge.Class.extend('CompanyX.Customer', {
    $extend: [CompanyX.Person],
    $init: function () {
        this.city = null;
        this.companyName = null;

        this.base();
    },
    getCompanyName: function () {
        return this.companyName;
    },
    setCompanyName: function (value) {
        this.companyName = value;
    },
    getCity: function () {
        return this.city;
    },
    setCity: function (value) {
        this.city = value;
    }
});



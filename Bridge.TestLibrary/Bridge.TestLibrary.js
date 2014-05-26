Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            var dict = new Bridge.Dictionary();
            dict.add("test", dict.get("new"));
            var date1 = new Bridge.DateTime("$init");
            var date2 = new Bridge.DateTime("$init$Int32$Int32$Int32", 2000, 11, 5);
            console.log(Bridge.DateTime.getToday().getDayOfYear());
            console.log("date2", date2.dateData);
        }
    }
});



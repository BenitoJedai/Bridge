ScriptKit.Class.extend('ScriptKit.TestLibrary.MyLibrary', {
    $statics: {
        getName: function () {
            return "Name";
        }
    }
});

ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.config = { Item: "Value", Item1: "Value1" };
            this.$name = "Geoff";
        },
        isEmpty: function (instance) {
            return instance.length > 0;
        },
        start: function () {
            var isEmpty = !TestProject.App.isEmpty("");
            var isEmpty1 = TestProject.App.isEmpty("");
            var obj = { };
            var s = ScriptKit.apply("", { Item: "Value", Item1: "Value1" });
            var cf = { Item: "Value", Item1: "Value1" };
            console.log(cf.Item1);
            console.log(TestProject.App.config.Item1);
            var name = "Item";
            console.log(TestProject.App.config[name]);
        }
    }
});


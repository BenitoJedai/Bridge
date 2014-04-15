ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.INDEX = "index";
            this.$name = null;
        },
        extMethod1: function (str, i) {
            return str;
        },
        extMethod2: function (str, i) {
            return str;
        },
        run: function () {
            var e = TestProject.MyEnum.$name;
            var c = TestProject.App.INDEX;
            var isCool = TestProject.Coolness.cool | TestProject.Coolness.veryCool | TestProject.Coolness.superCool;
            var s = (TestProject.App.ExtMethod2(TestProject.App.ExtMethod1("m".toLocaleLowerCase(),  1).toLocaleString(),  2));
        }
    }
});

ScriptKit.Class.extend('TestProject.Coolness', {
    $statics: {
        init: function () {
            this.cool = 2;
            this.notSoCool = 1;
            this.superCool = 8;
            this.veryCool = 4;
        }
    }
});

ScriptKit.Class.extend('TestProject.MyEnum', {
    $statics: {
        init: function () {
            this.item1 = 0;
            this.item2 = 1;
            this.$name = 2;
        }
    }
});


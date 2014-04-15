ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.iNDEX = "index";
            this.$name = null;
        },
        extMethod1: function (str, i) {
            return str;
        },
        extMethod2: function (str, i) {
            return str;
        },
        run: function () {
            var e = TestProject.MyEnum.$Name;
            var c = TestProject.App.INDEX;
            var s = (TestProject.App.ExtMethod2(TestProject.App.ExtMethod1("m".toLocaleLowerCase(),  1).toLocaleString(),  2));
        }
    }
});

ScriptKit.Class.extend('TestProject.Coolness', {
    $statics: {
        init: function () {
            this.cool = 0;
            this.notSoCool = 5;
            this.superCool = 1;
            this.veryCool = TestProject.Coolness.NotSoCool + 7;
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


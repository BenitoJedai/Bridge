ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.$name = null;
        },
        extMethod1: function (str) {
            return str;
        },
        extMethod2: function (str) {
            return str;
        },
        run: function () {
            var s = TestProject.App.ExtMethod2(TestProject.App.ExtMethod1("m".toLocaleLowerCase()).toLocaleString());
        }
    }
});


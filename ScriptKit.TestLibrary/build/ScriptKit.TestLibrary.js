ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.$name = null;
        },
        extMethod1: function (str, i) {
            return str;
        },
        extMethod2: function (str, i) {
            return str;
        },
        run: function () {
            var dt = new Date();
            var s = (TestProject.App.ExtMethod2(TestProject.App.ExtMethod1("m".toLocaleLowerCase(),  1).toLocaleString(),  2));
        }
    }
});


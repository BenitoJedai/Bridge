Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        method1$Class1: function (c) {
        },
        method1$Int32$String: function (i, s) {
        },
        method1: function () {
            if (arguments.length == 1) {
                if (Bridge.is(arguments[0], TestProject.Class1)) {
                    this.method1$Class1.apply(this, arguments);
                }
            }
            else if (arguments.length == 2) {
                if (Bridge.is(arguments[0], Number) && Bridge.is(arguments[1], String)) {
                    this.method1$Int32$String.apply(this, arguments);
                }
            }
        },
        start: function () {
            var arr = [ ];
            TestProject.Class1.method1$Class1();
            TestProject.Class1.method1$Int32$String(0);
            /* Method1("1", "2", "3", "4"); */
        }
    }
});



Bridge.Class.extend('TestProject.Class1', {
    $init: function () {
    },
    method1$Object: function (value) {
        console.log("object");
    },
    method1$String: function (value) {
        console.log("string");
    },
    method1$Int32: function (value) {
        console.log("int");
    },
    method1: function () {
        if (arguments.length == 1 && Bridge.is(arguments[0], Object)) {
            return this.method1$Object.apply(this, arguments);
        }
        if (arguments.length == 1 && Bridge.is(arguments[0], String)) {
            return this.method1$String.apply(this, arguments);
        }
        if (arguments.length == 1 && Bridge.is(arguments[0], Number)) {
            return this.method1$Int32.apply(this, arguments);
        }
        console.log("[no arguments]");
    },
    method: function () {
        this.method1();
        this.method1$Object({ });
        this.method1$Int32(32);
        this.method1$String("");
        this.method1();
        this.method1$Object({ });
        this.method1$Int32(32);
        this.method1$String("");
    }
});



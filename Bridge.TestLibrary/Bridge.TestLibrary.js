Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        method1: function (s) {
            var c1 = new TestProject.Class1("$init");
            var c2 = new TestProject.Class1("$init$Int32", 1);
            var c3 = new TestProject.Class1("$init$String", s);
        }
    },
    $multipleCtors: true,
    $init: function () {
        console.log("[no arguments]");
    },
    $init$Int32: function (value) {
        console.log("int");
    },
    $init$Double: function (value) {
        console.log("double");
    },
    $init$Object: function (value) {
        console.log("object");
    },
    $init$String: function (value) {
        console.log("string");
    }
});



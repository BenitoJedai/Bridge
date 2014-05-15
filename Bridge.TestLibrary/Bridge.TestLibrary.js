define("mClass1", ["bridge","mClass2"], function (_, mClass2) {
    var exports = {};
    Bridge.Class.extend('TestProject.Class1', {
        $scope: exports,
        $init: function () {
        },
        method1: function () {
            var c = Bridge.cast((new mClass2.TestProject.Class2()), mClass2.TestProject.Class2);
        }
    });

    return exports;
});

define("mClass2", ["bridge","mClass1"], function (_, mClass1) {
    var exports = {};
    Bridge.Class.extend('TestProject.Class2', {
        $scope: exports,
        $init: function () {
        },
        method2: function () {
            var c = new mClass1.TestProject.Class1();
            var c1 = Bridge.cast((new TestProject.Class2()), TestProject.Class2);
        }
    });

    return exports;
});



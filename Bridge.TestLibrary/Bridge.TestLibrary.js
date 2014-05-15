Bridge.Class.extend('TestProject.Class1', {
    $init: function () {
        this.f = 0;
    },
    method2: function () {
    }
});

Bridge.Class.extend('TestProject.Class1', {
    $init: function () {
    },
    method1: function () {
        this.f = 1;
        this.method2();
    }
});



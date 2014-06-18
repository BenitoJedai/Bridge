Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            /* var c = new Class1(); */
            /* c.Method1("str", c.Method2()); */
        }
    },
    $init: function () {
    },
    instStart: function () {
        /* GMethod1<int>(); */
        /* GMethod2<int>(); */
        gm3(this, new Bridge.List(), Bridge.List);
        this.m(this.m2(), "str");
        this.m(m3(1 - "s"), "str");
        m3(0 - "str");
    }
});



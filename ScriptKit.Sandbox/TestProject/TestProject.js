Class.extend('TestProject.App', {
    $extend: [Object],
    $statics: {
        init: function () {
            this.i = 5;
        },
        DoSomething: function (x1) {
            var temp = "test";
            if (x1 > 6) {
                /*  test comment */
                x1 = 1;
            }
            var val = 6;
            TestProject.App.Sum(TestProject.App.i, val);
            var temp2 = "another string";
        },
        Run: function () {
            var sum = TestProject.App.Sum(5, 6);
        },
        Sum: function (x1, x2) {
            return x1 + x2;
        }
    }
});

Class.extend('TestProject.ClassOne', {
    $extend: [Object],
    $statics: {
        init: function () {
            this.propertyZero = 0;
        },
        getPropertyZero: function () {
            return this.propertyZero;
        },
        setPropertyZero: function (value) {
            this.propertyZero = value;
        }
    },
    init: function () {
        this.b1 = false;
        this.f1 = 0;
        this.propertyOne = 0;
        this.s1 = null;

        /*  test comment */
        var fn = function (s) {
            return s;
        };
    },
    getPropertyOne: function () {
        return this.propertyOne;
    },
    setPropertyOne: function (value) {
        this.propertyOne = value;
    },
    getPropertyTwo: function () {
        return this.getPropertyOne()++;
        /*  TODO: Getter is emitted with extra newline after end block */
    }
    ,
    setPropertyTwo: function (value) {
        var iii = 0;
        this.setPropertyOne(iii);
        /*  TODO: Getter is emitted with extra newline after end block */
    }
    ,
    Run: function () {
        var sum = this.Sum(5, 6);
    },
    Sum: function (x1, x2) {
        return x1 + x2;
    }
});

Class.extend('TestProject.ClassTwo', {
    $extend: [TestProject.ClassOne],
    init: function () {
        this.base();
    },
    Sum: function (x1, x2) {
        var i = TestProject.ClassOne.getPropertyZero();
        /*  TODO: this "new" instance is not getting emitted properly. */
        /*  missing class name */
        var c1 = new ();
        var i2 = c1.getPropertyOne();
        return this.base(x1, x2) + x2 + x1;
    }
});


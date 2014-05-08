Bridge.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            var items = [ 1, 1, 2, 3, 5, 8 ];
            console.log(items);
        }
    }
});

Bridge.Class.extend('TestProject.Base1', {
    $init: function () {
    },
    method1: function () {
    },
    method2: function () {
    }
});

Bridge.Class.extend('TestProject.Base2', {
    $extend: [TestProject.Base1],
    $init: function () {
        this.base();
    },
    method1: function () {
    },
    method2: function () {
        TestProject.Base1.method1.call(this);
    }
});

Bridge.Class.extend('TestProject.Person', {
    $statics: {
        doSomething: function (person, d1, d2) {
            console.log(person.getName());
            if (Bridge.equals(d1, d2)) {
            }
        }
    },
    $init: function () {
        this.name = null;
    },
    getName: function () {
        return this.name;
    },
    setName: function (value) {
        this.name = value;
    }
});


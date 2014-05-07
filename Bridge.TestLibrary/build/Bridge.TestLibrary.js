Bridge.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            var person = Bridge.apply(new TestProject.Person(), { setName: "Geoff"} );
            var d1 = new Date();
            var d2 = new Date();
            var arr = [ ];
            if (Bridge.equals(d1, d2)) {
            }
            if (Bridge.equals(new Date(1), d2)) {
            }
            if (Bridge.equals(d1, new Date(person.getName()))) {
            }
            person.setName(person.getName());
        }
    }
});

Bridge.Class.extend('TestProject.Person', {
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


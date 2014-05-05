ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        $init: function () {
            this.$name = "Geoff";
        },
        start: function () {
            var list = new ScriptKit.Dictionary();
            list.add("geoff", ScriptKit.apply(new TestProject.Person(), { setFirstName: "Geoff"} ));
            list.add("daniil", ScriptKit.apply(new TestProject.Person(), { setFirstName: "Daniil"} ));
            list.add("vladimir", ScriptKit.apply(new TestProject.Person(), { setFirstName: "Vladimir"} ));
            var $i = ScriptKit.getEnumerator(list);
            while ($i.hasNext()) {
                var pair = $i.next();
                console.log(pair.key, pair.value.getFullName());
            }
        }
    },
    $init: function () {
        this.field = "Geoff";
    }
});

ScriptKit.Class.extend('TestProject.Person', {
    $init: function () {
        this.firstName = null;
    },
    getFirstName: function () {
        return this.firstName;
    },
    setFirstName: function (value) {
        this.firstName = value;
    },
    getFullName: function () {
        return this.getFirstName();
    }

});


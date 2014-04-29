ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
        }
    }
});

ScriptKit.Class.extend('TestProject.Class1', {
    $statics: {
        $init: function () {
            this.mysf = 0;
            this.$name = 0;
        },
        getName: function () {
            return this.$name;
        },
        setName: function (value) {
            this.$name = value;
        }
    },
    $init: function () {
        this.myf = 0;
        this.myProperty = 0;

        TestProject.Class1.setName(TestProject.Class1.getName());
        TestProject.Class1.setName(TestProject.Class1.getName());
        this.setMyProperty(this.getMyProperty());
        this.setMyProperty(this.getMyProperty());
        this.myf = 1;
        this.myf = 1;
        TestProject.Class1.mysf = 1;
        TestProject.Class1.mysf = 1;
        TestProject.Class1.mysf = TestProject.Class1.mysf;
        TestProject.Class1.mysf = TestProject.Class1.mysf;
        var args = window.arguments;
    },
    getMyProperty: function () {
        return this.myProperty;
    },
    setMyProperty: function (value) {
        this.myProperty = value;
    }
});

ScriptKit.Class.extend('TestProject.StructureA', {
    $init: function () {
        this.prop = 0;
    },
    getProp: function () {
        return this.prop;
    },
    setProp: function (value) {
        this.prop = value;
    }
});


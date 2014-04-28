ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
        }
    }
});

ScriptKit.Class.extend('TestProject.Class1', {
    $statics: {
        $name: function () {
        }
    },
    $init: function () {
        this.myField = 0;
        this.myProperty = 0;

        this.setMyProperty(this.getMyProperty());
        this.myField = 1;
        this.myMethod();
        TestProject.Class1.$name();
    },
    getMyProperty: function () {
        return this.myProperty;
    },
    setMyProperty: function (value) {
        this.myProperty = value;
    },
    myMethod: function () {
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


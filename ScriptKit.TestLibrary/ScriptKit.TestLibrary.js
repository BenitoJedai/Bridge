ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
        }
    }
});

ScriptKit.Class.extend('TestProject.Class1', {
    $statics: {
        $init: function () {
        },
        methodTakingThose: function (inputs) {
        }
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


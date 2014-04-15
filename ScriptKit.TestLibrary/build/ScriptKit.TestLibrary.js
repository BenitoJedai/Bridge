ScriptKit.Class.extend('ScriptKit.TestLibrary.MyLibrary', {
    $statics: {
        getName: function () {
            return "Name";
        }
    }
});

ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.$name = "Geoff";
        },
        isEmpty: function (instance) {
            return instance.length > 0;
        },
        start: function () {
            console.log("IsEmpty", ScriptKit.TestLibrary.MyLibrary.getName());
        }
    }
});


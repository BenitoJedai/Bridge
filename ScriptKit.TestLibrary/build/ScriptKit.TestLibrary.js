ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        $init: function () {
            this.$name = "Geoff";
        },
        start: function () {
            TestProject.App.$name = "Test";
            new TestProject.App().field = "";
        }
    },
    $init: function () {
        this.field = "Geoff";
    }
});


ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        init: function () {
            this.$name = "Geoff";
        },
        isEmpty: function (instance) {
            return instance.length > 0;
        },
        start: function () {
            console.log("IsEmpty", TestProject.App.isEmpty(TestProject.App.$name));
        }
    }
});


ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        $init: function () {
            this.VERSION = "1.0.0-beta";
        },
        start: function () {
            console.log(TestProject.App.VERSION);
            console.log(TestProject.App.VERSION);
            var app = new TestProject.App();
            console.log(app.vERSION1);
        }
    },
    $init: function () {
        this.vERSION1 = "1.0.0-beta";
    }
});


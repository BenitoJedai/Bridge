ScriptKit.Class.extend('DateTime', {
    $statics: {
        $init: function () {
            this.VERSION = "1.0.0-beta";
        },
        getName: function () {
            return "DateTimeJS";
        },
        getVersion: function () {
            return DateTime.VERSION;
        }
    }
});

ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            var date = new DateTime();
        }
    }
});


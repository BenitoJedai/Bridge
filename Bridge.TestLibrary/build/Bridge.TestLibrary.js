Bridge.Class.extend('DateTime', {
    $statics: {
        $init: function () {
            this.VERSION = "2.0.0-beta";
        },
        getVersion: function () {
            return DateTime.VERSION;
        }
    },
    $init: function () {
        this.date = null;
    },
    toDate: function () {
        return this.date;
    }
});

Bridge.Class.extend('TestProject.App', {
    $statics: {
        method: function (s, i) {
        },
        start: function () {
            TestProject.App.method();
            TestProject.App.method("t");
        }
    }
});


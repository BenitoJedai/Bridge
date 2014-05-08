Bridge.Class.extend('TestProject.App', {
    $statics: {
        log: function (obj) {
        },
        start: function () {
            TestProject.App.log(Bridge.DateTime.getToday().getDay());
        }
    }
});


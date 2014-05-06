Bridge.Class.extend('TestProject.App', {
    $statics: {
        getDate: function () {
            return new Date();
        },
        start: function () {
            var d1 = new Date();
            var d2 = new Date();
            /* if (d1 == d2) */
            if (Bridge.equals(TestProject.App.getDate(), d2)) {
                console.log("Equals");
            }
        }
    }
});


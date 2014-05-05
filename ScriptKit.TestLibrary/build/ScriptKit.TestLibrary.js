ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        $init: function () {
            this.$name = "Geoff";
        },
        start: function () {
            if (typeof arguments != "undefined") {
            }
            window.addEventListener("click", function () {
                console.log("Click", arguments);
            });
        }
    },
    $init: function () {
        this.field = "Geoff";
    }
});

ScriptKit.Class.extend('TestProject.Person', {
    $init: function () {
        this.firstName = null;
    },
    getFirstName: function () {
        return this.firstName;
    },
    setFirstName: function (value) {
        this.firstName = value;
    },
    getFullName: function () {
        return this.getFirstName();
    }

});


Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
        }
    },
    $init: function () {
    },
    loadInit1: function () {
    },
    method1: function () {
        document.addEventListener("DOMContentLoaded", Bridge.bind(this, this.loadInit1), false);
        document.addEventListener("DOMContentLoaded", Bridge.bind(this, this.loadInit), false);
        document.addEventListener("DOMContentLoaded", this.loadInit1, false);
    }
});

Bridge.Class.extend('TestProject.Class1Extension', {
    $statics: {
        loadInit: function (instance) {
        }
    }
});



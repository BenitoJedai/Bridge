Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            Bridge.fn.call(document.getElementById("el1"), "name", [1, 2]);
            Bridge.fn.call(document.getElementById("el1"), "name", [1, 2]);
        }
    }
});



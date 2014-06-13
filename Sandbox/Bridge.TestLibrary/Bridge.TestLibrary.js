Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            System.ObjectExtensions.as(document.getElementById("id"));
            Bridge.fn.call(document.getElementById("id"), "fnName", null);
        }
    }
});



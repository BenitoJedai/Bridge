ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            window.addEventListener("DOMContentLoaded", function (e) {
                console.log(ScriptKit.is(e, "MouseEvent"));
            });
        }
    }
});


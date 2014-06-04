Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            var btn = document.getElementById("btn1");
            btn.onclick += function (e) {
                window.alert("click1");
            };
            btn.onclick = function (e) { return e.stopPropagation() };
            btn.onclick += function (e) {
                window.alert("click2");
            };
        }
    }
});



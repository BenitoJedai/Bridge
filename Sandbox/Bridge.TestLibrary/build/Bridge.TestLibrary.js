Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        start: function () {
            window.onload = function () {
                var txt = Bridge.merge(document.createElement('input'), {
                    value: "Hello Input", 
                    style: {
                        width: "239px", color: "red"
                    }
                } );
                document.body.appendChild(txt);
            };
        }
    }
});



Bridge.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            var obj = { Test: "" };
            var c = Bridge.apply({
    doSomething: function () {
        console.log("DoSomething called");
    }
}, { setName: "Test"} );
            c.doSomething();
            console.log(c.name);
        }
    }
});


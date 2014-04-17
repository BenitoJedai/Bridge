ScriptKit.Class.extend('ScriptKit.TestLibrary.MyLibrary', {
    $statics: {
        getName: function () {
            return "Name";
        }
    }
});

ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
            var gt = new TestProject.GType();
            var s1 = gt.doSomething();
            var s2 = gt.doSomething2();
            var dict = new ScriptKit.Dictionary();
        }
    }
});

ScriptKit.Class.extend('TestProject.GType', {
    init: function () {
    },
    doSomething: function () {
        return ;
    },
    doSomething2: function () {
        return ;
    }
});


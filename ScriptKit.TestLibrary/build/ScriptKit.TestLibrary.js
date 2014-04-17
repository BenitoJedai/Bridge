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
            var list = new ScriptKit.List([ "Item1", "Item2", "Item3" ]);
            list.remove("Item2");
            var $i = ScriptKit.getEnumerator(list);
            while ($i.hasNext()) {
                var item = $i.next();
                console.log(item);
            }
        }
    }
});


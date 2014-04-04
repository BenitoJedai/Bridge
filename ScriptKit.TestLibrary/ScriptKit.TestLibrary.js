ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        Average: function (numbers) {
            var result = 0;
            var len = numbers.length;
            var $i = ScriptKit.getIterator(numbers)
            while ($i.hasNext()) {
                var number = $i.next();
                result += number;
            }
            return result / len;
        },
        CalculateMetrics: function (numbers) {
            console.info("Max: %d", TestProject.App.Max(numbers));
            console.info("Min: %d", TestProject.App.Min(numbers));
            console.info("Average: %d", TestProject.App.Average(numbers));
            console.info("Sum: %d", TestProject.App.Sum(numbers));
        },
        Max: function (numbers) {
            var result = Number.MIN_VALUE;
            var $i = ScriptKit.getIterator(numbers)
            while ($i.hasNext()) {
                var number = $i.next();
                if (number > result) {
                    result = number;
                }
            }
            return result;
        },
        Min: function (numbers) {
            var result = Number.MAX_VALUE;
            var $i = ScriptKit.getIterator(numbers)
            while ($i.hasNext()) {
                var number = $i.next();
                if (number < result) {
                    result = number;
                }
            }
            return result;
        },
        Sum: function (numbers) {
            var result = 0;
            var $i = ScriptKit.getIterator(numbers)
            while ($i.hasNext()) {
                var number = $i.next();
                result += number;
            }
            return result;
        }
    }
});

ScriptKit.Class.extend('TestProject.Class1', {
    init: function () {
        this.property1 = null;
    },
    getProperty1: function () {
        return this.property1;
    },
    setProperty1: function (value) {
        this.property1 = value;
    },
    getProperty2: function () {
        return this.getProperty1().Method1();
    }
    ,
    Method1: function () {
        return this;
    },
    Method2: function () {
        this.Method1().Method2();
    }
});


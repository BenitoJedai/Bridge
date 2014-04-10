ScriptKit.Class.extend('TestProject.App', {
    $statics: {
        average: function (numbers) {
            var result = 0;
            var len = numbers.length;
            var $i = ScriptKit.getIterator(numbers)
            while ($i.hasNext()) {
                var number = $i.next();
                result += number;
            }
            return result / len;
        },
        calculateMetrics: function (numbers) {
            /* var customer = new Customer { Name = "Geoff" }; */
            var customer = new TestProject.Customer("Geoff");
            customer.setName("Geoff");
            customer.setCustomerId(1234);
            console.log(customer.getName());
            var date = new Date();
            console.log(date.getDay());
            console.info("Max: %d", TestProject.App.max(numbers));
            console.info("Min: %d", TestProject.App.min(numbers));
            console.info("Average: %d", TestProject.App.average(numbers));
            console.info("Sum: %d", TestProject.App.sum(numbers));
        },
        max: function (numbers) {
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
        min: function (numbers) {
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
        sum: function (numbers) {
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

ScriptKit.Class.extend('TestProject.Customer', {
    $extend: [TestProject.Person],
    init: function (name) {
        this.customerId = 0;

        this.base();

        this.setName(name);
    },
    getCustomerId: function () {
        return this.customerId;
    },
    setCustomerId: function (value) {
        this.customerId = value;
    }
});

ScriptKit.Class.extend('TestProject.Person', {
    init: function () {
        this.name = null;
    },
    getName: function () {
        return this.name;
    },
    setName: function (value) {
        this.name = value;
    }
});


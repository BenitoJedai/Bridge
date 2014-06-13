Bridge.Class.extend('Bridge.DateTime', {
    $statics: {
        $init: function () {
            this.MaxValue = new Bridge.DateTime("$init$Double", 123);
            this.MinValue = new Bridge.DateTime("$init$Double", 0);
            this.VERSION = "2.0.0-beta";
        },
        getToday: function () {
            return new Bridge.DateTime("$init$Double", new Date().getTime()).getDate();
        },
        getNow: function () {
            return new Bridge.DateTime("$init$Double", new Date().getTime());
        },
        isLeapYear: function (year) {
            return ((year % 4 === 0 && year % 100 !== 0) || year % 400 === 0);
        },
        compare: function (t1, t2) {
            return t1.getTime() < t2.getTime() ? -1 : t1.getTime() > t2.getTime() ? 1 : 0;
        },
        equals: function (t1, t2) {
            return t1.getTime() === t2.getTime();
        },
        daysInMonth: function (year, month) {
            return [31, (Bridge.DateTime.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month - 1];
        },
        parse: function (s) {
            return new Bridge.DateTime("$init$Double", Date.parse(s).getTime());
        }
    },
    $multipleCtors: true,
    $ctorDetector: function () {
        if (arguments.length == 1) {
            if (Bridge.is(arguments[0], Number)) {
                this.$init$Double.apply(this, arguments);
            }
        }
        else if (arguments.length == 3) {
            if (Bridge.is(arguments[0], Number) && Bridge.is(arguments[1], Number) && Bridge.is(arguments[2], Number)) {
                this.$init$Int32$Int32$Int32.apply(this, arguments);
            }
        }
        else if (arguments.length == 0) {
            this.$init.apply(this, arguments);
        }
    },
    $init$Double: function (time) {
        this.dateData = null;

        this.dateData = new Date(time);
    },
    $init$Int32$Int32$Int32: function (year, month, day) {
        this.dateData = null;

        this.dateData = new Date(year, month, day);
    },
    $init: function () {
        this.dateData = null;
    },
    getYear: function () {
        return this.dateData.getFullYear();
    },
    getMonth: function () {
        return this.dateData.getMonth() + 1;
    },
    getDay: function () {
        return this.dateData.getDate();
    },
    getHour: function () {
        return this.dateData.getHours();
    },
    getMinute: function () {
        return this.dateData.getMinutes();
    },
    getSecond: function () {
        return this.dateData.getSeconds();
    },
    getMillisecond: function () {
        return this.dateData.getMilliseconds();
    },
    getDate: function () {
        var clone = new Bridge.DateTime("$init$Double", this.dateData.getTime());
        clone.dateData.setHours(0);
        clone.dateData.setMinutes(0);
        clone.dateData.setSeconds(0);
        clone.dateData.setMilliseconds(0);
        return clone;
    },
    getDayOfWeek: function () {
        return Bridge.cast(this.dateData.getDay(), Bridge.DayOfWeek);
    },
    getDayOfYear: function () {
        return Bridge.cast(Math.floor((this.getDate().dateData - new Date(new Date().getFullYear(), 0, 0, 0, 0, 0, 0)) / 86400000.0), Number);
    },
    getTime: function () {
        return this.dateData.getTime();
    },
    addYears: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setFullYear(this.dateData.getFullYear() + value);
        }
        return this;
    },
    addMonths: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setMonth(this.dateData.getMonth() + value);
        }
        return this;
    },
    addDays: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setDate(this.dateData.getDate() + value);
        }
        return this;
    },
    addHours: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setHours(this.dateData.getHours() + value);
        }
        return this;
    },
    addMinutes: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setMinutes(this.dateData.getMinutes() + value);
        }
        return this;
    },
    addSeconds: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setSeconds(this.dateData.getSeconds() + value);
        }
        return this;
    },
    addMilliseconds: function (value) {
        if (typeof value !== 'undefined') {
            this.dateData.setMilliseconds(this.dateData.getMilliseconds() + value);
        }
        return this;
    },
    compareTo: function (value) {
        return Bridge.DateTime.compare(this, value);
    },
    equals: function (value) {
        return Bridge.DateTime.equals$DateTime$DateTime(this, value);
    },
    isDaylightSavingTime: function () {
        var temp = Bridge.DateTime.getToday().dateData;
        temp.setMonth(0);
        temp.setDate(1);
        return temp.getTimezoneOffset() !== this.dateData.getTimezoneOffset();
    }
});


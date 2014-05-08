Bridge.Class.extend('Bridge.DateTime', {
    $statics: {
        $init: function () {
            this.MaxValue = new Bridge.DateTime(1234567890123);
            this.MinValue = new Bridge.DateTime(0);
            this.VERSION = "2.0.0-beta";
        },
        getNow: function () {
            return new Bridge.DateTime(new Date().getTime());
        },
        getToday: function () {
            return new Bridge.DateTime(new Date().getTime()).getDate();
        },
        compare: function (t1, t2) {
            return t1.getTime() < t2.getTime() ? -1 : t1.getTime() > t2.getTime() ? 1 : 0;
        },
        daysInMonth: function (year, month) {
            return [ 31, (Bridge.DateTime.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 ][month - 1];
        },
        equals: function (t1, t2) {
            return t1.getTime() == t2.getTime();
        },
        isLeapYear: function (year) {
            return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0);
        },
        parse: function (s) {
            return new Bridge.DateTime(Date.parse(s).getTime());
        }
    },
    $init: function (time) {
        this.dateData = null;

        this.dateData = new Date(time);
    },
    getDate: function () {
        var clone = new Bridge.DateTime(this.dateData.getTime());
        clone.dateData.setHours(0);
        clone.dateData.setMinutes(0);
        clone.dateData.setSeconds(0);
        clone.dateData.setMilliseconds(0);
        return clone;
    },
    getDay: function () {
        return this.dateData.getDate();
    },
    getDayOfWeek: function () {
        return Bridge.cast(this.dateData.getDay(), Bridge.DayOfWeek);
    },
    getDayOfYear: function () {
        return Bridge.cast(Math.floor((this.getDate().dateData - new Date(new Date().getFullYear(), 0, 0, 0, 0, 0, 0)) / 86400000.0), Number);
    },
    getHour: function () {
        return this.dateData.getHours();
    },
    getMillisecond: function () {
        return this.dateData.getMilliseconds();
    },
    getMinute: function () {
        return this.dateData.getMinutes();
    },
    getMonth: function () {
        return this.dateData.getMonth() + 1;
    },
    getSecond: function () {
        return this.dateData.getSeconds();
    },
    getTime: function () {
        return this.dateData.getTime();
    },
    getYear: function () {
        return this.dateData.getFullYear();
    },
    addDays: function (value) {
        this.dateData.setDate(this.dateData.getDate() + value);
        return this;
    },
    addHours: function (value) {
        this.dateData.setHours(this.dateData.getHours() + value);
        return this;
    },
    addMilliseconds: function (value) {
        this.dateData.setMilliseconds(this.dateData.getMilliseconds() + value);
        return this;
    },
    addMinutes: function (value) {
        this.dateData.setMinutes(this.dateData.getMinutes() + value);
        return this;
    },
    addMonths: function (value) {
        this.dateData.setMonth(this.dateData.getMonth() + value);
        return this;
    },
    addSeconds: function (value) {
        this.dateData.setSeconds(this.dateData.getSeconds() + value);
        return this;
    },
    addYears: function (value) {
        this.dateData.setFullYear(this.dateData.getFullYear() + value);
        return this;
    },
    compareTo: function (value) {
        return Bridge.DateTime.compare(this, value);
    },
    equals: function (value) {
        return Bridge.DateTime.equals(this, value);
    },
    isDaylightSavingTime: function () {
        var temp = Bridge.DateTime.getToday().dateData;
        temp.setMonth(0);
        temp.setDate(1);
        return temp.getTimezoneOffset() != this.dateData.getTimezoneOffset();
    }
});

Bridge.Class.extend('Bridge.DayOfWeek', {
    $statics: {
        $init: function () {
            this.friday = 5;
            this.monday = 1;
            this.saturday = 6;
            this.sunday = 0;
            this.thursday = 4;
            this.tuesday = 2;
            this.wednesday = 3;
        }
    }
});


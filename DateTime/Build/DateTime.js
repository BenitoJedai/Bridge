Bridge.Class.extend('DateTime', {
    $statics: {
        $init: function () {
            this.MaxValue = new DateTime(1234567890123);
            this.MinValue = new DateTime(0);
            this.VERSION = "2.0.0-beta";
        },
        getNow: function () {
            return new DateTime(new Date().getTime());
        }
        ,
        getToday: function () {
            return new DateTime(new Date().getTime()).clearTime();
        }
        ,
        compare: function (t1, t2) {
            return t1.time < t2.toDate().getTime() ? -1 : t1.toDate().getTime() > t2.toDate().getTime() ? 1 : 0;
        },
        equals: function (t1, t2) {
            var temp = DateTime.getNow();
            return t1.time == t2.time;
        },
        isLeapYear: function (year) {
            return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0);
        }
    },
    $init: function (time) {
        this.date = null;

        this.date = new Date(time);
    },
    getTime: function () {
        return this.toDate().getTime();
    }
    ,
    clearTime: function () {
        /* * @param {Boolean} .clone() this DateTime instance before clearing Time */
        /* return this.hours(0).minutes(0).seconds(0).milliseconds(0); */
        this.date.setHours(0);
        this.date.setMinutes(0);
        this.date.setSeconds(0);
        this.date.setMilliseconds(0);
        return this;
    },
    clone: function () {
        return new DateTime(this.date.getTime());
    },
    resetTime: function () {
        var n = new Date();
        this.date.setHours(n.getHours());
        this.date.setMinutes(n.getMinutes());
        this.date.setSeconds(n.getSeconds());
        this.date.setMilliseconds(n.getMilliseconds());
        /*  return this.hours(n.getHours()).minutes(n.getMinutes()).seconds(n.getSeconds()).milliseconds(n.getMilliseconds()); */
        return this;
    },
    toDate: function () {
        return this.date;
    }
});


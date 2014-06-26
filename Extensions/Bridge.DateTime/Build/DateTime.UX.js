Bridge.Class.extend('Bridge.DateTime.UX', {
    $statics: {
        add: function (instance, config) {
            return instance.addYears(config.year).addMonths(config.month).addDays(config.day).addHours(config.hour).addMinutes(config.minute).addSeconds(config.second).addMilliseconds(config.millisecond);
        },
        set: function (instance, config) {
            if (config.year >= 0) {
                Bridge.DateTime.UX.setYear(instance, config.year);
            }
            if (config.month >= 1 && config.month < 12) {
                Bridge.DateTime.UX.setMonth(instance, config.month);
            }
            if (config.day >= 1 && config.day <= Bridge.DateTime.daysInMonth(instance.getYear(), instance.getMonth())) {
                Bridge.DateTime.UX.setDay(instance, config.day);
            }
            if (config.hour >= 0 && config.hour <= 23) {
                Bridge.DateTime.UX.setHour(instance, config.hour);
            }
            if (config.minute >= 0 && config.minute <= 59) {
                Bridge.DateTime.UX.setMinute(instance, config.minute);
            }
            if (config.second >= 0 && config.second <= 59) {
                Bridge.DateTime.UX.setSeconds(instance, config.second);
            }
            if (config.millisecond >= 0 && config.millisecond <= 999) {
                Bridge.DateTime.UX.setMilliseconds(instance, config.millisecond);
            }
            return instance;
        },
        clearTime: function (instance) {
            return Bridge.DateTime.UX.set(instance, { hour: 0, minute: 0, second: 0, millisecond: 0 });
        },
        resetTime: function (instance) {
            var now = Bridge.DateTime.getNow();
            return Bridge.DateTime.UX.set(instance, { hour: now.getHour(), minute: now.getMinute(), second: now.getSecond(), millisecond: now.getMillisecond() });
        },
        clone: function (instance) {
            return new Bridge.DateTime("$init$Double", instance.dateData.getTime());
        },
        setYear: function (instance, year) {
            return instance.addYears(-(instance.getYear() - year));
        },
        setMonth: function (instance, month) {
            return instance.addMonths(-(instance.getMonth() - month));
        },
        setDay: function (instance, day) {
            return instance.addDays(-(instance.getDay() - day));
        },
        setHour: function (instance, hour) {
            return instance.addHours(-(instance.getHour() - hour));
        },
        setMinute: function (instance, minute) {
            return instance.addMinutes(-(instance.getMinute() - minute));
        },
        setSeconds: function (instance, second) {
            return instance.addSeconds(-(instance.getSecond() - second));
        },
        setMilliseconds: function (instance, millisecond) {
            return instance.addMilliseconds(-(instance.getMillisecond() - millisecond));
        }
    }
});


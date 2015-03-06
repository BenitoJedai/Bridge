Bridge.Class.extend("Bridge.DateTimeFormatInfo", {
    $extend: [Bridge.IFormatProvider, Bridge.ICloneable],

    $statics: {
        $allStandardFormats: {
            "d": "shortDatePattern",
            "D": "longDatePattern",
            "f": "longDatePattern shortTimePattern",
            "F": "longDatePattern longTimePattern",
            "g": "shortDatePattern shortTimePattern",
            "G": "shortDatePattern longTimePattern",
            "m": "monthDayPattern",
            "M": "monthDayPattern",
            "o": "roundtripFormat",
            "O": "roundtripFormat",
            "r": "rFC1123",
            "R": "rFC1123",
            "s": "sortableDateTimePattern",
            "t": "shortTimePattern",
            "T": "longTimePattern",
            "u": "universalSortableDateTimePattern",
            "U": "longDatePattern longTimePattern",
            "y": "yearMonthPattern",
            "Y": "yearMonthPattern"
        },

        $ctor: function () {
            this.invariantInfo = Bridge.merge(new Bridge.DateTimeFormatInfo(), {
                abbreviatedDayNames: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"],
                abbreviatedMonthGenitiveNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", ""],
                abbreviatedMonthNames: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", ""],
                amDesignator: "AM",
                dateSeparator: "/",
                dayNames: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
                firstDayOfWeek: 0,
                fullDateTimePattern: "dddd, MMMM dd, yyyy h:mm:ss tt",
                longDatePattern: "dddd, MMMM dd, yyyy",
                longTimePattern: "h:mm:ss tt",
                monthDayPattern: "MMMM dd",
                monthGenitiveNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", ""],
                monthNames: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", ""],
                pmDesignator: "PM",
                rFC1123: "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'",
                shortDatePattern: "M/d/yyyy",
                shortestDayNames: ["Su", "Mo", "Tu", "We", "Th", "Fr", "Sa"],
                shortTimePattern: "h:mm tt",
                sortableDateTimePattern: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
                timeSeparator: ":",
                universalSortableDateTimePattern: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
                yearMonthPattern: "MMMM, yyyy",
                roundtripFormat: "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"
            });
        }
    },

    getFormat: function (type) {
        switch (type) {
            case Bridge.DateTimeFormatInfo:
                return this.dateTimeFormat;
            default:
                return null;
        }
    },

    getAbbreviatedDayName: function (dayofweek) {
        if (dayofweek < 0 || dayofweek > 6) {
            throw new Bridge.ArgumentOutOfRangeException("dayofweek");
        }        
        return this.abbreviatedDayNames[dayofweek];
    },

    getAbbreviatedMonthName: function (month) {
        if (month < 1 || month > 13) {
            throw new Bridge.ArgumentOutOfRangeException("month");
        }
        return this.abbreviatedMonthNames[month - 1];
    },

    getAllDateTimePatterns: function (format) {
        var f = Bridge.DateTimeFormatInfo.$allStandardFormats,
            formats,
            names,
            pattern,
            i,
            result = [];

        if (format) {
            if (!f[format]) {
                throw new Bridge.ArgumentException(null, "format");
            }

            formats = {};
            formats[format] = f[format];
        }
        else {
            formats = f;
        }

        for (f in formats) {
            names = formats[f].split(" ");
            pattern = "";
            for (i = 0; i < names.length; i++) {
                pattern = (i == 0 ? "" : (pattern + " ")) + this[names[i]];
            }

            result.push(pattern);
        }

        return result;
    },

    getDayName: function (dayofweek) {
        if (dayofweek < 0 || dayofweek > 6) {
            throw new Bridge.ArgumentOutOfRangeException("dayofweek");
        }
 
        return this.dayNames[dayofweek];
    },

    getMonthName: function (month) {
        if (month < 1 || month > 13) {
            throw new Bridge.ArgumentOutOfRangeException("month");
        }
        return this.monthNames[month-1];
    },

    getShortestDayName: function (dayOfWeek) {
        if (dayOfWeek < 0 || dayOfWeek > 6) {
            throw new Bridge.ArgumentOutOfRangeException("dayOfWeek");
        }
        return this.shortestDayNames[dayOfWeek];
    },

    clone: function () {
        return Bridge.copy(new Bridge.DateTimeFormatInfo(), this, [
            "abbreviatedDayNames",
            "abbreviatedMonthGenitiveNames",
            "abbreviatedMonthNames",
            "amDesignator",
            "dateSeparator",
            "dayNames",
            "firstDayOfWeek",
            "fullDateTimePattern",
            "longDatePattern",
            "longTimePattern",
            "monthDayPattern",
            "monthGenitiveNames",
            "monthNames",
            "pmDesignator",
            "rFC1123",
            "shortDatePattern",
            "shortestDayNames",
            "shortTimePattern",
            "sortableDateTimePattern",
            "timeSeparator",
            "universalSortableDateTimePattern",
            "yearMonthPattern",
            "roundtripFormat"
        ]);
    }
});

Bridge.Class.extend("Bridge.DateTimeFormatInfo", {
    $extend: [Bridge.IFormatProvider, Bridge.ICloneable],

    $statics: {
        $ctor: function () {
            this.numberNegativePatterns =  ["(n)", "-n", "- n", "n-", "n -"];
            this.currencyNegativePattern = ["($n)", "-$n", "$-n", "$n-", "(n$)", "-n$", "n-$", "n$-", "-n $", "-$ n", "n $-", "$ n-", "$ -n", "n- $", "($ n)", "(n $)"];
            this.currencyPositivePattern = ["$n", "n$", "$ n", "n $"];
            this.percentNegativePattern = ["-n %", "-n%", "-%n", "%-n", "%n-", "n-%", "n%-", "-% n", "n %-", "% n-", "% -n", "n- %"];
            this.percentPositivePattern = ["n %", "n%", "%n", "% n"];

            this.invariantInfo = Bridge.merge(new Bridge.NumberFormatInfo(), {
                naNSymbol: "NaN",
                negativeSign: "-",
                positiveSign: "+",
                negativeInfinitySymbol: "-Infinity",
                positiveInfinitySymbol: "Infinity",

                percentSymbol: "%",
                percentGroupSizes: [3],
                percentDecimalDigits: 2,
                percentDecimalSeparator: ".",
                percentGroupSeparator: ",",
                percentPositivePattern: 0,
                percentNegativePattern: 0,

                currencySymbol: "$",
                currencyGroupSizes: [3],
                currencyDecimalDigits: 2,
                currencyDecimalSeparator: ".",
                currencyGroupSeparator: ",",
                currencyNegativePattern: 0,
                currencyPositivePattern: 0,

                numberGroupSizes: [3],
                numberDecimalDigits: 2,
                numberDecimalSeparator: ".",
                numberGroupSeparator: ",",
                numberNegativePattern: 0
            });
        }
    },

    getFormat: function (type) {
        switch (type) {
            case Bridge.NumberFormatInfo:
                return this.numberFormat;
            default:
                return null;
        }
    },

    clone: function () {
        return Bridge.copy(new Bridge.NumberFormatInfo(), this, [
            "naNSymbol",
            "negativeSign",
            "positiveSign",
            "negativeInfinitySymbol",
            "positiveInfinitySymbol",
            "percentSymbol",
            "percentGroupSizes",
            "percentDecimalDigits",
            "percentDecimalSeparator",
            "percentGroupSeparator",
            "percentPositivePattern",
            "percentNegativePattern",
            "currencySymbol",
            "currencyGroupSizes",
            "currencyDecimalDigits",
            "currencyDecimalSeparator",
            "currencyGroupSeparator",
            "currencyNegativePattern",
            "currencyPositivePattern",
            "numberGroupSizes",
            "numberDecimalDigits",
            "numberDecimalSeparator",
            "numberGroupSeparator",
            "numberNegativePattern"
        ]);
    }
});

Bridge.Class.extend("Bridge.CultureInfo", {
    $extend: [Bridge.IFormatProvider, Bridge.ICloneable],

    $statics: {
        $ctor: function () {
            this.cultures = {};
            this.invariantCulture = Bridge.merge(new Bridge.CultureInfo("en-US"), {
                englishName: "English (United States)",
                nativeName: "English (United States)",
                numberFormat: Bridge.NumberFormatInfo.invariantInfo, 
                dateTimeFormat: Bridge.DateTimeFormatInfo.invariantInfo
            });
            this.setCurrentCulture(this.invariantCulture);
        },

        getCurrentCulture : function () {
            return this.currentCulture;
        },

        setCurrentCulture: function (culture) {
            this.currentCulture = culture;
            Bridge.DateTimeFormatInfo.currentInfo = culture.dateTimeFormat;
            Bridge.NumberFormatInfo.currentInfo = culture.numberFormat;
        },

        getCultureInfo: function (name) {
            if (!name) {
                throw new Bridge.ArgumentNullException("name");
            }
            return this.cultures[name];
        },

        getCultures: function () {
            var names = Bridge.getPropertyNames(this.cultures),
                result = [],
                i;
            for (i = 0; i < names.length; i++) {
                result.push(this.cultures[names[i]]);
            }

            return result;
        }
    },

    $ctor: function (name) {
        this.name = name;
        Bridge.CultureInfo.cultures[name] = this;
    },

    getFormat:  function (type) {
        switch (type) {
            case Bridge.NumberFormatInfo:
                return this.numberFormat;
            case Bridge.DateTimeFormatInfo:
                return this.dateTimeFormat;
            default:
                return null;
        }
    },

    clone: function () {
        return Bridge.copy(new Bridge.CultureInfo(this.name), this, [
            "englishName",
            "nativeName",
            "numberFormat",
            "dateTimeFormat"
        ]);
    }
});
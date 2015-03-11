Bridge.Date = {    
    today : function() {
        var d = new Date();
        return new Date(d.getFullYear(), d.getMonth(), d.getDate());
    },

    isUseGenitiveForm: function(format, index, tokenLen, patternToMatch) {
	    var i,
            repeat = 0;
        
	    for (i = index - 1; i >= 0 && format[i] != patternToMatch; i--) { 
	    }

        if (i >= 0) {
            while (--i >= 0 && format[i] == patternToMatch) {
                repeat++;
            }
            if (repeat <= 1) {
                return true;
            }
        }
	
        for (i = index + tokenLen; i < format.length && format[i] != patternToMatch; i++) {
        }

        if (i < format.length) {
            repeat = 0;                
            while (++i < format.length && format[i] == patternToMatch) {
                repeat++;
            }
            if (repeat <= 1) {
                return true;
            }
        }
        return false;
    },

    format: function (date, format, provider) {
        var me = this,
            df = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.DateTimeFormatInfo),        
            year = date.getFullYear(),
            month = date.getMonth(),
            dayOfMonth = date.getDate(),
            dayOfWeek = date.getDay(),
            hour = date.getHours(),
            minute = date.getMinutes(),
            second = date.getSeconds(),
            millisecond = date.getMilliseconds,
            timezoneOffset = date.getTimezoneOffset(),
            formats;

        format = format || "G";

        if (format.length == 1) {
            formats = df.getAllDateTimePatterns(format, true);
            format = formats ? formats[0] : format;
        }

        return format.replace(/(\\.|'[^']*'|"[^"]*"|d{1,4}|M{1,4}|yyyy|yy|y|HH?|hh?|mm?|ss?|tt?|f{1,3}|z{1,3}|\:|\/)/g,
			function (match, group, index) {
			    var part = match;
			    switch (match) {
			        case "dddd":
			            part = df.dayNames[dayOfWeek];
			            break;
			        case "ddd":
			            part = df.abbreviatedDayNames[dayOfWeek];
			            break;
			        case "dd":
			            part = dayOfMonth < 10 ? "0" + dayOfMonth : dayOfMonth;
			            break;
			        case "d":
			            part = dayOfMonth;
			            break;
			        case "MMMM":
			            if (me.isUseGenitiveForm(format, index, 4, "d")) {
			                part = df.monthGenitiveNames[month];
			            }
			            else {
			                part = df.monthNames[month];
			            }
			            break;
			        case "MMM":
			            if (me.isUseGenitiveForm(format, index, 4, "d")) {
			                part = df.abbreviatedMonthGenitiveNames[month];
			            }
			            else {
			                part = df.abbreviatedMonthNames[month];
			            }
			            break;
			        case "MM":
			            part = (month + 1) < 10 ? "0" + (month + 1) : (month + 1);
			            break;
			        case "M":
			            part = month + 1;
			            break;
			        case "yyyy":
			            part = year;
			            break;
			        case "yy":
			            part = (year % 100).toString();
			            if (part.length == 1) {
			                part = "0" + part;
			            }
			            break;
			        case "y":
			            part = year % 100;
			            break;
			        case "h":
			        case "hh":
			            part = hour % 12;
			            if (!part) {
			                part = "12";
			            }
			            else if (match == "hh" && part.length == 1) {
			                part = "0" + part;
			            }
			            break;
			        case "HH":
			            part = hour.toString();
			            if (part.length == 1) {
			                part = "0" + part;
			            }
			            break;
			        case "H":
			            part = hour;
			            break;
			        case "mm":
			            part = minute.toString();
			            if (part.length == 1) {
			                part = "0" + part;
			            }
			            break;
			        case "m":
			            part = minute;
			            break;
			        case "ss":
			            part = second.toString();
			            if (part.length == 1) {
			                part = "0" + part;
			            }
			            break;
			        case "s":
			            part = second;
			            break;
			        case "t":
			        case "tt":
			            part = (hour < 12) ? df.amDesignator : df.pmDesignator;
			            if (match == "t") {
			                part = part.charAt(0);
			            }
			            break;
			        case "f":
			        case "ff":
			        case "fff":
			            part = millisecond.toString();
			            if (part.length < 3) {
			                part = Array(3 - part.length).join("0") + part;
			            }

			            if (match == "ff") {
			                part = part.substr(0, 2);
			            }
			            else if (match == "f") {
			                part = part.charAt(0);
			            }
			            break;
			        case "z":
			            part = timezoneOffset / 60;
			            part = ((part >= 0) ? "-" : "+") + Math.floor(Math.abs(part));
			            break;
			        case "zz":
			        case "zzz":
			            part = timezoneOffset / 60;			            
			            part = ((part >= 0) ? "-" : "+") + Bridge.String.alignString(Math.floor(Math.abs(part)).toString(), 2, "0", 2);
			            if (match == "zzz") {
			                part += df.timeSeparator + Bridge.String.alignString(Math.floor(Math.abs(timezoneOffset % 60)).toString(), 2, "0", 2);
			            }
			            break;
			        case ":":
			            part = df.timeSeparator;
			            break;
			        case "/":
			            part = df.dateSeparator;
			            break;
			        default:
			            part = match.substr(1, match.length - 1 - (match.charAt(0) != "\\"));
			            break;
			    }

			    return part;
			});
    },

    parse: function (value, provider) {
        throw new Bridge.NotImplementedException();
    },

    parseExact: function (value, format, provider) {
        throw new Bridge.NotImplementedException();
    },

    tryParse: function (value, provider, result) {
        throw new Bridge.NotImplementedException();
    },

    tryParseExact: function (value, format, provider, result) {
        throw new Bridge.NotImplementedException();
    },

    isDaylightSavingTime: function (dt) {
        var temp = Bridge.Date.today();
        temp.setMonth(0);
        temp.setDate(1);

        return temp.getTimezoneOffset() != dt.getTimezoneOffset();
    },

    toUTC: function(date) {
        return new Date(date.getUTCFullYear(), 
                        date.getUTCMonth(), 
                        date.getUTCDate(), 
                        date.getUTCHours(), 
                        date.getUTCMinutes(), 
                        date.getUTCSeconds(), 
                        date.getUTCMilliseconds());
    },

    toLocal: function(date) {
        return new Date(Date.UTC(date.getFullYear(),
                                 date.getMonth(),
                                 date.getDate(),
                                 date.getHours(),
                                 date.getMinutes(),
                                 date.getSeconds(),
                                 date.getMilliseconds()));
    }
};
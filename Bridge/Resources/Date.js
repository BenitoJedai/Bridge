Bridge.Date = {    
    utcNow:  function () {
        var d = new Date();

        return new Date(d.getUTCFullYear(), d.getUTCMonth(), d.getUTCDate(), d.getUTCHours(), d.getUTCMinutes(), d.getUTCSeconds(), d.getUTCMilliseconds());
    },

    today: function () {
        var d = new Date();

        return new Date(d.getFullYear(), d.getMonth(), d.getDate());
    },

    isUseGenitiveForm: function (format, index, tokenLen, patternToMatch) {
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
        else if (format.length == 2 && format.charAt(0) == "%") {
            format = format.charAt(1);
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
			            if (me.isUseGenitiveForm(format, index, 3, "d")) {
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
        var dt = Date.parse(value);
        if (!isNaN(dt)) {
            return new Date(dt);
        }
        return this.parseExact(value, null, provider);
    },    

    parseExact: function (str, format, provider, silent) {
        if (!format) {
            format = ["G", "g", "F", "f", "D", "d", "R", "r", "s", "U", "u", "O", "o", "Y", "y", "M", "m", "T", "t"];
        }

        if (Bridge.isArray(format)) {
            var i,
                d;

            for (i = 0; i < format.length; i++) {
                d = Bridge.Date.parseExact(str, format[i], provider, true);

                if (d != null) {
                    return d;
                }
            }

            if (silent) {
                return null;
            }

            throw new Bridge.FormatException("String does not contain a valid string representation of a date and time.");
        }

        var df = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.DateTimeFormatInfo),
            am = df.amDesignator,
            pm = df.pmDesignator,
            int = 0,
            index = 0,
            i,
            c,
            token,
            year = 0,
            month = 1,
            date = 1,
            hh = 0,
            mm = 0,
            ss = 0,
            ff = 0,
            tt = "",
            zzh = 0,
            zzm = 0,
            zzi,
            sign,
            neg,
            names,
            name,
            invalid = false,
            inQuotes = false;

        if (str == null) {
            throw new Bridge.ArgumentNullException("str");
        }

        format = format || "G";

        if (format.length == 1) {
            formats = df.getAllDateTimePatterns(format, true);
            format = formats ? formats[0] : format;
        }
        else if (format.length == 2 && format.charAt(0) == "%") {
            format = format.charAt(1);
        }

        while (index < format.length) {            
            c = format.charAt(index);
            token = "";

            while ((format.charAt(index) == c) && (index < format.length)) {
                token += c;
                index++
            }
            
            if (token == "yyyy" || token == "yy" || token == "y") {
                if (token == "yyyy") {
                    year = this.subparseInt(str, int, 4, 4);
                }
                else if (token == "yy") {
                    year = this.subparseInt(str, int, 2, 2);
                }
                else if (token == "y") {
                    year = this.subparseInt(str, int, 2, 4);
                }

                if (year == null) {
                    invalid = true;
                    break;
                }

                int += year.length;

                if (year.length == 2) {
                    year = ~~year;
                    year = (year > 30 ? 1900 : 2000) + year;
                }
            }
            else if (token == "MMM" || token == "MMMM") {
                month = 0;

                if (token === "MMM") {
                    if (this.isUseGenitiveForm(format, index, 3, "d")) {
                        names = df.abbreviatedMonthGenitiveNames;
                    }
                    else {
                        names = df.abbreviatedMonthNames;
                    }
                }
                else {
                    if (this.isUseGenitiveForm(format, index, 4, "d")) {
                        names = df.monthGenitiveNames;
                    }
                    else {
                        names = df.monthNames;
                    }
                }                
                
                for (i = 0; i < names.length; i++) {
                    name = names[i];

                    if (str.substring(int, int + name.length).toLowerCase() == name.toLowerCase()) {
                        month = (i % 12) + 1;
                        int += name.length;

                        break;
                    }
                }

                if ((month < 1) || (month > 12)) {
                    invalid = true;

                    break;
                }
            }
            else if (token == "MM" || token == "M") {
                month = this.subparseInt(str, int, token.length, 2);

                if (month == null || month < 1 || month > 12) {
                    invalid = true;

                    break;
                }

                int += month.length;
            }
            else if (token == "dddd" || token == "ddd") {
                names = token === "ddd" ? df.abbreviatedDayNames : df.dayNames;

                for (i = 0; i < names.length; i++) {
                    name = names[i];

                    if (str.substring(int, int + name.length).toLowerCase() == name.toLowerCase()) {                        
                        int += name.length;

                        break;
                    }
                }
            }
            else if (token == "dd" || token == "d") {
                date = this.subparseInt(str, int, token.length, 2);

                if (date == null || date < 1 || date > 31) {
                    invalid = true;

                    break;
                }

                int += date.length;
            }
            else if (token == "hh" || token == "h") {
                hh = this.subparseInt(str, int, token.length, 2);

                if (hh == null || hh < 1 || hh > 12) {
                    invalid = true;

                    break;
                }

                int += hh.length;
            }
            else if (token == "HH" || token == "H") {
                hh = this.subparseInt(str, int, token.length, 2);

                if (hh == null || hh < 0 || hh > 23) {
                    invalid = true;

                    break;
                }

                int += hh.length;
            }
            else if (token == "mm" || token == "m") {
                mm = this.subparseInt(str, int, token.length, 2);

                if (mm == null || mm < 0 || mm > 59) {
                    return null;
                }

                int += mm.length;
            }
            else if (token == "ss" || token == "s") {
                ss = this.subparseInt(str, int, token.length, 2);

                if (ss == null || ss < 0 || ss > 59) {
                    invalid = true;

                    break;
                }

                int += ss.length;
            }
            else if (token == "u") {
                ff = this.subparseInt(str, int, 1, 7);

                if (ff == null) {
                    invalid = true;

                    break;
                }                

                int += ff.length;

                if (ff.length > 3) {
                    ff = ff.substring(0, 3);
                }
            }
            else if (token == "fffffff" || token == "ffffff" || token == "fffff" || token == "ffff" || token == "fff" || token == "ff" || token == "f") {
                ff = this.subparseInt(str, int, token.length, 7);

                if (ff == null) {
                    invalid = true;

                    break;
                }               

                int += ff.length;

                if (ff.length > 3) {
                    ff = ff.substring(0, 3);
                }
            }
            else if (token == "t") {
                if (str.substring(int, int + 1).toLowerCase() == am.charAt(0).toLowerCase()) {
                    tt = am;
                }
                else if (str.substring(int, int + 1).toLowerCase() == pm.charAt(0).toLowerCase()) {
                    tt = pm;
                }
                else {
                    invalid = true;

                    break;
                }

                int += 1;
            }
            else if (token == "tt") {
                if (str.substring(int, int + 2).toLowerCase() == am.toLowerCase()) {
                    tt = am;
                }
                else if (str.substring(int, int + 2).toLowerCase() == pm.toLowerCase()) {
                    tt = pm;
                }
                else {
                    invalid = true;

                    break;
                }

                int += 2;
            }
            else if (token == "z" || token == "zz") {
                sign = str.charAt(int);

                if (sign == "-") {
                    neg = true;
                }
                else if (sign == "+") {
                    neg = false;
                }
                else {
                    invalid = true;

                    break;
                }

                int++;

                zzh = this.subparseInt(str, int, 1, 2);

                if (zzh == null || zzh > 14) {
                    invalid = true;

                    break;
                }

                int += zzh.length;

                if (neg) {
                    zzh = -zzh;
                }                
            }
            else if (token == "zzz") {
                name = str.substring(int, int + 6);
                int += 6;

                if (name.length != 6) {
                    invalid = true;

                    break;
                }

                sign = name.charAt(0);

                if (sign == "-") {
                    neg = true;
                }
                else if (sign == "+") {
                    neg = false;
                }
                else {
                    invalid = true;

                    break;
                }

                zzi = 1;
                zzh = this.subparseInt(name, zzi, 1, 2);

                if (zzh == null || zzh > 14) {
                    invalid = true;

                    break;
                }

                zzi += zzh.length;

                if (neg) {
                    zzh = -zzh;
                }

                if (name.charAt(zzi) != df.timeSeparator) {
                    invalid = true;

                    break;
                }

                zzi++;

                zzm = this.subparseInt(name, zzi, 1, 2);

                if (zzm == null || zzh > 59) {
                    invalid = true;

                    break;
                }                
            }
            else {
                name = str.substring(int, int + token.length);

                if ((!inQuotes && ((token == ":" && name != df.timeSeparator) ||
                    (token == "/" && name != df.dateSeparator))) ||
                    (name != token && token != "'" && token != '"' && token != "\\")) {
                    invalid = true;

                    break;
                }

                if (inQuotes == "\\") {
                    inQuotes = false;
                }

                if (token != "'" && token != '"' && token != "\\") {
                    int += token.length;
                }
                else {
                    if (inQuotes === false) {
                        inQuotes = token;
                    }
                    else {
                        if (inQuotes != token) {
                            invalid = true;
                            break;
                        }
                        inQuotes = false;
                    }
                }
            }
        }

        if (inQuotes) {
            invalid = true;
        }
        
        if (!invalid) {
            if (int != str.length) {
                invalid = true;
            }
            else if (month == 2) {
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
                    if (date > 29) {
                        invalid = true;
                    }
                }
                else if (date > 28) {
                    invalid = true;
                }
            }
            else if ((month == 4) || (month == 6) || (month == 9) || (month == 11)) {
                if (date > 30) {
                    invalid = true;
                }
            }
        }

        if (invalid) {
            if (silent) {
                return null;
            }

            throw new Bridge.FormatException("String does not contain a valid string representation of a date and time.");
        }
        
        if (hh < 12 && tt == pm) {
            hh = hh - 0 + 12;
        }
        else if (hh > 11 && tt == am) {
            hh -= 12;
        }

        if (zzh == 0 && zzm == 0) {
            return new Date(year, month - 1, date, hh, mm, ss, ff);
        }

        return new Date(Date.UTC(year, month - 1, date, hh - zzh, mm - zzm, ss, ff));
    },

    subparseInt: function (str, index, min, max) {
        var x,
            token;

        for (x = max; x >= min; x--) {
            token = str.substring(index, index + x);

            if (token.length < min) {
                return null;
            }

            if (/^\d+$/.test(token)) {
                return token;
            }
        }

        return null;
    },

    tryParse: function (value, provider, result) {
        result.v = this.parseExact(value, null, provider, true);
        
        if (result.v == null) {
            result.v = new Date(-864e13);

            return false;
        }

        return true;
    },

    tryParseExact: function (value, format, provider, result) {
        result.v = this.parseExact(value, format, provider, true);

        if (result.v == null) {
            result.v = new Date(-864e13);

            return false;
        }

        return true;
    },

    isDaylightSavingTime: function (dt) {
        var temp = Bridge.Date.today();

        temp.setMonth(0);
        temp.setDate(1);

        return temp.getTimezoneOffset() != dt.getTimezoneOffset();
    },

    toUTC: function (date) {
        return new Date(date.getUTCFullYear(), 
                        date.getUTCMonth(), 
                        date.getUTCDate(), 
                        date.getUTCHours(), 
                        date.getUTCMinutes(), 
                        date.getUTCSeconds(), 
                        date.getUTCMilliseconds());
    },

    toLocal: function (date) {
        return new Date(Date.UTC(date.getFullYear(),
                                 date.getMonth(),
                                 date.getDate(),
                                 date.getHours(),
                                 date.getMinutes(),
                                 date.getSeconds(),
                                 date.getMilliseconds()));
    }
};
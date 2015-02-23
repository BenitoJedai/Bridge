Bridge.Date = {    
    today : function() {
        var d = new Date();
        return new Date(d.getFullYear(), d.getMonth(), d.getDate());
    },

    format: function (date, format) {
        throw new Bridge.NotImplementedException();
    },

    parseExact: function (value, format) {
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
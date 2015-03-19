Bridge.Class.define('Bridge.IFormattable', {
    statics: {
        $is: function (obj) {
            if (Bridge.isNumber(obj)) {
                return true;
            }

            if (Bridge.isDate(obj)) {
                return true;
            }

            return Bridge.is(obj, Bridge.IFormattable, true);
        }
    }
});

Bridge.Class.define('Bridge.IComparable', { });

Bridge.Class.define('Bridge.IFormatProvider', {});
Bridge.Class.define('Bridge.ICloneable', {});
Bridge.Class.generic('Bridge.IComparable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IComparable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.define($$name, {
    }));
});

Bridge.Class.generic('Bridge.IEquatable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEquatable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.define($$name, {
    }));
});

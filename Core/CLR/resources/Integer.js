Bridge.Class.extend('Bridge.Int', {
    $extend: [Bridge.IComparable, Bridge.IFormattable],
    $statics: {
        instanceOf : function (instance) {
            return typeof(instance) === 'number' && isFinite(instance) && Math.round(instance, 0) == instance;
        },

        getDefaultValue : function () {
            return 0;
        },

        format : function (num, format) {            
            throw new Bridge.NotImplementedException();
            //return num.toString();
        },

        tryParse : function (str, result, min, max) {
            result.v = 0;

            if (!/^[+-]?[0-9]+$/.test(str)) {
                return false;
            }

            result.v = parseInt(str, 10);

            if (result.v < min || result.v > max) {
                return false;
            }

            return true;
        },

        trunc : function (num) {
            if (!Bridge.isNumber(num)) {
                return null;
            }

            return num > 0 ? Math.floor(num) : Math.ceil(num);
        },

        div : function (x, y) {
            if (!Bridge.isNumber(x) || !Bridge.isNumber(y)) {
                return null;
            }

            if (y === 0) {
                throw new Bridge.DivideByZeroException();
            }

            return this.trunc(x / y);
        }
    }
});

Bridge.Class.addExtend(Bridge.Int, [Bridge.IComparable$1(Bridge.Int), Bridge.IEquatable$1(Bridge.Int)]);
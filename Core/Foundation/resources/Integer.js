Bridge.Class.extend('Bridge.Int', {
    $extend: [Bridge.IComparable, Bridge.IFormattable],
    $statics: {
        instanceOf : function (instance) {
            return typeof(instance) === 'number' && isFinite(instance) && Math.round(instance, 0) == instance;
        },

        getDefaultValue : function () {
            return 0;
        },

        format : function (num, format, provider) {            
            var nf = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.NumberFormatInfo);
        },

        parseFloat: function (str, provider) {
            if (str == null) {
                throw new Bridge.ArgumentNullException("str");
            }

            var nfInfo = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.NumberFormatInfo),
                result = parseFloat(str.replace(nfInfo.numberDecimalSeparator, '.'));

            if (isNaN(result) && str !== nfInfo.naNSymbol) {
                if (str == nfInfo.negativeInfinitySymbol) {
                    return Number.NEGATIVE_INFINITY;
                }

                if (str == nfInfo.positiveInfinitySymbol) {
                    return Number.POSITIVE_INFINITY;
                }

                throw new Bridge.FormatException("Input string was not in a correct format.");
            }

            return result;
        },

        tryParseFloat: function (str, provider, result) {
            result.v = 0;
            if (str == null) {
                return false;
            }
            var nfInfo = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.NumberFormatInfo);
            result.v = parseFloat(str.replace(nfInfo.numberDecimalSeparator, '.'));

            if (isNaN(result.v) && str !== nfInfo.naNSymbol) {
                if (str == nfInfo.negativeInfinitySymbol) {
                    result.v = Number.NEGATIVE_INFINITY;
                    return true;
                }

                if (str == nfInfo.positiveInfinitySymbol) {
                    result.v = Number.POSITIVE_INFINITY;
                    return true;
                }

                return false;
            }

            return true;
        },

        parseInt: function (str, min, max, radix) {            
            if (str == null) {
                throw new Bridge.ArgumentNullException("str");
            }

            if (!/^[+-]?[0-9]+$/.test(str)) {
                throw new Bridge.FormatException("Input string was not in a correct format.");
            }
            var result = parseInt(str, radix || 10);
            if (isNaN(result)) {
                throw new Bridge.FormatException("Input string was not in a correct format.");
            }

            if (result < min || result > max) {
                throw new Bridge.OverflowException();
            }
            return result;
        },

        tryParseInt: function (str, result, min, max, radix) {
            result.v = 0;
            if (!/^[+-]?[0-9]+$/.test(str)) {
                return false;
            }
            result.v = parseInt(str, radix || 10);
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
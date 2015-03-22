
// @source resources/Task.js

Bridge.Class.define('Bridge.Exception', {
    constructor: function (message, innerException) {
        this.message = message;
        this.innerException = innerException;
        this.errorStack = new Error();
        this.data = new Bridge.Dictionary$2(Object, Object)();
    },

    getMessage: function () {
        return this.message;
    },

    getInnerException: function () {
        return this.innerException;
    },

    getStackTrace: function () {
        return this.errorStack.stack;
    },

    getData: function () {
        return this.data;
    },

    toString: function () {
        return this.getMessage();
    },

    statics: {
        create: function (error) {
            if (Bridge.is(error, Bridge.Exception)) {
                return error;
            }

            if (error instanceof TypeError) {                
                return new Bridge.NullReferenceException(error.message, new Bridge.ErrorException(error));
            }
            else if (error instanceof RangeError) {
                return new Bridge.ArgumentOutOfRangeException(null, error.message, new Bridge.ErrorException(error));
            }
            else if (error instanceof Error) {
                return new Bridge.ErrorException(o);
            }
            else {
                return new Bridge.Exception(error ? error.toString() : null);
            }
        }
    }
});

Bridge.Class.define('Bridge.ErrorException', {
    extend: [Bridge.Exception],

    constructor: function (error) {
        Bridge.Exception.prototype.$constructor.call(this, error.message);
        this.errorStack = error;
        this.error = error;
    },

    getError: function () {
        return this.error;
    }
});

Bridge.Class.define('Bridge.ArgumentException', {
    extend: [Bridge.Exception],

    constructor: function (message, paramName, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Value does not fall within the expected range.", innerException);
        this.paramName = paramName;
    },

    getParamName: function () {
        return this.paramName;
    }
});

Bridge.Class.define('Bridge.ArgumentNullException', {
    extend: [Bridge.ArgumentException],

    constructor: function (paramName, message, innerException) {
        if (!message) {
            message = 'Value cannot be null.';

            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }
        }

        Bridge.ArgumentException.prototype.$constructor.call(this, message, paramName, innerException);
    }
});

Bridge.Class.define('Bridge.ArgumentOutOfRangeException', {
    extend: [Bridge.ArgumentException],

    constructor: function (paramName, message, innerException, actualValue) {
        if (!message) {
            message = 'Value is out of range.';

            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }
        }

        Bridge.ArgumentException.prototype.$constructor.call(this, message, paramName, innerException);

        this.actualValue = actualValue;
    },

    getActualValue: function () {
        return this.actualValue;
    }
});

Bridge.Class.define('Bridge.CultureNotFoundException', {
    extend: [Bridge.ArgumentException],

    constructor: function (paramName, invalidCultureName, message, innerException) {
        if (!message) {
            message = 'Culture is not supported.';

            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }

            if (invalidCultureName) {
                message += '\n' + invalidCultureName + ' is an invalid culture identifier.';
            }            
        }

        Bridge.ArgumentException.prototype.$constructor.call(this, message, paramName, innerException);

        this.invalidCultureName = invalidCultureName;
    },

    getInvalidCultureName: function () {
        return this.invalidCultureName;
    }
});

Bridge.Class.define('Bridge.KeyNotFoundException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Key not found.", innerException);
    }
});

Bridge.Class.define('Bridge.ArithmeticException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Overflow or underflow in the arithmetic operation.", innerException);
    }
});

Bridge.Class.define('Bridge.DivideByZeroException', {
    extend: [Bridge.ArithmeticException],

    constructor: function (message, innerException) {
        Bridge.ArithmeticException.prototype.$constructor.call(this, message || "Division by 0.", innerException);
    }
});

Bridge.Class.define('Bridge.OverflowException', {
    extend: [Bridge.ArithmeticException],

    constructor: function (message, innerException) {
        Bridge.ArithmeticException.prototype.$constructor.call(this, message || "Arithmetic operation resulted in an overflow.", innerException);
    }
});

Bridge.Class.define('Bridge.FormatException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Invalid format.", innerException);
    }
});

Bridge.Class.define('Bridge.InvalidCastException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "The cast is not valid.", innerException);
    }
});

Bridge.Class.define('Bridge.InvalidOperationException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Operation is not valid due to the current state of the object.", innerException);
    }
});

Bridge.Class.define('Bridge.NotImplementedException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "The method or operation is not implemented.", innerException);
    }
});

Bridge.Class.define('Bridge.NotSupportedException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Specified method is not supported.", innerException);
    }
});

Bridge.Class.define('Bridge.NullReferenceException', {
    extend: [Bridge.Exception],

    constructor: function (message, innerException) {
        Bridge.Exception.prototype.$constructor.call(this, message || "Object is null.", innerException);
    }
});
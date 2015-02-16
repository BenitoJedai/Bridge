
// @source resources/Core.js

Bridge.nullable = {
    hasValue: function (obj) {
        return (obj !== null) && (obj !== undefined);
    },

    getValue: function (obj) {
        if (!Bridge.nullable.hasValue(obj)) {
            throw new Brdige.InvalidOperationException("Nullable instance doesn't have a value.");
        }
        return obj;
    },

    getValueOrDefault: function (obj, defValue) {
        return Bridge.nullable.hasValue(obj) ? obj : defValue;
    },

    add: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a + b : null;
    },

    band: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a & b : null;
    },

    bor: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a | b : null;
    },

    and: function (a, b) {
        if (a === true && b === true) {
            return true;
        }
        else if (a === false || b === false) {
            return false;
        }

        return null;
    },

    or: function (a, b) {
        if (a === true || b === true) {
            return true;
        }
        else if (a === false && b === false) {
            return false;
        }

        return null;
    },

    div: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a / b : null;
    },

    eq: function (a, b) {
        return !Bridge.hasValue(a) ? !Bridge.hasValue(b) : (a === b);
    },

    xor: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a ^ b : null;
    },

    gt: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) && a > b;
    },

    gte: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) && a >= b;
    },

    neq: function (a, b) {
        return !Bridge.hasValue(a) ? Bridge.hasValue(b) : (a !== b);
    },

    lt: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) && a < b;
    },

    lte: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) && a <= b;
    },

    mod: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a % b : null;
    },

    mul: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a * b : null;
    },

    sl: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a << b : null;
    },

    sr: function (a, b) {
        return Bridge.hasValue(a) && Bridge.hasValue(b) ? a >> b : null;
    },

    sub: function(a, b) {
	    return Bridge.hasValue(a) && Bridge.hasValue(b) ? a - b : null;
    },

    bnot: function (a) {
        return Bridge.hasValue(a) ? ~a : null;
    },

    neg: function (a) {
        return Bridge.hasValue(a) ? -a : null;
    },

    not: function(a) {
	    return Bridge.hasValue(a) ? !a : null;
    },    

    pos: function(a) {
	    return Bridge.hasValue(a) ? +a : null;
    },    

    lift: function() {
	    for (var i = 1; i < arguments.length; i++) {
	        if (!Bridge.hasValue(arguments[i])) {
	            return null;
	        }
	    }

	    return arguments[0].apply(null, Array.prototype.slice.call(arguments, 1));
    }
};

Bridge.hasValue = Bridge.nullable.hasValue;
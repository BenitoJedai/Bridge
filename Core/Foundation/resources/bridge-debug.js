/*
 * @version   : 1.0.0 - Bridge.NET
 * @author    : Object.NET, Inc. http://www.bridge.net/
 * @date      : 2015-03-11
 * @copyright : Copyright (c) 2008-2014, Object.NET, Inc. (http://www.object.net/). All rights reserved.
 * @license   : See license.txt and https://github.com/bridgedotnet/Bridge.NET/blob/master/LICENSE.
 */

// @source resources/Core.js

window.Bridge = {
    emptyFn: function () { },

    copy : function (to, from, keys, toIf) {
        if (typeof keys === 'string') {
            keys = keys.split(/[,;\s]+/);
        }

        for (var name, i = 0, n = keys ? keys.length : 0; i < n; i++) {
            name = keys[i];

            if (toIf !== true || to[name] === undefined) {
                if (Bridge.is(from[name], Bridge.ICloneable)) {
                    to[name] = from[name].clone();
                }
                else {
                    to[name] = from[name];
                }
            }
        }

        return to;
    },

    ns: function (ns, scope) {
        var nsParts = ns.split('.');

        if (!scope) {
            scope = window;
        }

        for (i = 0; i < nsParts.length; i++) {
            if (typeof scope[nsParts[i]] == 'undefined') {
                scope[nsParts[i]] = {};
            }

            scope = scope[nsParts[i]];
        }

        return scope;
    },

    on: function (event, elem, fn) {
        function listenHandler(e) {
            var ret = fn.apply(this, arguments);

            if (ret === false) {
                e.stopPropagation();
                e.preventDefault();
            }

            return(ret);
        }

        function attachHandler() {            
            var ret = fn.call(elem, window.event);

            if (ret === false) {
                window.event.returnValue = false;
                window.event.cancelBubble = true;
            }

            return (ret);
        }

        if (elem.addEventListener) {
            elem.addEventListener(event, listenHandler, false);
        }
        else {
            elem.attachEvent("on" + event, attachHandler);
        }
    },

    getHashCode : function (value) {
        if (Bridge.isEmpty(value, true)) {
            throw new Bridge.InvalidOperationException('HashCode cannot be calculated for empty value');
        }

        if (Bridge.isFunction(value.getHashCode)) {
            return value.getHashCode();
        }

        if (Bridge.isBoolean(value)) {
            return obj ? 1 : 0;
        }

        if (Bridge.isDate(value)) {
            return value.valueOf() & 0xFFFFFFFF;
        }

        if (Bridge.isNumber(value)) {            
            value = value.toExponential();

            return parseInt(value.substr(0, value.indexOf('e')).replace('.', ''), 10) & 0xFFFFFFFF;
        }        

        if (Bridge.isString(value)) {
            var hash = 0,
                i;

            for (i = 0; i < value.length; i++) {
                hash = (((hash << 5) - hash) + value.charCodeAt(i)) & 0xFFFFFFFF;
            }

            return hash;
        }
        
        return value.$$hashCode || (value.$$hashCode = (Math.random() * 0x100000000) | 0);
    },

    getDefaultValue : function (type) {
        if (Bridge.isFunction(type.getDefaultValue)) {
            return type.getDefaultValue();
        }
        else if (type === Boolean) {
            return false;
        }
        else if (type === Date) {
            return new Date(0);
        }
        else if (type === Number) {
            return 0;
        }

        return null;
    },

    getTypeName: function (type) {
        return type.$$name || (type.toString().match(/^\s*function\s*([^\s(]+)/) || [])[1] || "Object";
    },

    is : function (obj, type, ignoreFn) {
	    if (typeof type == "string") {
            type = Bridge.unroll(type);
	    }

        if (obj == null) {
	        return false;
        }

        if (ignoreFn !== true) {
	        if (Bridge.isFunction(type.$is)) {
	            return type.$is(obj);
	        }

	        if (Bridge.isFunction(type.instanceOf)) {
	            return type.instanceOf(obj);
	        }
        }	  

        if ((obj.constructor == type) || (obj instanceof type)) {
	        return true;
        }

        if (Bridge.isArray(obj) && type == Bridge.IEnumerable) {
            return true;
        }

        if (!type.$$inheritors) {
            return false;
        }

        var inheritors = type.$$inheritors;	  

        for (var i = 0; i < inheritors.length; i++) {
            if (Bridge.is(obj, inheritors[i])) {
	            return true;
	        }
        }

        return false;
	},
	
    as : function (obj, type) {
	    return Bridge.is(obj, type) ? obj : null;
    },
	
    cast : function(obj, type) {
	    var result = Bridge.as(obj, type);

	    if (result == null) {
	        throw new Bridge.InvalidCastException('Unable to cast type ' + Bridge.getTypeName(obj.constructor) + ' to type ' + Bridge.getTypeName(type));
	    }

	    return result;
    },
	
	apply : function (obj, values) {
	    var names = Bridge.getPropertyNames(values, false);

	    for (var i = 0; i < names.length; i++) {
	        var name = names[i];

	        if (typeof obj[name] == "function" && typeof values[name] != "function") {
	            obj[name](values[name]);
	        }
	        else {
	            obj[name] = values[name];
	        }
	    }

	    return obj;
    },

	merge: function (to, from) {
	    var object,
            key,
			i,
            value,
            toValue,
			fn;

	    if (Bridge.isArray(from) && Bridge.isFunction(to.add || to.push)) {
	        fn = Bridge.isArray(to) ? to.push : to.add;

	        for (i = 0; i < from.length; i++) {
	            fn.apply(to, from[i]);
	        }
	    }
	    else {
	        for (key in from) {
	            value = from[key];

	            if (typeof to[key] == "function" && typeof value != "function") {
	                if (key.match(/^\s*get[A-Z]/)) {
	                    Bridge.merge(to[key](), value);
	                }
	                else {
	                    to[key](value);
	                }
	            }
	            else if (value && value.constructor === Object) {
	                toValue = to[key];
	                Bridge.merge(toValue, value);
	            }
	            else {
	                to[key] = value;
	            }
	        }
	    }

	    return to;
	},

	getEnumerator: function (obj) {
	    if (obj && obj.getEnumerator) {
	      return obj.getEnumerator();
	    }

	    if ((Object.prototype.toString.call(obj) === '[object Array]') ||
            (obj && Bridge.isDefined(obj.length))) {
	      return new Bridge.ArrayEnumerator(obj);
	    }
	    
	    throw new Bridge.InvalidOperationException('Cannot create enumerator');
	},

	getPropertyNames : function(obj, includeFunctions) {
	    var names = [],
	        name;

	    for (name in obj) {
            if (includeFunctions || typeof obj[name] !== 'function') {
                names.push(name);
            }
	    }

	    return names;
	},

	isDefined: function (value, noNull) {
	    return typeof value !== 'undefined' && (noNull ? value != null : true);
	},

	isEmpty: function (value, allowEmpty) {
	    return (value == null) || (!allowEmpty ? value === '' : false) || ((!allowEmpty && Bridge.isArray(value)) ? value.length === 0 : false);
	},

	toArray: function (ienumerable) {
	    var i,
	        item,
            len,
	        result = [];

	    if (Bridge.isArray(ienumerable)) {
            for (i = 0, len = ienumerable.length; i < len; ++i) {
                result.push(ienumerable[i]);
            }
	    }
	    else {
            i = Bridge.getEnumerator(ienumerable);

            while (i.moveNext()) {
                item = i.getCurrent();
                result.push(item);
            }
	    }	    

	    return result;
	},

    isArray: function (obj) {
        return Object.prototype.toString.call(obj) === '[object Array]';
    },

    isFunction: function (obj) {
        return typeof (obj) === 'function';
    },

    isDate: function (obj) {
        return Object.prototype.toString.call(obj) === '[object Date]';
    },

    isNull: function (value) {
        return (value === null) || (value === undefined);
    },

    isBoolean: function (value) {
        return typeof value === 'boolean';
    },

    isNumber: function (value) {
        return typeof value === 'number' && isFinite(value);
    },

    isString: function (value) {
        return typeof value === 'string';
    },

    unroll: function (value) {
        var d = value.split("."),
            o = window[d[0]],
            i;

        for (var i = 1; i < d.length; i++) {
            if (!o) {
                return null;
            }

            o = o[d[i]];
        }

        return o;
    },

    equals: function (a, b) {
        if (a && Bridge.isFunction(a.equals)) {
            return a.equals(b);
        }
        else if (Bridge.isDate(a) && Bridge.isDate(b)) {
            return a.valueOf() === b.valueOf();
        }
        else if (Bridge.isNull(a) && Bridge.isNull(b)) {
            return true;
        }
        
        return a === b;
    },

    compare : function (a, b) {
        if (!Bridge.isDefined(a, true)) {
            throw new Bridge.NullReferenceException();
        }
        else if (Bridge.isNumber(a) || Bridge.isString(a) || Bridge.isBoolean(a)) {
            return a < b ? -1 : (a > b ? 1 : 0);
        }
        else if (Bridge.isDate(a)) {
            return Bridge.compare(a.valueOf(), b.valueOf());
        }

        return a.compareTo(b);
    },

    equalsT : function (a, b) {
        if (!Bridge.isDefined(a, true)) {
            throw new Bridge.NullReferenceException();
        }
        else if (Bridge.isNumber(a) || Bridge.isString(a) || Bridge.isBoolean(a)) {
            return a === b;
        }
        else if (Bridge.isDate(a)) {
            return a.valueOf() === b.valueOf();
        }
        
        return a.equalsT(b);
    },

    format: function (obj, formatString) {
        if (Bridge.isNumber(obj)) {
            return Bridge.Int.format(obj, formatString);
        }
        else if (Bridge.isDate(obj)) {
            return Bridge.Date.format(obj, formatString);
        }

        return obj.format(formatString);
    },

    getType : function (instance) {
        if (!Bridge.isDefined(instance, true)) {
            throw new Bridge.NullReferenceException('instance is null');
        }

        try {
            return instance.constructor;
        } catch (ex) {
            return Object;
        }
    },

    isLower: function isLower(c) {
        var s = String.fromCharCode(c);

        return s === s.toLowerCase() && s !== s.toUpperCase();
    },

    isUpper: function isUpper(c) {
        var s = String.fromCharCode(c);

        return s !== s.toLowerCase() && s === s.toUpperCase();
    },

    fn: {
        call: function (obj, fnName){
            var args = Array.prototype.slice.call(arguments, 2);

            return obj[fnName].apply(obj, args);
        },

        bind: function (obj, method, args, appendArgs) {
            if (method && method.$method == method && method.$scope == obj) {
                return method;
            }

            var fn = null;

            if (arguments.length === 2) {
                fn = function () {
                    return method.apply(obj, arguments)
                };
            }
            else {
                fn = function () {
                    var callArgs = args || arguments;

                    if (appendArgs === true) {
                        callArgs = Array.prototype.slice.call(arguments, 0);
                        callArgs = callArgs.concat(args);
                    }
                    else if (typeof appendArgs == 'number') {
                        callArgs = Array.prototype.slice.call(arguments, 0);

                        if (appendArgs === 0) {
                            callArgs.unshift.apply(callArgs, args);
                        }
                        else if (appendArgs < callArgs.length) {
                            callArgs.splice.apply(callArgs, [appendArgs, 0].concat(args));
                        }
                        else {
                            callArgs.push.apply(callArgs, args);
                        }
                    }

                    return method.apply(obj, callArgs);
                };
            }

            fn.$method = method;
            fn.$scope = obj;

            return fn;
        },

        bindScope: function (obj, method) {
            var fn = function () {
                var callArgs = Array.prototype.slice.call(arguments, 0);

                callArgs.unshift.apply(callArgs, [obj]);

                return method.apply(obj, callArgs);
            };

            fn.$method = method;
            fn.$scope = obj;

            return fn;
        },

        $build: function (handlers) {
            var fn = function () {
                var list = arguments.callee.$invocationList,
                    result,
                    i,
                    handler;

                for (i = 0; i < list.length; i++) {
                    handler = list[i];
                    result = handler.apply(null, arguments);
                }

                return result;
            };

            fn.$invocationList = handlers ? Array.prototype.slice.call(handlers, 0) : [];

            if (fn.$invocationList.length == 0) {
                return null;
            }

            return fn;
        },

        combine: function (fn1, fn2) {
            if (!fn1 || !fn2) {                
                return fn1 || fn2;
            }

            var list1 = fn1.$invocationList ? fn1.$invocationList : [fn1],
                list2 = fn2.$invocationList ? fn2.$invocationList : [fn2];

            return Bridge.fn.$build(list1.concat(list2));
        },

        remove: function (fn1, fn2) {
            if (!fn1 || !fn2) {
                return fn1 || null;
            }

            var list1 = fn1.$invocationList ? fn1.$invocationList : [fn1],
                list2 = fn2.$invocationList ? fn2.$invocationList : [fn2],
                result = [],
                exclude,
                i, j;
            
            for (i = list1.length - 1; i >= 0; i--) {
                exclude = false;

                for (j = 0; j < list2.length; j++) {
                    if (list1[i] === list2[j] || (list1[i].$method === list2[j].$method && list1[i].$scope === list2[j].$scope)) {
                        exclude = true;
                        break;
                    }
                }

                if (!exclude) {
                    result.push(list1[i]);
                }
            }

            result.reverse();

            return Bridge.fn.$build(result);
        }
    }
};

// @source resources/Core.js

Bridge.nullable = {
    hasValue: function (obj) {
        return (obj !== null) && (obj !== undefined);
    },

    getValue: function (obj) {
        if (!Bridge.nullable.hasValue(obj)) {
            throw new Bridge.InvalidOperationException("Nullable instance doesn't have a value.");
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

// @source resources/Class.js



// Inspired by base2 and Prototype
(function () {
    var initializing = false;

    // The base Class implementation (does nothing)
    Bridge.Class = function () { };
    Bridge.Class.cache = {};
    Bridge.Class.initCtor = function () {
        if (this.$multipleCtors && arguments.length > 0 && typeof arguments[0] == 'string' && Bridge.isFunction(this[arguments[0]])) {
            this[arguments[0]].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (this.$ctorDetector) {
            this.$ctorDetector.apply(this, arguments);
        } else if (this.$ctor) {
            this.$ctor.apply(this, arguments);
        }
    };

    // Create a new Class that inherits from this class
    Bridge.Class.extend = function (className, prop) {
        var extend = prop.$extend,
            statics = prop.$statics,
            base = extend ? extend[0].prototype : this.prototype,
            prototype,
            nameParts,
            scope = prop.$scope || window,
            i,
            name;

        delete prop.$extend;
        delete prop.$statics;

        // Instantiate a base class (but only create the instance,
        // don't run the init constructor)
        initializing = true;
        prototype = extend ? new extend[0]() : new Object();
        initializing = false;

        if (!prop.$multipleCtors && !prop.$ctor) {
            prop.$ctor = extend ? function () {
                base.$ctor();
            } : function () { };
        }

        if (!prop.$ctorMembers) {
            prop.$ctorMembers = extend ? function () {
                base.$ctorMembers.apply(this, arguments);
            } : function () { };
        }

        prop.$$ctorCtor = Bridge.Class.initCtor;

        // Copy the properties over onto the new prototype
        for (name in prop) {            
            prototype[name] = prop[name];
        }

        prototype.$$name = className;

        // The dummy class constructor
        function Class() {
            if (!(this instanceof Class)) {
                var args = Array.prototype.slice.call(arguments, 0),
                    object = Object.create(Class.prototype),
                    result = Class.apply(object, args);

                return typeof result === 'object' ? result : object;
            }

            // All construction is actually done in the init method
            if (!initializing) {
                if (this.$ctorMembers) {
                    this.$ctorMembers.apply(this, arguments);
                }                

                this.$$ctorCtor.apply(this, arguments);
            }
        }

        // Populate our constructed prototype object
        Class.prototype = prototype;

        // Enforce the constructor to be what we expect
        Class.prototype.constructor = Class;

        Class.$$name = className;

        if (statics) {
            for (name in statics) {
                Class[name] = statics[name];
            }
        }

        scope = Bridge.Class.set(scope, className, Class);

        if (!extend) {
            extend = [Object];
        }

        Class.$$extend = extend;

        for (i = 0; i < extend.length; i++) {
            scope = extend[i];

            if (!scope.$$inheritors) {
                scope.$$inheritors = [];
            }

            scope.$$inheritors.push(Class);
        }

        if (Class.$ctor) {
            Class.$ctor.call(Class);
        }

        return Class;
    };

    Bridge.Class.addExtend = function (cls, extend) {        
        Array.prototype.push.apply(cls.$$extend, extend);

        for (i = 0; i < extend.length; i++) {
            scope = extend[i];

            if (!scope.$$inheritors) {
                scope.$$inheritors = [];
            }

            scope.$$inheritors.push(cls);
        }
    };

    Bridge.Class.set = function (scope, className, cls) {
        var nameParts = className.split('.');

        for (i = 0; i < (nameParts.length - 1) ; i++) {
            if (typeof scope[nameParts[i]] == 'undefined') {
                scope[nameParts[i]] = {};
            }

            scope = scope[nameParts[i]];
        }

        scope[nameParts[nameParts.length - 1]] = cls;

        return scope;
    };

    Bridge.Class.genericName = function () {
        var name = arguments[0];
        for (var i = 1; i < arguments.length; i++) {
            name += '$' + Bridge.getTypeName(arguments[i]);
        }
        return name;
    };

    Bridge.Class.generic = function (className, scope, fn) {
        if (!fn) {
            fn = scope;
            scope = window;
        }

        Bridge.Class.set(scope, className, fn);
        return fn;
    };
})();

// @source resources/Task.js

Bridge.Class.extend('Bridge.Exception', {
    $ctor: function (message, innerException) {
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

    $statics: {
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

Bridge.Class.extend('Bridge.ErrorException', {
    $extend: [Bridge.Exception],

    $ctor: function (error) {
        Bridge.Exception.prototype.$ctor.call(this, error.message);
        this.errorStack = error;
        this.error = error;
    },

    getError: function () {
        return this.error;
    }
});

Bridge.Class.extend('Bridge.ArgumentException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, paramName, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Value does not fall within the expected range.", innerException);
        this.paramName = paramName;
    },

    getParamName: function () {
        return this.paramName;
    }
});

Bridge.Class.extend('Bridge.ArgumentNullException', {
    $extend: [Bridge.ArgumentException],

    $ctor: function (paramName, message, innerException) {
        if (!message) {
            message = 'Value cannot be null.';
            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }
        }

        Bridge.ArgumentException.prototype.$ctor.call(this, message, paramName, innerException);
    }
});

Bridge.Class.extend('Bridge.ArgumentOutOfRangeException', {
    $extend: [Bridge.ArgumentException],

    $ctor: function (paramName, message, innerException, actualValue) {
        if (!message) {
            message = 'Value is out of range.';
            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }
        }

        Bridge.ArgumentException.prototype.$ctor.call(this, message, paramName, innerException);

        this.actualValue = actualValue;
    },

    getActualValue: function () {
        return this.actualValue;
    }
});

Bridge.Class.extend('Bridge.CultureNotFoundException', {
    $extend: [Bridge.ArgumentException],

    $ctor: function (paramName, invalidCultureName, message, innerException) {
        if (!message) {
            message = 'Culture is not supported.';
            if (paramName) {
                message += '\nParameter name: ' + paramName;
            }
            if (invalidCultureName) {
                message += '\n' + invalidCultureName + ' is an invalid culture identifier.';
            }            
        }

        Bridge.ArgumentException.prototype.$ctor.call(this, message, paramName, innerException);

        this.invalidCultureName = invalidCultureName;
    },

    getInvalidCultureName: function () {
        return this.invalidCultureName;
    }
});

Bridge.Class.extend('Bridge.KeyNotFoundException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Key not found.", innerException);
    }
});

Bridge.Class.extend('Bridge.ArithmeticException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Overflow or underflow in the arithmetic operation.", innerException);
    }
});

Bridge.Class.extend('Bridge.DivideByZeroException', {
    $extend: [Bridge.ArithmeticException],

    $ctor: function (message, innerException) {
        Bridge.ArithmeticException.prototype.$ctor.call(this, message || "Division by 0.", innerException);
    }
});

Bridge.Class.extend('Bridge.OverflowException', {
    $extend: [Bridge.ArithmeticException],

    $ctor: function (message, innerException) {
        Bridge.ArithmeticException.prototype.$ctor.call(this, message || "Arithmetic operation resulted in an overflow.", innerException);
    }
});

Bridge.Class.extend('Bridge.FormatException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Invalid format.", innerException);
    }
});

Bridge.Class.extend('Bridge.InvalidCastException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "The cast is not valid.", innerException);
    }
});

Bridge.Class.extend('Bridge.InvalidOperationException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Operation is not valid due to the current state of the object.", innerException);
    }
});

Bridge.Class.extend('Bridge.NotImplementedException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "The method or operation is not implemented.", innerException);
    }
});

Bridge.Class.extend('Bridge.NotSupportedException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Specified method is not supported.", innerException);
    }
});

Bridge.Class.extend('Bridge.NullReferenceException', {
    $extend: [Bridge.Exception],

    $ctor: function (message, innerException) {
        Bridge.Exception.prototype.$ctor.call(this, message || "Object is null.", innerException);
    }
});
Bridge.Class.extend('Bridge.IFormattable', {
    $statics: {
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

Bridge.Class.extend('Bridge.IComparable', { });

Bridge.Class.extend('Bridge.IFormatProvider', {});
Bridge.Class.extend('Bridge.ICloneable', {});
Bridge.Class.generic('Bridge.IComparable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IComparable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
    }));
});

Bridge.Class.generic('Bridge.IEquatable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEquatable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
    }));
});

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

Bridge.Class.extend("Bridge.NumberFormatInfo", {
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
Bridge.Date = {    
    today : function() {
        var d = new Date();
        return new Date(d.getFullYear(), d.getMonth(), d.getDate());
    },

    format: function (date, format) {
        throw new Bridge.NotImplementedException();
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
Bridge.Class.extend('Bridge.TimeSpan', {
    $extend: [Bridge.IComparable],
    $statics: {
        fromDays: function (value) {
            return new Bridge.TimeSpan(value * 864e9);
        },

        fromHours: function (value) {
            return new Bridge.TimeSpan(value * 36e9);
        },

        fromMilliseconds: function (value) {
            return new Bridge.TimeSpan(value * 1e4);
        },

        fromMinutes: function (value) {
            return new Bridge.TimeSpan(value * 6e8);
        },

        fromSeconds: function (value) {
            return new Bridge.TimeSpan(value * 1e7);
        },

        fromTicks: function (value) {
            return new Bridge.TimeSpan(value);
        },

        $ctor: function () {
            this.zero = new Bridge.TimeSpan(0);
            this.maxValue = new Bridge.TimeSpan(864e13);
            this.minValue = new Bridge.TimeSpan(-864e13);
        },

        getDefaultValue: function () {
            return new Bridge.TimeSpan(0);
        }
    },

    $ctorMembers : function () {
        this.ticks = 0;
    },

    $ctor: function () {
        if (arguments.length == 1) {
            this.ticks = arguments[0];
        }
        else if (arguments.length == 3) {
            this.ticks = (((arguments[0] * 60 + arguments[1]) * 60) + arguments[2]) * 1e7;
        }
        else if (arguments.length == 4) {
            this.ticks = ((((arguments[0] * 24 + arguments[1]) * 60 + arguments[2]) * 60) + arguments[3]) * 1e7;
        }
        else if (arguments.length == 5) {
            this.ticks = (((((arguments[0] * 24 + arguments[1]) * 60 + arguments[2]) * 60) + arguments[3]) * 1e3 + arguments[4]) * 1e4;
        }
    },

    getTicks: function () {
        return this.ticks;
    },

    getDays: function () {
        return this.ticks / 864e9 | 0;
    },

    getHours: function () {
        return this.ticks / 36e9 % 24 | 0;
    },

    getMilliseconds: function () {
        return this.ticks / 1e4 % 1e3 | 0;
    },

    getMinutes: function () {
        return this.ticks / 6e8 % 60 | 0;
    },

    getSeconds: function () {
        return this.ticks / 1e7 % 60 | 0;
    },

    getTotalDays: function () {
        return this.ticks / 864e9;
    },

    getTotalHours: function () {
        return this.ticks / 36e9;
    },

    getTotalMilliseconds: function () {
        return this.ticks / 1e4;
    },

    getTotalMinutes: function () {
        return this.ticks / 6e8;
    },

    getTotalSeconds: function () {
        return this.ticks / 1e7;
    },

    get12HourHour: function () {
        return (this.getHours() > 12) ? this.getHours() - 12 : (this.getHours() === 0) ? 12 : this.getHours();
    },

    add: function (ts) {
        return new Bridge.TimeSpan(this.ticks + ts.ticks);
    },

    subtract: function (ts) {
        return new Bridge.TimeSpan(this.ticks - ts.ticks);
    },

    duration: function () {
        return new Bridge.TimeSpan(Math.abs(this.ticks));
    },

    negate: function () {
        return new Bridge.TimeSpan(-this.ticks);
    },

    compareTo: function (other) {
        return this.ticks < other.ticks ? -1 : (this.ticks > other.ticks ? 1 : 0);
    },

    equals: function (other) {
        return other.ticks === this.ticks;
    },

    toString: function (formatStr, provider) {
        var ticks = this.ticks,
            result = "",
            me = this,
            dtInfo = (provider || Bridge.CultureInfo.getCurrentCulture()).getFormat(Bridge.DateTimeFormatInfo),
            format = function (t, n) {
                return Bridge.String.alignString((t | 0).toString(), n || 2, "0", 2);
            };

        if (formatStr) {            
            return formatStr.replace(/dd?|HH?|hh?|mm?|ss?|tt?/g,
                function (formatStr) {                    
                    switch (formatStr) {
                        case "d":
                            return me.getDays();
                        case "dd":
                            return format(me.getDays());
                        case "H":
                            return me.getHours();
                        case "HH":
                            return format(me.getHours());
                        case "h":
                            return me.get12HourHour();
                        case "hh":
                            return format(me.get12HourHour());
                        case "m":
                            return me.getMinutes();
                        case "mm":
                            return format(me.getMinutes());
                        case "s":
                            return me.getSeconds();
                        case "ss":
                            return format(me.getSeconds());
                        case "t":
                            return ((me.getHours() < 12) ? dtInfo.amDesignator : dtInfo.pmDesignator).substring(0, 1);
                        case "tt":
                            return (me.getHours() < 12) ? dtInfo.amDesignator : dtInfo.pmDesignator;
                    }
                }
            );
        }        
        
        if (Math.abs(ticks) >= 864e9) {
            result += format(ticks / 864e9) + ".";
            ticks %= 864e9;
        }

        result += format(ticks / 36e9) + ":";
        ticks %= 36e9;
        result += format(ticks / 6e8 | 0) + ':';
        ticks %= 6e8;
        result += format(ticks / 1e7);
        ticks %= 1e7;

        if (ticks > 0) {
            result += "." + format(ticks, 7);
        }

        return result;
    }
});

Bridge.Class.addExtend(Bridge.TimeSpan, [Bridge.IComparable$1(Bridge.TimeSpan), Bridge.IEquatable$1(Bridge.TimeSpan)]);
Bridge.String = {
    format : function (format) {
        var _formatRe = /\{\{|\}\}|\{(\d+)(?:,(-?\d+))?(?:\:([\w\s\.]*))?\}/g,
            args = Array.prototype.slice.call(arguments, 1);

        return format.replace(_formatRe, function (m, idx, alignment, formatStr) {
            if (m === "{{" || m === "}}") {
                return m.charAt(0);
            }

            var replaceValue = args[parseInt(idx, 10)],
                values,
                match;

            if (!Bridge.isDefined(replaceValue, true)) {
                return "";
            }

            if (alignment) {
                alignment = parseInt(alignment, 10);
                if (!Bridge.isNumber(alignment)) {
                    alignment = null;
                }
            }

            if (formatStr && Bridge.is(replaceValue, Bridge.IFormattable)) {
                values = [replaceValue];

                return Bridge.String.alignString(Bridge.format(replaceValue, formatStr), alignment);
            }

            return Bridge.String.alignString(replaceValue.toString(), alignment);
        });
    },

    alignString : function (str, alignment, pad, dir) {
        if (!alignment) {
            return str;
        }

        if (!pad) {
            pad = " ";
        }

        if (!dir) {
            dir = alignment < 0 ? 2 : 1;
        }

        alignment = Math.abs(alignment);

        if (alignment + 1 >= str.length) {
            switch (dir) {
                case 2:
                    str = Array(alignment + 1 - str.length).join(pad) + str;
                    break;

                case 3:
                    var padlen = alignment - str.length,
                        right = Math.ceil(padlen / 2),
                        left = padlen - right;

                    str = Array(left + 1).join(pad) + str + Array(right + 1).join(pad);
                    break;

                case 1:
                default:
                    str = str + Array(alignment + 1 - str.length).join(pad);
                    break;
            }
        }

        return str;
    },

    startsWith: function (str, prefix) {
        if (!prefix.length) {
            return true;
        }

        if (prefix.length > str.length) {
            return false;
        }

        return str.match("^" + prefix) !== null;
    },

    endsWith: function (str, suffix) {
        if (!suffix.length) {
            return true;
        }

        if (suffix.length > str.length) {
            return false;
        }

        return str.match(suffix + "$") !== null;
    }
};

// @source resources/Browser.js

(function () {
  var check = function (regex) {
    return regex.test(navigator.userAgent);
  },

  isStrict = document.compatMode == 'CSS1Compat',

  version = function (is, regex) {
    var m;

    return (is && (m = regex.exec(navigator.userAgent))) ? parseFloat(m[1]) : 0;
  },

  docMode = document.documentMode,
  isOpera = check(/opera/),
  isOpera10_5 = isOpera && check(/version\/10\.5/),
  isChrome = check(/\bchrome\b/),
  isWebKit = check(/webkit/),
  isSafari = !isChrome && check(/safari/),
  isSafari2 = isSafari && check(/applewebkit\/4/),
  isSafari3 = isSafari && check(/version\/3/),
  isSafari4 = isSafari && check(/version\/4/),
  isSafari5_0 = isSafari && check(/version\/5\.0/),
  isSafari5 = isSafari && check(/version\/5/),
  isIE = !isOpera && (check(/msie/) || check(/trident/)),
  isIE7 = isIE && ((check(/msie 7/) && docMode != 8 && docMode != 9 && docMode != 10) || docMode == 7),
  isIE8 = isIE && ((check(/msie 8/) && docMode != 7 && docMode != 9 && docMode != 10) || docMode == 8),
  isIE9 = isIE && ((check(/msie 9/) && docMode != 7 && docMode != 8 && docMode != 10) || docMode == 9),
  isIE10 = isIE && ((check(/msie 10/) && docMode != 7 && docMode != 8 && docMode != 9) || docMode == 10),
  isIE11 = isIE && ((check(/trident\/7\.0/) && docMode != 7 && docMode != 8 && docMode != 9 && docMode != 10) || docMode == 11),
  isIE6 = isIE && check(/msie 6/),
  isGecko = !isWebKit && !isIE && check(/gecko/),
  isGecko3 = isGecko && check(/rv:1\.9/),
  isGecko4 = isGecko && check(/rv:2\.0/),
  isGecko5 = isGecko && check(/rv:5\./),
  isGecko10 = isGecko && check(/rv:10\./),
  isFF3_0 = isGecko3 && check(/rv:1\.9\.0/),
  isFF3_5 = isGecko3 && check(/rv:1\.9\.1/),
  isFF3_6 = isGecko3 && check(/rv:1\.9\.2/),
  isWindows = check(/windows|win32/),
  isMac = check(/macintosh|mac os x/),
  isLinux = check(/linux/),
  scrollbarSize = null,
  chromeVersion = version(true, /\bchrome\/(\d+\.\d+)/),
  firefoxVersion = version(true, /\bfirefox\/(\d+\.\d+)/),
  ieVersion = version(isIE, /msie (\d+\.\d+)/),
  operaVersion = version(isOpera, /version\/(\d+\.\d+)/),
  safariVersion = version(isSafari, /version\/(\d+\.\d+)/),
  webKitVersion = version(isWebKit, /webkit\/(\d+\.\d+)/),
  isSecure = /^https/i.test(window.location.protocol),
  isiPhone = /iPhone/i.test(navigator.platform),
  isiPod = /iPod/i.test(navigator.platform),
  isiPad = /iPad/i.test(navigator.userAgent),
  isBlackberry = /Blackberry/i.test(navigator.userAgent),
  isAndroid = /Android/i.test(navigator.userAgent),
  isDesktop = isMac || isWindows || (isLinux && !isAndroid),
  isTablet = isiPad,
  isPhone = !isDesktop && !isTablet;

  Bridge.Browser = {
    isStrict: isStrict,
    isIEQuirks: isIE && (!isStrict && (isIE6 || isIE7 || isIE8 || isIE9)),
    isOpera: isOpera,
    isOpera10_5: isOpera10_5,
    isWebKit: isWebKit,
    isChrome: isChrome,
    isSafari: isSafari,
    isSafari3: isSafari3,
    isSafari4: isSafari4,
    isSafari5: isSafari5,
    isSafari5_0: isSafari5_0,
    isSafari2: isSafari2,
    isIE: isIE,
    isIE6: isIE6,
    isIE7: isIE7,
    isIE7m: isIE6 || isIE7,
    isIE7p: isIE && !isIE6,
    isIE8: isIE8,
    isIE8m: isIE6 || isIE7 || isIE8,
    isIE8p: isIE && !(isIE6 || isIE7),
    isIE9: isIE9,
    isIE9m: isIE6 || isIE7 || isIE8 || isIE9,
    isIE9p: isIE && !(isIE6 || isIE7 || isIE8),
    isIE10: isIE10,
    isIE10m: isIE6 || isIE7 || isIE8 || isIE9 || isIE10,
    isIE10p: isIE && !(isIE6 || isIE7 || isIE8 || isIE9),
    isIE11: isIE11,
    isIE11m: isIE6 || isIE7 || isIE8 || isIE9 || isIE10 || isIE11,
    isIE11p: isIE && !(isIE6 || isIE7 || isIE8 || isIE9 || isIE10),
    isGecko: isGecko,
    isGecko3: isGecko3,
    isGecko4: isGecko4,
    isGecko5: isGecko5,
    isGecko10: isGecko10,
    isFF3_0: isFF3_0,
    isFF3_5: isFF3_5,
    isFF3_6: isFF3_6,
    isFF4: 4 <= firefoxVersion && firefoxVersion < 5,
    isFF5: 5 <= firefoxVersion && firefoxVersion < 6,
    isFF10: 10 <= firefoxVersion && firefoxVersion < 11,
    isLinux: isLinux,
    isWindows: isWindows,
    isMac: isMac,
    chromeVersion: chromeVersion,
    firefoxVersion: firefoxVersion,
    ieVersion: ieVersion,
    operaVersion: operaVersion,
    safariVersion: safariVersion,
    webKitVersion: webKitVersion,
    isSecure: isSecure,
    isiPhone: isiPhone,
    isiPod: isiPod,
    isiPad: isiPad,
    isBlackberry: isBlackberry,
    isAndroid: isAndroid,
    isDesktop: isDesktop,
    isTablet: isTablet,
    isPhone: isPhone,
    iOS: isiPhone || isiPad || isiPod,
    standalone: !!window.navigator.standalone
  };
})();
Bridge.Class.extend('Bridge.IEnumerable', {});
Bridge.Class.extend('Bridge.IEnumerator', {});
Bridge.Class.extend('Bridge.IEqualityComparer', {});
Bridge.Class.extend('Bridge.ICollection', {
    $extend: [Bridge.IEnumerable]
});

Bridge.Class.generic('Bridge.IEnumerator$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEnumerator$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerator]
    }));
});

Bridge.Class.generic('Bridge.IEnumerable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEnumerable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable]
    }));
});

Bridge.Class.generic('Bridge.ICollection$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.ICollection$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable$1(T)]
    }));
});

Bridge.Class.generic('Bridge.IEqualityComparer$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEqualityComparer$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEqualityComparer]
    }));
});

Bridge.Class.generic('Bridge.IDictionary$2', function (TKey, TValue) {
    var $$name = Bridge.Class.genericName('Bridge.IDictionary$2', TKey, TValue);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable$1(Bridge.KeyValuePair$2(TKey, TValue))],
    }));
});

Bridge.Class.extend("Bridge.CustomEnumerator", {
    $extend: [Bridge.IEnumerator],

    $ctor: function (moveNext, getCurrent, reset, dispose, scope) {
        this.$moveNext = moveNext;
        this.$getCurrent = getCurrent;
        this.$dispose = dispose;
        this.$reset = reset;
        this.scope = scope;
    },

    moveNext: function () {
        try {
            return this.$moveNext.call(this.scope);
        }
        catch (ex) {
            this.dispose.call(this.scope);
            throw ex;
        }
    },

    getCurrent: function () {
        return this.$getCurrent.call(this.scope);
    },

    reset: function () {
        if (this.$reset) {
            this.$reset.call(this.scope);
        }
    },

    dispose: function () {
        if (this.$dispose)
            this.$dispose.call(this.scope);
    }
});

Bridge.Class.extend('Bridge.ArrayEnumerator', {
    $ctor: function (array) {
        this.array = array;
        this.reset();
    },

    moveNext: function () {
        this.index++;
        return this.index < this.array.length;
    },

    getCurrent: function () {
        return this.array[this.index];
    },

    reset: function () {
        this.index = -1;
    }
});
Bridge.Class.generic('Bridge.EqualityComparer$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.EqualityComparer$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEqualityComparer$1(T)],

        equals: function (x, y) {
            if (!Bridge.isDefined(x, true)) {
                return !Bridge.isDefined(y, true);
            }
            else {
                return Bridge.isDefined(y, true) ? Bridge.equals(x, y) : false;
            }
        },

        getHashCode: function (obj) {
            return Bridge.isDefined(obj, true) ? Bridge.getHashCode(obj) : 0;
        }
    }));
});

Bridge.EqualityComparer$1.default = new Bridge.EqualityComparer$1(Object)();
Bridge.Class.generic('Bridge.KeyValuePair$2', function (TKey, TValue) {
    var $$name = Bridge.Class.genericName('Bridge.KeyValuePair$2', TKey, TValue);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $ctor: function (key, value) {
            this.key = key;
            this.value = value;
        }
    }));
});

Bridge.Class.generic('Bridge.Dictionary$2', function (TKey, TValue) {
    var $$name = Bridge.Class.genericName('Bridge.Dictionary$2', TKey, TValue);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IDictionary$2(TKey, TValue)],

        $ctor: function (obj, comparer) {
            this.comparer = comparer || Bridge.EqualityComparer$1.default;
            this.clear();

            if (Bridge.is(obj, Bridge.Dictionary$2(TKey, TValue))) {
                var e = Bridge.getEnumerator(obj),
                    c;

                while (e.moveNext()) {
                    c = e.getCurrent();
                    this.add(c.key, c.value);
                }
            }
            else if (Object.prototype.toString.call(obj) === '[object Object]') {
                var names = Bridge.getPropertyNames(obj),
                    name;
                for (var i = 0; i < names.length; i++) {
                    name = names[i];
                    this.add(name, obj[name]);
                }
            }  
        },

        getKeys: function () {
            return new Bridge.DictionaryCollection$1(TKey)(this, true);
        },

        getValues: function () {
            return new Bridge.DictionaryCollection$1(TKey)(this, false);
        },

        clear: function () {
            this.entries = {};
            this.count = 0;
        },

        findEntry : function (key) {
            var hash = this.comparer.getHashCode(key),
                entries,
                i;

            if (Bridge.isDefined(this.entries[hash])) {
                entries = this.entries[hash];
                for (i = 0; i < entries.length; i++) {
                    if (this.comparer.equals(entries[i].key, key)) {
                        return entries[i];
                    }
                }
            }
        },

        containsKey: function (key) {
            return !!this.findEntry(key);
        },

        containsValue: function (value) {
            var e, i;

            for (e in this.entries) {
                if (this.entries.hasOwnProperty(e)) {
                    var entries = this.entries[e];
                    for (i = 0; i < entries.length; i++) {
                        if (this.comparer.equals(entries[i].value, value)) {
                            return true;
                        }
                    }
                }
            }

            return false;
        },

        get: function (key) {
            var entry = this.findEntry(key);

            if (!entry) {
                throw new Bridge.KeyNotFoundException('Key ' + key + ' does not exist.');
            }

            return entry.value;
        },

        set: function (key, value, add) {
            var entry = this.findEntry(key),
                hash;

            if (entry) {
                if (add) {
                    throw new Bridge.ArgumentException('Key ' + key + ' already exists.');
                }

                entry.value = value;
                return;
            }

            hash = this.comparer.getHashCode(key);
            entry = new Bridge.KeyValuePair$2(TKey, TValue)(key, value);

            if (this.entries[hash]) {
                this.entries[hash].push(entry);
            }
            else {
                this.entries[hash] = [entry];
            }            

            this.count++;
        },

        add: function (key, value) {
            this.set(key, value, true);
        },

        remove: function (key) {
            var hash = this.comparer.getHashCode(key),
                entries,
                i;

            if (!this.entries[hash]) {
                return false;
            }

            entries = this.entries[hash];
            for (i = 0; i < entries.length; i++) {
                if (this.comparer.equals(entries[i].key, key)) {
                    entries.splice(i, 1);
                    if (entries.length == 0) {
                        delete this.entries[hash];
                    }
                    this.count--;
                    return true;
                }
            }

            return false;
        },

        getCount: function () {
            return this.count;
        },

        getComparer: function () {
            return this.comparer;
        },

        tryGetValue: function (key, value) {
            var entry = this.findEntry(key);
            value.v = entry ? entry.value : Bridge.getDefaultValue(TValue);
            return !!entry;
        },

        getCustomEnumerator: function (fn) {
            var hashes = Bridge.getPropertyNames(this.entries),
                hashIndex = -1,
                keyIndex;

            return new Bridge.CustomEnumerator(function () {
                if (hashIndex < 0 || keyIndex >= (this.entries[hashes[hashIndex]].length - 1)) {
                    keyIndex = -1;
                    hashIndex++;
                }
                if (hashIndex >= hashes.length) {
                    return false;
                }
                    
                keyIndex++;
                return true;
            }, function () {
                return fn(this.entries[hashes[hashIndex]][keyIndex]);
            }, function () {
                hashIndex = -1;
            }, null, this);
        },

        getEnumerator: function () {
            return new Bridge.DictionaryEnumerator(this.entries);
        }
    }));
});

Bridge.Class.generic('Bridge.DictionaryCollection$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.DictionaryCollection$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.ICollection$1(T)],

        $ctor: function (dictionary, keys) {
            this.dictionary = dictionary;
            this.keys = keys;
        },

        getCount: function () {
            return this.dictionary.getCount();
        },

        getEnumerator: function () {
            return this.dictionary.getCustomEnumerator(this.keys ? function (e) {
                return e.key;
            } : function (e) {
                return e.value;
            });
        },

        contains: function (value) {
            return this.keys ? this.dictionary.containsKey(value) : this.dictionary.containsValue(value);
        },

        add: function (v) {
            throw new Bridge.NotSupportedException();
        },
        
        clear: function () {
            throw new Bridge.NotSupportedException();
        },

        remove: function () {
            throw new Bridge.NotSupportedException();
        }
    }));
});
Bridge.Class.generic('Bridge.List$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.List$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.ICollection$1(T), Bridge.ICollection],
        $ctor: function (obj) {
            if (Object.prototype.toString.call(obj) === '[object Array]') {
                this.items = obj;
            }
            else if (Bridge.is(obj, Bridge.IEnumerable)) {
                this.items = Bridge.toArray(obj);
            }
            else {
                this.items = [];
            }
        },

        checkIndex: function (index) {
            if (index < 0 || index > (this.items.length - 1)) {
                throw new Bridge.ArgumentOutOfRangeException('Index out of range');
            }
        },

        getCount: function () {
            return this.items.length;
        },

        get: function (index) {
            this.checkIndex(index);

            return this.items[index];
        },

        set: function (index, value) {
            this.checkIndex(index);
            this.items[index] = value;
        },

        add: function (value) {
            this.items.push(value);
        },

        addRange: function (items) {
            var array = Bridge.toArray(items),
                i,
                len;

            for (i = 0, len = array.length; i < len; ++i) {
                this.items.push(array[i]);
            }
        },

        clear: function () {
            this.items = [];
        },

        indexOf: function (item, startIndex) {
            var i;

            if (!Bridge.isDefined(startIndex)) {
                startIndex = 0;
            }

            if (startIndex != 0) {
                this.checkIndex(index);
            }

            for (i = startIndex; i < this.items.length; i++) {
                if (item === this.items[i]) {
                    return i;
                }
            }

            return -1;
        },

        contains: function (item) {
            return this.indexOf(item) > -1;
        },

        getEnumerator: function () {
            return new Bridge.ArrayEnumerator(this.items);
        },

        getRange: function (index, count) {
            if (!Bridge.isDefined(index)) {
                index = 0;
            }

            if (!Bridge.isDefined(count)) {
                count = this.items.length;
            }

            if (index != 0) {
                this.checkIndex(index);
            }

            this.checkIndex(index + count - 1);

            var result = [],
                i;

            for (i = index; i < count; i++) {
                result.push(this.items[i]);
            }

            return result;
        },

        insert: function (index, item) {
            if (index != 0) {
                this.checkIndex(index);
            }


            if (Bridge.isArray(item)) {
                for (var i = 0; i < item.length; i++) {
                    this.insert(index++, item[i]);
                }
            }
            else {
                this.items.splice(index, 0, item);
            }
        },

        join: function (delimeter) {
            return this.items.join(delimeter);
        },

        lastIndexOf: function (item, fromIndex) {
            if (!Bridge.isDefined(fromIndex)) {
                fromIndex = this.items.length - 1;
            }

            if (fromIndex != 0) {
                this.checkIndex(fromIndex);
            }

            for (var i = fromIndex; i >= 0; i--) {
                if (item === this.items[i]) {
                    return i;
                }
            }

            return -1;
        },

        remove: function (item) {
            var index = this.indexOf(item);

            this.checkIndex(index);
            this.items.splice(index, 1);
        },

        removeAt: function (index) {
            this.checkIndex(index);
            this.items.splice(index, 1);
        },

        removeRange: function (index, count) {
            this.checkIndex(index);
            this.items.splice(index, count);
        },

        reverse: function () {
            this.items.reverse();
        },

        slice: function (start, end) {
            this.items.slice(start, end);
        },

        sort: function (comparison) {
            this.items.sort(comparison);
        },

        splice: function (start, count, items) {
            this.items.splice(start, count, items);
        },

        unshift: function () {
            this.items.unshift();
        },

        toArray: function () {
            return Bridge.toArray(this);
        }
    }));
});


// @source resources/Task.js

Bridge.Class.extend('Bridge.Task', {
    $ctor: function (action, state) {
        this.action = action;
        this.state = state;
        this.error = null;
        this.status = Bridge.TaskStatus.created;
        this.callbacks = [];
        this.result = null;
    },

    $statics: {
        delay : function (delay, state) {
            var task = new Bridge.Task();

            setTimeout(function () {
                task.setResult(state);
            }, delay);

            return task;
        },

        fromResult : function (result) {
            var task = new Bridge.Task();
            t.status = Bridge.TaskStatus.ranToCompletion;
            t.result = result;

            return t;
        },

        run : function (fn) {
            var task = new Bridge.Task();

            setTimeout(function () {
                try {
                    task.setResult(fn());
                }
                catch (e) {
                    task.setError(e);
                }
            }, 0);

            return task;
        },

        whenAll : function (tasks) {
            var task = new Bridge.Task(),
                result,
                executing = tasks.length, 
                cancelled = false, 
                errors = [],
                i;

            if (!Bridge.isArray(tasks)) {
                tasks = Array.prototype.slice.call(arguments, 0);
            }

            if (tasks.length === 0) {
                task.setResult([]);

                return task;
            }

            result = new Array(tasks.length);

            for (i = 0; i < tasks.length; i++) {                
                tasks[i].$index = i;
                tasks[i].continueWith(function (t) {
                    switch (t.status) {
                        case Bridge.TaskStatus.ranToCompletion:
                            result[t.$index] = t.getResult();
                            break;
                        case Bridge.TaskStatus.canceled:
                            cancelled = true;
                            break;
                        case Bridge.TaskStatus.faulted:
                            errors.push(t.error);                                
                            break;
                        default:
                            throw new Bridge.InvalidOperationException('Invalid task status: ' + t.status);
                    }

                    executing--;

                    if (!executing) {
                        if (errors.length > 0) {
                            task.setError(errors);
                        }
                        else if (cancelled) {
                            task.setCanceled();
                        }
                        else {
                            task.setResult(result);
                        }
                    }
                });
            }

            return task;
        },

        whenAny : function (tasks) {
            if (!Bridge.isArray(tasks)) {
                tasks = Array.prototype.slice.call(arguments, 0);
            }

            if (!tasks.length) {
                throw new Bridge.ArgumentException('At least one task is required');
            }

            var task = new Bridge.Task(),
                i;

            for (i = 0; i < tasks.length; i++) {
                tasks[i].continueWith(function (t) {
                    switch (t.status) {
                        case Bridge.TaskStatus.ranToCompletion:
                            task.complete(t);
                            break;
                        case Bridge.TaskStatus.canceled:
                            task.cancel();
                            break;
                        case Bridge.TaskStatus.faulted:
                            task.fail(t.error);
                            break;
                        default:
                            throw new Bridge.InvalidOperationException('Invalid task status: ' + t.status);
                    }
                });
            }

            return task;
        },

        fromCallback : function (target, method) {
            var task = new Bridge.Task(),
                args = Array.prototype.slice.call(arguments, 2),
                callback;

            callback = function (value) {
                task.setResult(value);
            };
	
            args.push(callback);

            target[method].apply(target, args);

            return task;
        },

        fromCallbackResult: function (target, method, resultHandler) {
            var task = new Bridge.Task(),
                args = Array.prototype.slice.call(arguments, 3),
                callback;

            callback = function (value) {
                task.setResult(value);
            };

            resultHandler(args, callback);

            target[method].apply(target, args);

            return task;
        },

        fromCallbackOptions: function (target, method, name) {
            var task = new Bridge.Task(),
                args = Array.prototype.slice.call(arguments, 3),
                callback;

            callback = function (value) {
                task.setResult(value);
            };

            args[0] = args[0] || {};
            args[0][name] = callback;

            target[method].apply(target, args);

            return task;
        },

        fromPromise : function (promise, handler) {
            var task = new Bridge.Task();

            if (!promise.then) {
                promise = promise.promise();
            }

            promise.then(function() {
                task.setResult(handler ? handler.apply(null, arguments) : arguments);
            }, function() {
                task.setError(new Error(Array.prototype.slice.call(arguments, 0)));
            });

            return task;
        }
    },

    continueWith: function (continuationAction) {
        var task = new Bridge.Task(),
            me = this,
            fn = function() {
                try {
                    task.setResult(continuationAction(me));
                }
                catch (e) {
                    task.setError(e);
                }
            };

        if (this.isCompleted()) {
            setTimeout(fn, 0);
        }
        else {
            this.callbacks.push(fn);
        }

        return task;
    },

    start: function () {
        if (this.status !== Bridge.TaskStatus.created) {
            throw new Error('Task was already started.');
        }

        var me = this;
        this.status = Bridge.TaskStatus.running;

        setTimeout(function() {
            try {
                var result = me.action(me.state);
                delete me.action;
                delete me.state;
                me.complete(result);
            }
            catch (e) {
                me.fail(e);
            }
        }, 0);
    },

    runCallbacks: function () {
        for (var i = 0; i < this.callbacks.length; i++) {
            this.callbacks[i](this);
        }

        delete this.callbacks;
    },

    complete: function (result) {
        if (this.isCompleted()) {
            return false;
        }

        this.result = result;
        this.status = Bridge.TaskStatus.ranToCompletion;
        this.runCallbacks();

        return true;
    },

    fail: function (error) {
        if (this.isCompleted()) {
            return false;
        }

        this.error = error;
        this.status = Bridge.TaskStatus.faulted;
        this.runCallbacks();

        return true;
    },

    cancel: function () {
        if (this.isCompleted()) {
            return false;
        }

        this.status = Bridge.TaskStatus.canceled;
        this.runCallbacks();

        return true;
    },

    isCanceled: function () {
        return this.status === Bridge.TaskStatus.canceled;
    },

    isCompleted: function () {
        return this.status == Bridge.TaskStatus.ranToCompletion || this.status == Bridge.TaskStatus.canceled || this.status == Bridge.TaskStatus.faulted;
    },

    isFaulted: function () {
        return this.status === Bridge.TaskStatus.faulted;
    },

    getResult: function () {
        switch (this.status) {
            case Bridge.TaskStatus.ranToCompletion:
                return this.result;
            case Bridge.TaskStatus.canceled:
                throw new Error('Task was cancelled.');
            case Bridge.TaskStatus.faulted:
                throw this.error;
            default:
                throw new Error('Task is not yet completed.');
        }
    },

    setCanceled: function () {
        if (!this.cancel()) {
            throw new Error('Task was already completed.');
        }
    },

    setResult: function (result) {
        if (!this.complete(result)) {
            throw new Error('Task was already completed.');
        }
    },

    setError: function (error) {
        if (!this.fail(error)) {
            throw new Error('Task was already completed.');
        }
    },
                                        
    dispose: function () {
    },

    getAwaiter: function () {
        return this;
    }
});

Bridge.Class.extend('Bridge.TaskStatus', {
    $statics: {
        created: 0,
        waitingForActivation: 1,
        waitingToRun: 2,
        running: 3,
        waitingForChildrenToComplete: 4,
        ranToCompletion: 5,
        canceled: 6,
        faulted: 7
    }
});
Bridge.Validation = {
    isNull: function (value) {
        return !Bridge.isDefined(value, true);
    },

    isEmpty: function (value) {
        return value == null || value.length === 0 || Bridge.is(value, Bridge.ICollection) ? value.getCount() == 0 : false;
    },

    isNotEmptyOrWhitespace: function (value) {
        return Bridge.isDefined(value, true) && !(/^$|\s+/.test(value));
    },

    isNotNull: function (value) {
        return Bridge.isDefined(value, true);
    },

    isNotEmpty: function (value) {
        return !Bridge.Validation.isEmpty(value);
    },

    email : function (value) {
        var re = /^(")?(?:[^\."])(?:(?:[\.])?(?:[\w\-!#$%&'*+/=?^_`{|}~]))*\1@(\w[\-\w]*\.){1,5}([A-Za-z]){2,6}$/;

        return re.test(value);
    },

    url: function (value) {
        var re = /(((^https?)|(^ftp)):\/\/((([\-\w]+\.)+\w{2,3}(\/[%\-\w]+(\.\w{2,})?)*(([\w\-\.\?\\\/+@&#;`~=%!]*)(\.\w{2,})?)*)|(localhost|LOCALHOST))\/?)/i;

        return re.test(value);
    },

    alpha: function (value) {
        var re = /^[a-zA-Z_]+$/;

        return re.test(value);
    },

    alphaNum: function (value) {
        var re = /^[a-zA-Z_]+$/;

        return re.test(value);
    },

    creditCard: function (value, type) {
        var re,
            checksum,
            i,
            digit;

        if (type == "Visa") {
            // Visa: length 16, prefix 4, dashes optional.
            re = /^4\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
        }
        else if (type == "MasterCard") {
            // Mastercard: length 16, prefix 51-55, dashes optional.
            re = /^5[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$/;
        }
        else if (type == "Discover") {
            // Discover: length 16, prefix 6011, dashes optional.
            re = /^6011-?\d{4}-?\d{4}-?\d{4}$/;
        }
        else if (type == "AmericanExpress") {
            // American Express: length 15, prefix 34 or 37.
            re = /^3[4,7]\d{13}$/;
        }
        else if (type == "DinersClub") {
            // Diners: length 14, prefix 30, 36, or 38.
            re = /^3[0,6,8]\d{12}$/;
        }
        else {
            // Basing min and max length on
            // http://developer.ean.com/general_info/Valid_Credit_Card_Types
            if (!value || value.length < 13 || value.length > 19) {
                return false;
            }

            re = /[^0-9 \-]+/;
        }

        if (!re.test(value)) {
            return false;
        }

        // Remove all dashes for the checksum checks to eliminate negative numbers
        value = value.split("-").join("");

        // Checksum ("Mod 10")
        // Add even digits in even length strings or odd digits in odd length strings.
        checksum = 0;

        for (i = (2 - (value.length % 2)) ; i <= value.length; i += 2) {
            checksum += parseInt(ccnum.charAt(i - 1));
        }

        // Analyze odd digits in even length strings or even digits in odd length strings.
        for (i = (value.length % 2) + 1; i < value.length; i += 2) {
            digit = parseInt(value.charAt(i - 1)) * 2;

            if (digit < 10) {
                checksum += digit;
            }
            else {
                checksum += (digit - 9);
            }
        }

        return (checksum % 10) == 0;
    }
};
Bridge.Class.extend('Bridge.Attribute', { });
Bridge.Class.extend('Bridge.INotifyPropertyChanged', {});
Bridge.Class.extend('Bridge.PropertyChangedEventArgs', {
    $ctor: function (propertyName) {
        this.propertyName = propertyName;
    }
});
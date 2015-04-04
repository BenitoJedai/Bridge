// @source Core.js

var Bridge = {
    emptyFn: function () { },

    copy: function (to, from, keys, toIf) {
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
                scope[nsParts[i]] = { };
            }

            scope = scope[nsParts[i]];
        }

        return scope;
    },

    ready: function (fn) {
        if (typeof window.jQuery !== 'undefined') {
            $(fn);
        } else {
            Bridge.on('DOMContentLoaded', document, fn);
        }
    },

    on: function (event, elem, fn) {
        var listenHandler = function (e) {
            var ret = fn.apply(this, arguments);

            if (ret === false) {
                e.stopPropagation();
                e.preventDefault();
            }

            return(ret);
        }

        var attachHandler = function () {            
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

    getHashCode: function (value) {
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

    getDefaultValue: function (type) {
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

    is: function (obj, type, ignoreFn) {
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
	
    as: function (obj, type) {
	    return Bridge.is(obj, type) ? obj : null;
    },
	
    cast: function (obj, type) {
	    var result = Bridge.as(obj, type);

	    if (result == null) {
	        throw new Bridge.InvalidCastException('Unable to cast type ' + Bridge.getTypeName(obj.constructor) + ' to type ' + Bridge.getTypeName(type));
	    }

	    return result;
    },
	
	apply: function (obj, values) {
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
	            else {
	                var setter = "set" + key.charAt(0).toUpperCase() + key.slice(1);
	                if (typeof to[setter] == "function" && typeof value != "function") {
	                    to[setter](value);
	                }
	                else if (value && value.constructor === Object && to[key]) {
	                    toValue = to[key];
	                    Bridge.merge(toValue, value);
	                }
	                else {
	                    to[key] = value;
	                }
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

	getPropertyNames: function (obj, includeFunctions) {
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

    compare: function (a, b) {
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

    equalsT: function (a, b) {
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

    getType: function (instance) {
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

            obj = obj || window;

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

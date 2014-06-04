Bridge = {
	is : function (obj, type) {
	  if (typeof type == "string") {
	      type = Bridge.unroll(type);
	  }

	  if (obj == null) {
		return false;
	  }

	  if (Bridge.isFunction(type)) {
	      return type(obj);
	  }

	  if (Bridge.isFunction(type.instanceOf)) {
	      return type.instanceOf(obj);
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
	      throw Error('Unable to cast type ' + Bridge.getTypeName(obj.constructor) + ' to type ' + Bridge.getTypeName(type));
	  }
	  return result;
	},

	getTypeName : function(type) {	  
	   return type.$name || '[native Object]';	  
	},
	
	apply : function (obj, values) {
	  var names = Bridge.getPropertyNames(values, false);
	  for(var i = 0; i < names.length; i++) {
	      var name = names[i];

	      if (typeof obj[name] == "function") {
	          obj[name](values[name]);
	      }
	      else {
	          obj[name] = values[name];
	      }
	  }
	  return obj;
	},

	getEnumerator: function (obj) {
	    if (obj && obj.getEnumerator) {
	        return obj.getEnumerator();
	    }

	    if ((Object.prototype.toString.call(obj) === '[object Array]') ||
            (obj && Bridge.isDefined(obj.length))) {
	        return new Bridge.ArrayEnumerator(obj);
	    }
	    
	    throw Error('Cannot create enumerator');
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

	isDefined: function (value) {
	    return typeof value !== 'undefined';
	},

	toArray: function (ienumerable) {
	    var i,
	        item,
            len
	        result = [];

	    if (Bridge.isArray(ienumerable)) {
	        for (i = 0, len = ienumerable.length; i < len; ++i) {
	            result.push(ienumerable[i]);
	        }
	    }
	    else {
	        i = Bridge.getEnumerator(ienumerable);
	        while (i.hasNext()) {
	            item = i.next();
	            result.push(item);
	        }
	    }	    

	    return result;
	},

    isArray: function (obj) {
        return Object.prototype.toString.call(obj) === '[object Array]';
    },

    isFunction: function (obj) {
        return typeof (obj) === 'function' && obj.prototype && !obj.prototype.call && !obj.prototype.apply;
    },

    isDate: function (obj) {
        return Object.prototype.toString.call(obj) === '[object Date]';
    },

    isNull: function (value) {
        return (value === null) || (value === undefined);
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

    fn: {
        call: function (obj, fnName){
            var args = Array.prototype.slice.call(arguments, 2);
            return obj[fnName].apply(obj, args);
        },

        bind: function (obj, method, args, appendArgs) {
            if (arguments.length === 2) {
                return function () {
                    return method.apply(obj, arguments)
                }
            }

            return function () {
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
        },

        bindScope: function (obj, method) {
            return function () {
                var callArgs = Array.prototype.slice.call(arguments, 0);
                callArgs.unshift.apply(callArgs, [obj]);

                return method.apply(obj, callArgs);
            };
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
                    if (list1[i] === list2[j]) {
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
/*
 * @version   : 1.0.0 - Bridge.NET
 * @author    : Object.NET, Inc. http://www.bridge.net/
 * @date      : 2014-06-01
 * @copyright : Copyright (c) 2008-2014, Object.NET, Inc. (http://www.object.net/). All rights reserved.
 * @license   : See license.txt and https://github.com/bridgedotnet/Bridge.NET/blob/master/LICENSE.
 */


// @source resources/Core.js

Bridge = {
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

    is : function (obj, type) {
	  if (typeof type == "string") {
        type = Bridge.unroll(type);
	  }

	  if (obj == null) {
		  return false;
	  }

	  if (Bridge.isFunction(type.$is)) {
	      return type.$is(obj);
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
            value,
            toValue;

	    for (key in from) {
	        value = from[key];
	        if (value && value.constructor === Object) {
	            toValue = to[key];
	            Bridge.merge(toValue, value);
	        }
	        else {
	            if (typeof to[key] == "function" && typeof value != "function") {
	                to[key](value);
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
          len,
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
      return typeof (obj) === 'function';
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
            if (list1[i] === list2[j] || 
                (list1[i].$method === list2[j].$method && list1[i].$scope === list2[j].$scope)) {
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
            throw Error("Nullable instance doesn't have a value.");
        }
        return obj;
    },

    getValueOrDefault: function (obj, defValue) {
        return Bridge.nullable.hasValue(obj) ? obj : defValue;
    }
};

Bridge.hasValue = Bridge.nullable.hasValue;

// @source resources/Class.js



// Inspired by base2 and Prototype
(function () {
    var initializing = false,
          fnTest = /xyz/.test(function () { xyz; }) ? /\bbase\b/ : /.*/;

    // The base Class implementation (does nothing)
    Bridge.Class = function () { };
    Bridge.Class.cache = {};

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

        if (!prop.$multipleCtors && !prop.$init) {
            prop.$init = extend ? function () {
                this.base();
            } : function () { };
        }

        if (!prop.$multipleCtors && !prop.$init) {
            prop.$init = extend ? function () {
                this.base();
            } : function () { };
        }

        if (!prop.$initMembers) {
            prop.$initMembers = extend ? function () {
                this.base();
            } : function () { };
        }

        // Copy the properties over onto the new prototype
        for (name in prop) {
            // Check if we're overwriting an existing function
            prototype[name] = typeof prop[name] == 'function' &&
              typeof base[name] == 'function' && fnTest.test(prop[name]) ?
              (function (name, fn) {
                  return function () {
                      var tmp = this.base;

                      // Add a new .base() method that is the same method
                      // but on the super-class
                      this.base = base[name];

                      // The method only need to be bound temporarily, so we
                      // remove it when we're done executing
                      var ret = fn.apply(this, arguments);

                      this.base = tmp;

                      return ret;
                  };
              })(name, prop[name]) :
              prop[name];
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
                if (this.$initMembers) {
                    this.$initMembers();
                }

                if (this.$multipleCtors && arguments.length > 0 && typeof arguments[0] == 'string' && Bridge.isFunction(this[arguments[0]])) {
                    this[arguments[0]].apply(this, Array.prototype.slice.call(arguments, 1));
                }
                else if (this.$ctorDetector) {
                    this.$ctorDetector.apply(this, arguments);
                }
                else if (this.$init) {
                    this.$init.apply(this, arguments);
                }
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

        if (Class.$init) {
            Class.$init.call(Class);
        }

        return Class;
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

  Bridge.apply(Bridge, {
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
  });
})();

// @source resources/Collections.js

Bridge.Class.extend('Bridge.IEnumerable', {});
Bridge.Class.extend('Bridge.IEnumerator', {});

Bridge.Class.extend('Bridge.Dictionary', {
  $extend: [Bridge.IEnumerable],

  $init: function (obj) {
    if (Object.prototype.toString.call(obj) === '[object Object]') {
      this.entries = obj;
      this.count = Bridge.getPropertyNames(this.entries).length;
    }
    else if (Bridge.is(obj, Bridge.Dictionary)) {
      this.entries = {};
      this.count = 0;

      for (var key in obj) {
        this.entries[key] = obj[key];
        this.count++;
      }
    }
    else {
      this.entries = {};
      this.count = 0;
    }
  },

  getKeys: function () {
    return Bridge.getPropertyNames(this.entries, false);
  },

  getValues: function () {
    var keys = this.getKeys(),
        result = [];

    for (var i = 0; i < keys.length; i++) {
      result.push(this.entries[keys[i]]);
    }

    return result;
  },

  clear: function () {
    this.entries = {};
    this.count = 0;
  },

  containsKey: function (key) {
    return Bridge.isDefined(this.entries[key]);
  },

  containsValue: function (value) {
    var keys = this.getKeys();

    for (var i = 0; i < keys.length; i++) {
      if (value === this.entries[keys[i]]) {
        return true;
      }
    }

    return false;
  },

  get: function (key) {
    if (!this.containsKey(key)) {
      throw new Error('Key not found: ' + key);
    }

    return this.entries[key];
  },

  set: function (key, value) {
    if (!this.containsKey(key)) {
      this.count++;
    }

    this.entries[key] = value;
  },

  add: function (key, value) {
    if (!this.containsKey(key)) {
      this.count++;
      this.entries[key] = value;
    }
    else {
      throw new Error('Key already exists: ' + key);
    }
  },

  remove: function (key) {
    if (this.containsKey(key)) {
      this.count--;
    }

    delete this.entries[key];
  },

  getCount: function () {
    return this.count;
  },

  getEnumerator: function () {
    return new Bridge.DictionaryEnumerator(this.entries);
  }
});

Bridge.Class.extend('Bridge.ICollection', {
  $extend: [Bridge.IEnumerable]
});

Bridge.Class.extend('Bridge.List', {
  $extend: [Bridge.ICollection],
  $init: function (obj) {
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
      throw new Error('Index out of range');
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
});


Bridge.Class.extend('Bridge.ArrayEnumerator', {
  $init: function (array) {
    this.array = array;
    this.index = 0;
  },

  hasNext: function () {
    return this.index < this.array.length;
  },

  next: function () {
    return this.array[this.index++];
  }
});

Bridge.Class.extend('Bridge.DictionaryEnumerator', {
  $init: function (entries) {
    this.entries = entries;
    this.keys = Bridge.getPropertyNames(this.entries, false);
    this.index = 0;
  },

  hasNext: function () {
    return this.index < this.keys.length;
  },

  next: function () {
    var index = this.index++,
        key = this.keys[index];

    return {
      key: key,
      value: this.entries[key]
    };
  }
});

// @source resources/Task.js

Bridge.Class.extend('Bridge.Task', {
    $init: function (action, state) {
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
            setTimeout(function() {
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
                            throw new Error('Invalid task status: ' + t.status);
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
                throw new Error('At least one task is required');
            }

            var task = new Bridge.Task(),
                i;
            for (i = 0; i < tasks.length; i++) {
                tasks[i].continueWith(function(t) {
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
                            throw new Error('Invalid task status: ' + t.status);
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

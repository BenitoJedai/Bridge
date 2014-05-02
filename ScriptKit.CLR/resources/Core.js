ScriptKit = {
	is : function (obj, type) {
	  if (typeof type == "string") {
	      type = ScriptKit.unroll(type);
	  }

	  if (obj == null) {
		return false;
	  }

	  if (obj.constructor == type) {
		return true;
	  }

	  if (ScriptKit.isArray(obj) && type == ScriptKit.IEnumerable) {
	      return true;
	  }

	  if (!type.$$inheritors) {
	      return false;
	  }

	  var inheritors = type.$$inheritors;	  
	  for (var i = 0; i < inheritors.length; i++) {
		if (ScriptKit.is(obj, inheritors[i])) {
		  return true;
		}
	  }
	  return false;
	},
	
	as : function (obj, type) {
	  return ScriptKit.is(obj, type) ? obj : null;
	},
	
	cast : function(obj, type) {
	  var result = ScriptKit.as(obj, type);
	  if (result == null) {
	      throw Error('Unable to cast type ' + ScriptKit.getTypeName(obj.constructor) + ' to type ' + ScriptKit.getTypeName(type));
	  }
	  return result;
	},

	getTypeName : function(type) {	  
	   return type.$name || '[native Object]';	  
	},
	
	bind : function (obj, method) {
	  return function () {
		return method.apply(obj, arguments)
	  }
	},
	
	apply : function (obj, values) {
	  var names = ScriptKit.getPropertyNames(values, false);
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

	    if (Object.prototype.toString.call(obj) === '[object Array]') {
	        return new ScriptKit.ArrayEnumerator(obj);
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

	    if (ScriptKit.isArray(ienumerable)) {
	        for (i = 0, len = ienumerable.length; i < len; ++i) {
	            result.push(ienumerable[i]);
	        }
	    }
	    else {
	        i = ScriptKit.getEnumerator(ienumerable);
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
    }
};
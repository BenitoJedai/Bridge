ScriptKit = {
	is : function (obj, type) {
	  if (obj == null || !type.inheritors) {
		return false;
	  }
	  if (obj.constructor == type) {
		return true;
	  }
	  var inheritors = type.inheritors;	  
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
		obj[name] = values[name];
	  }
	  return obj;
	},

	getIterator : function (obj) {
	    if (Object.prototype.toString.call(obj) === '[object Array]') {
	        return new ScriptKit.ArrayIterator(obj);
	    }
	    
	    throw Error('Cannot create iterator');
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
	}
};
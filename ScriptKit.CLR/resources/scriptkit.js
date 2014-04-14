/*
 * @version   : 1.0.0 - ScriptKit.NET License
 * @author    : Ext.NET, Inc. http://www.ext.net/
 * @date      : 2014-03-04
 * @copyright : Copyright (c) 2008-2014, Ext.NET, Inc. (http://www.ext.net/). All rights reserved.
 * @license   : See license.txt and http://www.ext.net/license/.
 */


ScriptKit={is:function(obj,type){if(obj==null||!type.inheritors){return false;}
if(obj.constructor==type){return true;}
var inheritors=type.inheritors;for(var i=0;i<inheritors.length;i++){if(ScriptKit.is(obj,inheritors[i])){return true;}}
return false;},as:function(obj,type){return ScriptKit.is(obj,type)?obj:null;},cast:function(obj,type){var result=ScriptKit.as(obj,type);if(result==null){throw Error('Unable to cast type '+ScriptKit.getTypeName(obj.constructor)+' to type '+ScriptKit.getTypeName(type));}
return result;},getTypeName:function(type){return type.$name||'[native Object]';},bind:function(obj,method){return function(){return method.apply(obj,arguments)}},apply:function(obj,values){var names=ScriptKit.getPropertyNames(values,false);for(var i=0;i<names.length;i++){var name=names[i];if(typeof obj[name]=="function"){obj[name](values[name]);}
else{obj[name]=values[name];}}
return obj;},getIterator:function(obj){if(Object.prototype.toString.call(obj)==='[object Array]'){return new ScriptKit.ArrayIterator(obj);}
throw Error('Cannot create iterator');},getPropertyNames:function(obj,includeFunctions){var names=[],name;for(name in obj){if(includeFunctions||typeof obj[name]!=='function'){names.push(name);}}
return names;}};

(function(){var initializing=false,fnTest=/xyz/.test(function(){xyz;})?/\bbase\b/:/.*/;ScriptKit.Class=function(){};ScriptKit.Class.extend=function(className,prop){var extend=prop.$extend,statics=prop.$statics,base=extend?extend[0].prototype:this.prototype,prototype,nameParts,scope,i,name;delete prop.$extend;delete prop.$statics;initializing=true;prototype=extend?new extend[0]():new Object();initializing=false;for(name in prop){prototype[name]=typeof prop[name]=="function"&&typeof base[name]=="function"&&fnTest.test(prop[name])?(function(name,fn){return function(){var tmp=this.base;this.base=base[name];var ret=fn.apply(this,arguments);this.base=tmp;return ret;};})(name,prop[name]):prop[name];}
prototype.$name=className;function Class(){if(!initializing&&this.init)
this.init.apply(this,arguments);}
Class.prototype=prototype;Class.prototype.constructor=Class;if(statics){for(name in statics){Class[name]=statics[name];}}
nameParts=className.split('.');scope=window;for(i=0;i<(nameParts.length-1);i++){if(typeof scope[nameParts[i]]=='undefined'){scope[nameParts[i]]={};}
scope=scope[nameParts[i]];}
scope[nameParts[nameParts.length-1]]=Class;if(!extend){extend=[Object];}
Class.$extend=extend;for(i=0;i<extend.length;i++){scope=extend[i];if(!scope.$inheritors){scope.$inheritors=[];}
scope.$inheritors.push(Class);}
if(Class.init){Class.init.call(Class);}
return Class;};})();

ScriptKit.Class.extend("ScriptKit.ArrayIterator",{init:function(array){this.array=array;this.index=0;},hasNext:function(){return this.index<this.array.length;},next:function(){return this.array[this.index++];}});

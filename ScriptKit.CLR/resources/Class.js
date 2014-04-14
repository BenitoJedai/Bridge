/* Simple JavaScript Inheritance
 * By John Resig http://ejohn.org/
 * MIT Licensed.
 */
// Inspired by base2 and Prototype
(function () {
    var initializing = false,
		fnTest = /xyz/.test(function () { xyz; }) ? /\bbase\b/ : /.*/;

    // The base Class implementation (does nothing)
    ScriptKit.Class = function () { };

    // Create a new Class that inherits from this class
    ScriptKit.Class.extend = function (className, prop) {
        var extend = prop.$extend,
			statics = prop.$statics,
			base = extend ? extend[0].prototype : this.prototype,
			prototype,
			nameParts,
			scope,
			i,
			name;

        delete prop.$extend;
        delete prop.$statics;

        // Instantiate a base class (but only create the instance,
        // don't run the init constructor)
        initializing = true;
        prototype = extend ? new extend[0]() : new Object();
        initializing = false;

        // Copy the properties over onto the new prototype
        for (name in prop) {
            // Check if we're overwriting an existing function
            prototype[name] = typeof prop[name] == "function" &&
              typeof base[name] == "function" && fnTest.test(prop[name]) ?
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

        prototype.$name = className;

        // The dummy class constructor
        function Class() {
            // All construction is actually done in the init method
            if (!initializing && this.init)
                this.init.apply(this, arguments);
        }

        // Populate our constructed prototype object
        Class.prototype = prototype;

        // Enforce the constructor to be what we expect
        Class.prototype.constructor = Class;

        if (statics) {
            for (name in statics) {
                Class[name] = statics[name];
            }
        }

        nameParts = className.split('.');
        scope = window;
        for (i = 0; i < (nameParts.length - 1) ; i++) {
            if (typeof scope[nameParts[i]] == 'undefined') {
                scope[nameParts[i]] = {};
            }

            scope = scope[nameParts[i]];
        }

        scope[nameParts[nameParts.length - 1]] = Class;

        if (!extend) {
            extend = [Object];
        }

        Class.$extend = extend;

        for (i = 0; i < extend.length; i++) {
            scope = extend[i];
            if (!scope.$inheritors) {
                scope.$inheritors = [];
            }
            scope.$inheritors.push(Class);
        }

        if (Class.init) {
            Class.init.call(Class);
        }

        return Class;
    };
})();
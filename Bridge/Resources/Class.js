// @source Class.js

(function () {
    var initializing = false;

    // The base Class implementation
    var base = {
        cache: { },

        initCtor: function () {
            var value = arguments[0];

            if (this.$multipleCtors && arguments.length > 0 && typeof value == 'string') {
                value = value === "constructor" ? "$constructor" : value;

                if ((value === "$constructor" || Bridge.String.startsWith(value, "constructor\\$")) && Bridge.isFunction(this[value])) {
                    this[value].apply(this, Array.prototype.slice.call(arguments, 1));

                    return;
                }            
            }

            if (this.$constructor) {
                this.$constructor.apply(this, arguments);
            }
        },

        initConfig: function (extend, base, cfg, statics, scope) {                
            scope.$initMembers = function () {
                var name,
                    config;

                config = Bridge.isFunction(cfg) ? cfg() : cfg;

                if (extend && !statics && base.$initMembers) {
                    base.$initMembers.apply(this, arguments);
                }

                if (config.fields) {
                    for (name in config.fields) {
                        this[name] = config.fields[name];
                    }
                }

                if (config.properties) {
                    for (name in config.properties) {
                        this[name] = config.properties[name];
                    
                        var rs = name.charAt(0) == "$",
                            cap = rs ? name.slice(1) : name;

                        this["get" + cap] = (function (name) {
                            return function () {
                                return this[name];
                            };                        
                        })(name);

                        this["set" + cap] = (function (name) {
                            return function (value) {
                                this[name] = value;
                            };
                        })(name);
                    }
                }

                if (config.events) {
                    for (name in config.events) {
                        this[name] = config.events[name];

                        var rs = name.charAt(0) == "$",
                            cap = rs ? name.slice(1) : name;

                        this["add" + cap] = (function (name) {
                            return function (value) {
                                this[name] = Bridge.fn.combine(this[name], value);
                            };
                        })(name);

                        this["remove" + cap] = (function (name) {
                            return function (value) {
                                this[name] = Bridge.fn.remove(this[name], value);
                            };
                        })(name);
                    }
                }
                if (config.alias) {
                    for (name in config.alias) {
                        if (this[name]) {
                            this[name] = this[config.alias[name]];
                        }
                    }
                }

                if (config.init) {
                    config.init.apply(this, arguments);
                }
            };
        },

        // Create a new Class that inherits from this class
        define: function (className, prop) {
            var extend = prop.$extends,
                statics = prop.$statics || prop.statics,
                base = extend ? extend[0].prototype : this.prototype,
                prototype,
                nameParts,
                scope = prop.$scope || Bridge.global,
                i,
                v,
                ctorCounter,
                isCtor,
                name;

            if (Bridge.isFunction(extend)) {
                extend = null;
            }
            else if (prop.$extends) {
                delete prop.$extends;
            }
            else {
                delete prop.extends;
            }

            if (Bridge.isFunction(statics)) {
                statics = null;
            }
            else if (prop.$statics) {
                delete prop.$statics;
            }
            else {
                delete prop.statics;
            }

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
                        this.$initMembers.apply(this, arguments);
                    }

                    this.$$initCtor.apply(this, arguments);
                }
            }

            // Instantiate a base class (but only create the instance,
            // don't run the init constructor)
            initializing = true;
            prototype = extend ? new extend[0]() : new Object();
            initializing = false;

            if (statics) {
                var staticsConfig = statics.$config/* || statics.config*/;

                if (staticsConfig/* && !Bridge.isFunction(staticsConfig)*/) {
                    Bridge.Class.initConfig(extend, base, staticsConfig, true, Class);

                    if (statics.$config) {
                        delete statics.$config;
                    }
                    else {
                        delete statics.config;
                    }
                }
            }        

            var instanceConfig = prop.$config/* || prop.config*/;

            if (instanceConfig/* && !Bridge.isFunction(instanceConfig)*/) {
                Bridge.Class.initConfig(extend, base, instanceConfig, false, prop);

                if (prop.$config) {
                    delete prop.$config;
                }
                else {
                    delete prop.config;
                }
            }
            else {
                prop.$initMembers = extend ? function () {
                    base.$initMembers.apply(this, arguments);
                } : function () { };
            }

            prop.$$initCtor = Bridge.Class.initCtor;

            // Copy the properties over onto the new prototype
            ctorCounter = 0;

            for (name in prop) {            
                v = prop[name];
                isCtor = name === "constructor";

                if (Bridge.isFunction(v) && (isCtor || Bridge.String.startsWith(name, "constructor\\$"))) {
                    ctorCounter++;
                }

                prototype[isCtor ? "$constructor" : name] = prop[name];
            }

            if (ctorCounter == 0) {
                prototype.$constructor = extend ? function () {
                    base.$constructor();
                } : function () { };
            }

            if (ctorCounter > 1) {
                prototype.$multipleCtors = true;
            }

            prototype.$$name = className;        

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

            Class.$$extends = extend;

            for (i = 0; i < extend.length; i++) {
                scope = extend[i];

                if (!scope.$$inheritors) {
                    scope.$$inheritors = [];
                }

                scope.$$inheritors.push(Class);
            }
        
            if (Class.$initMembers) {
                Class.$initMembers.call(Class);
            }

            if (Class.constructor) {
                Class.constructor.call(Class);
            }

            return Class;
        },


        addExtend: function (cls, extend) {        
            Array.prototype.push.apply(cls.$$extends, extend);

            for (i = 0; i < extend.length; i++) {
                scope = extend[i];

                if (!scope.$$inheritors) {
                    scope.$$inheritors = [];
                }

                scope.$$inheritors.push(cls);
            }
        },

        set: function (scope, className, cls) {
            var nameParts = className.split('.');

            for (i = 0; i < (nameParts.length - 1) ; i++) {
                if (typeof scope[nameParts[i]] == 'undefined') {
                    scope[nameParts[i]] = { };
                }

                scope = scope[nameParts[i]];
            }

            scope[nameParts[nameParts.length - 1]] = cls;

            return scope;
        },

        genericName: function () {
            var name = arguments[0];

            for (var i = 1; i < arguments.length; i++) {
                name += '$' + Bridge.getTypeName(arguments[i]);
            }

            return name;
        },

        generic: function (className, scope, fn) {
            if (!fn) {
                fn = scope;
                scope = Bridge.global;
            }

            Bridge.Class.set(scope, className, fn);

            return fn;
        }
    };

    Bridge.Class = base;
    Bridge.define = Bridge.Class.define;
})();
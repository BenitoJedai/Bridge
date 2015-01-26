Bridge.Class.extend('Bridge.Aspects.AbstractMethodAspectAttribute', {
    $extend: [Bridge.Aspects.MulticastAspectAttribute],

    $$setAspect: Bridge.emptyFn,

    init: function (methodName, scope) {
        this.methodName = methodName;
        this.scope = scope;
        this.targetMethod = this.scope[this.methodName];

        if (!this.runTimeValidate(methodName, scope)) {
            return;
        }

        this.scope.$$aspects = this.scope.$$aspects || {};
        if (!this.scope.$$aspects[this.$$name]) {
            this.scope.$$aspects[this.$$name] = [];
        }
        this.scope.$$aspects[this.$$name].push(this);        

        this.$$setAspect();
    },

    remove: function () {
        var i,
            aspects = this.scope.$$aspects[this.$$name];

        this.scope[this.methodName] = this.targetMethod;

        for (i = aspects.length - 1; i >= 0; i--) {
            if (aspects[i] === this) {
                aspects.splice(i, 1);
                return;
            }
        }
    },

    runTimeValidate: function () {
        return true;
    }
});

Bridge.Class.extend('Bridge.Aspects.MethodAspectAttribute', {
    $extend: [Bridge.Aspects.AbstractMethodAspectAttribute],

    onEntry: Bridge.emptyFn,
    onExit: Bridge.emptyFn,
    onSuccess: Bridge.emptyFn,
    onException: Bridge.emptyFn,    

    $$setAspect: function () {
        var me = this,
            fn = function () {
                var methodArgs = {
                        arguments: arguments,
                        methodName: me.methodName,
                        scope: me.scope,
                        exception: null,
                        flow: 0
                    },                    
                    catchException;

                me.onEntry(methodArgs);

                if (methodArgs.flow == 3) {
                    return methodArgs.returnValue;
                }

                try {
                    methodArgs.returnValue = me.targetMethod.apply(me.scope, methodArgs.arguments);

                    if (methodArgs.flow == 3) {
                        return methodArgs.returnValue;
                    }

                    me.onSuccess(methodArgs);
                }
                catch (e) {
                    methodArgs.exception = Bridge.Exception.create(e);                    

                    catchException = me.$$exceptionTypes.length == 0;
                    if (me.$$exceptionTypes.length > 0) {
                        for (var i = 0; i < me.$$exceptionTypes.length; i++) {
                            if (Bridge.is(methodArgs.exception, me.$$exceptionTypes[i])) {
                                catchException = true;
                                break;
                            }
                        }
                    }

                    if (catchException) {
                        me.onException(methodArgs);
                    }                    

                    if (methodArgs.flow == 3) {
                        return methodArgs.returnValue;
                    }

                    if (methodArgs.flow == 2 && methodArgs.flow == 0) {
                        throw e;
                    }
                }
                finally {
                    me.onExit(methodArgs);
                }

                return methodArgs.returnValue;
            };

        this.$$exceptionTypes = this.getExceptionsTypes(this.methodName, this.scope) || [];
        this.scope[this.methodName] = fn;
    },    

    getExceptionsTypes: function () {
        return [];
    }    
});

Bridge.Class.extend('Bridge.Aspects.BufferedMethodAttribute', {
    $extend: [Bridge.Aspects.AbstractMethodAspectAttribute],

    $init: function (buffer) {
        this.buffer = buffer;
    },

    $$setAspect: function () {
        var me = this,
            timeoutId,
            fn = function () {
                var args = Array.prototype.slice.call(arguments, 0);

                if (timeoutId) {
                    clearTimeout(timeoutId);
                }

                timeoutId = setTimeout(function () {
                    me.targetMethod.apply(me.scope, args);
                }, me.buffer);
            };

        this.scope[this.methodName] = fn;
    }
});

Bridge.Class.extend('Bridge.Aspects.BarrierMethodAttribute', {
    $extend: [Bridge.Aspects.AbstractMethodAspectAttribute],

    $init: function (count) {
        this.count = count;
    },

    $$setAspect: function () {
        var me = this,
            fn = function () {
                if (!--me.count) {
                    me.targetMethod.apply(me.scope, arguments);
                }
            };

        this.scope[this.methodName] = fn;
    }
});

Bridge.Class.extend('Bridge.Aspects.DelayedMethodAttribute', {
    $extend: [Bridge.Aspects.AbstractMethodAspectAttribute],

    $init: function (delay) {
        this.delay = delay;
    },

    $$setAspect: function () {
        var me = this,
            fn = function () {
                var args = Array.prototype.slice.call(arguments);

                setTimeout(function () {
                    me.targetMethod.apply(me.scope, args);
                }, me.delay);
            };

        this.scope[this.methodName] = fn;
    }
});

Bridge.Class.extend('Bridge.Aspects.ThrottledMethodAttribute', {
    $extend: [Bridge.Aspects.AbstractMethodAspectAttribute],

    $init: function (interval) {
        this.interval = interval;
    },

    $$setAspect: function () {
        var me = this,
            lastCallTime = 0,
            elapsed,
            lastArgs,
            timer,
            execute = function () {
                me.targetMethod.apply(me.scope, lastArgs);
                lastCallTime = +new Date();
                timer = null;
            };

        this.scope[this.methodName] =  function () {
            elapsed = +new Date() - lastCallTime;
            lastArgs = arguments;
            if (elapsed >= me.interval) {
                clearTimeout(timer);
                execute();
            }
            else if (!timer) {
                timer = setTimeout(execute, me.interval - elapsed);
            }
        };        
    }
});
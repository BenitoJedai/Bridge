Bridge.Class.extend('Bridge.MethodAspectAttribute', {
    $extend: [Bridge.MulticastAspectAttribute],

    onEntry: Bridge.emptyFn,
    onExit: Bridge.emptyFn,
    onSuccess: Bridge.emptyFn,
    onException: Bridge.emptyFn,

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
        this.exceptionTypes = this.getExceptionsTypes(methodName, scope) || [];

        this.$$setAspect();
    },

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
                    me.onSuccess(methodArgs);

                    if (methodArgs.flow == 3) {
                        return methodArgs.returnValue;
                    }
                }
                catch (e) {
                    methodArgs.exception = Bridge.Exception.create(e);                    

                    catchException = me.exceptionTypes.length == 0;
                    if (me.exceptionTypes.length > 0) {
                        for (var i = 0; i < me.exceptionTypes.length; i++) {
                            if (Bridge.is(methodArgs.exception, me.exceptionTypes[i])) {
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

        this.scope[this.methodName] = fn;
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

    getExceptionsTypes: function () {
        return [];
    },

    runTimeValidate: function () {
        return true;
    }
});
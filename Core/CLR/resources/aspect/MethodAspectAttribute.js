Bridge.Class.extend('Bridge.MethodAspectAttribute', {
    $extend: [Bridge.AspectAttribute],

    onEntry: Bridge.emptyFn,
    onExit: Bridge.emptyFn,
    onSuccess: Bridge.emptyFn,
    onException: Bridge.emptyFn,

    init: function (methodName, scope) {
        this.methodName = methodName;
        this.scope = scope;
        this.targetMethod = this.scope[this.methodName];

        this.scope.$$aspects = this.scope.$$aspects || {};
        if (!this.scope.$$aspects[this.$$name]) {
            this.scope.$$aspects[this.$$name] = [];
        }
        this.scope.$$aspects[this.$$name].push(this);

        this.removeExists();
        this.setAspect();
    },

    setAspect: function () {
        var me = this,
            fn = function () {
                var methodArgs = {
                    arguments: arguments,
                    methodName: me.methodName,
                    scope: me.scope,
                    exception: null,
                    rethrowException: true,
                    cancelTargetInvocation: false
                };
                me.onEntry(methodArgs);
                try {
                    if (!methodArgs.cancelTargetInvocation) {
                        methodArgs.returnValue = me.targetMethod.apply(me.scope, methodArgs.arguments);
                        me.onSuccess(methodArgs);
                    }
                }
                catch (e) {
                    methodArgs.exception = Bridge.Exception.create(e);
                    me.onException(methodArgs);

                    if (methodArgs.rethrowException) {
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

    removeExists : function () {
        var i,
            aspect,
            aspects = this.scope.$$aspects[this.$$name];

        for (i = aspects.length - 1; i >= 0; i--) {
            aspect = aspects[i];
            if (aspect.methodName === this.methodName) {
                this.scope[this.methodName] = aspect.targetMethod;
                aspects.splice(i, 1);
                return;
            }
        }
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
    }
});
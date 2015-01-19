Bridge.Class.extend('Bridge.PropertyAspectAttribute', {
    $extend: [Bridge.MulticastAspectAttribute],

    onGetValue: Bridge.emptyFn,
    onSetValue: Bridge.emptyFn,
    onSuccess: Bridge.emptyFn,
    onException: Bridge.emptyFn,

    init: function (propertyName, scope) {
        this.propertyName = propertyName;
        this.scope = scope;
        this.getter = this.scope["get" + this.propertyName];
        this.setter = this.scope["set" + this.propertyName];

        if (!this.runTimeValidate(propertyName, scope)) {
            return;
        }

        this.scope.$$aspects = this.scope.$$aspects || {};
        if (!this.scope.$$aspects[this.$$name]) {
            this.scope.$$aspects[this.$$name] = [];
        }
        this.scope.$$aspects[this.$$name].push(this);
        this.$$exceptionTypes = this.getExceptionsTypes(propertyName, scope) || [];

        this.$$setAspect();
    },

    $$setAspect: function () {
        var fn = function (me, isGetter) {
                return function (value) {
                    var methodArgs = {
                            value: value,
                            propertyName: me.propertyName,
                            scope: me.scope,
                            exception: null,
                            flow: 0,
                            isGetter : isGetter
                        },                    
                        catchException;

                    if (isGetter) {
                        me.onGetValue(methodArgs);
                    }
                    else {
                        me.onSetValue(methodArgs);
                    }

                    if (methodArgs.flow == 3) {
                        return isGetter ? methodArgs.value : undefined;
                    }

                    try {
                        if (isGetter) {
                            methodArgs.value = me.getter.call(me.scope);
                        }
                        else {
                            me.setter.call(me.scope, methodArgs.value);
                        }

                        if (methodArgs.flow == 3) {
                            return isGetter ? methodArgs.value : undefined;
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
                            return isGetter ? methodArgs.value : undefined;
                        }

                        if (methodArgs.flow == 2 && methodArgs.flow == 0) {
                            throw e;
                        }
                    }

                    return isGetter ? methodArgs.value : undefined;
                }
            };

        if (this.getter) {
            this.scope["get" + this.propertyName] = fn(this, true);
        }

        if (this.setter) {
            this.scope["set" + this.propertyName] = fn(this, false);
        }        
    },

    remove: function () {
        var i,
            aspects = this.scope.$$aspects[this.$$name];

        this.scope[this.propertyName] = this.getter;
        this.scope[this.propertyName] = this.setter;
        
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
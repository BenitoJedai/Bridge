Bridge.Class.extend('Bridge.Aspects.ParameterAspectAttribute', {
    $extend: [Bridge.Aspects.MulticastAspectAttribute],

    init: function (argName, argIndex, argType, methodName, scope) {
        this.parameterName = argName;
        this.parameterIndex = argIndex;
        this.parameterType = argType;
        this.methodName = methodName;
        this.scope = scope;
        this.targetMethod = this.scope[this.methodName];

        if (!this.runTimeValidate(argName, argIndex, argType, methodName, scope)) {
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
    },

    parameterValidate: Bridge.emptyFn,

    $$setAspect: function () {
        var me = this,
            fn = function () {
                var args = Array.prototype.slice.call(arguments, 0),
                    valArg = {
                        parameterIndex: me.parameterIndex,
                        parameterType: me.parameterType,
                        parameterName: me.parameterName,
                        parameter: args[me.parameterIndex],
                        methodName: me.methodName,
                        scope: me.scope
                    };

                me.parameterValidate(valArg);
                args[me.parameter] = valArg.parameter;
                return me.targetMethod.apply(me.scope, args);
            };

        this.scope[this.methodName] = fn;
    }
});

Bridge.Class.extend('Bridge.Aspects.ParameterContractAspectAttribute', {
    $extend: [Bridge.Aspects.ParameterAspectAttribute],

    parameterValidate: function (arg) {
        if (this.validate(arg) === false) {
            throw this.createException(arg);
        }
    },

    createException: function (arg) {
        return new Bridge.ArgumentException(this.message, arg.parameterName);
    },

    validate: Bridge.emptyFn
});

Bridge.Class.extend('Bridge.Aspects.CreditCardAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (type) {
        this.type = type;
    },

    validate: function (arg) {
        return Bridge.Validation.isNull(arg.parameter) || Bridge.Validation.creditCard(arg.parameter, this.type);
    }
});

Bridge.Class.extend('Bridge.Aspects.EmailAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    validate: function (arg) {
        return Bridge.Validation.isNull(arg.parameter) || Bridge.Validation.email(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.UrlAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    validate: function (arg) {
        return Bridge.Validation.isNull(arg.parameter) || Bridge.Validation.url(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.GreaterThanAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (min, strict) {
        this.min = min;
        this.strict = strict;
    },

    createException: function (arg) {
        return new Bridge.ArgumentOutOfRangeException(arg.parameterName, this.message, null, arg.parameter);
    },

    validate: function (arg) {
        return this.strict !== false ? arg.parameter > this.min : arg.parameter >= this.min;
    }
});

Bridge.Class.extend('Bridge.Aspects.LessThanAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (max, strict) {
        this.max = max;
        this.strict = strict;
    },

    createException: function (arg) {
        return new Bridge.ArgumentOutOfRangeException(arg.parameterName, this.message, null, arg.parameter);
    },

    validate: function (arg) {
        return this.strict !== false ? arg.parameter < this.max : arg.parameter <= this.max;
    }
});

Bridge.Class.extend('Bridge.Aspects.NotEmptyAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    createException: function (arg) {
        return new Bridge.ArgumentNullException(arg.parameterName, this.message);
    },

    validate: function (arg) {
        return Bridge.Validation.isNotEmpty(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.NotNullAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    createException: function (arg) {
        return new Bridge.ArgumentNullException(arg.parameterName, this.message);
    },

    validate: function (arg) {
        return Bridge.Validation.isNotNull(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.PositiveAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    createException: function (arg) {
        return new Bridge.ArgumentOutOfRangeException(arg.parameterName, this.message, null, arg.parameter);
    },

    validate: function (arg) {
        return arg.parameter >= 0;
    }
});

Bridge.Class.extend('Bridge.Aspects.NegativeAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    createException: function (arg) {
        return new Bridge.ArgumentOutOfRangeException(arg.parameterName, this.message, null, arg.parameter);
    },

    validate: function (arg) {
        return arg.parameter < 0;
    }
});

Bridge.Class.extend('Bridge.Aspects.RangeAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (min, max, strict) {
        this.min = min;
        this.max = max;
        this.strict = strict;
    },

    createException: function (arg) {
        return new Bridge.ArgumentOutOfRangeException(arg.parameterName, this.message, null, arg.parameter);
    },

    validate: function (arg) {
        if (this.strict === false) {
            return arg.parameter <= this.max && arg.parameter >= this.min;
        }
        return arg.parameter < this.max && arg.parameter > this.min;
    }
});

Bridge.Class.extend('Bridge.Aspects.RequiredAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    createException: function (arg) {
        return new Bridge.ArgumentNullException(arg.parameterName, this.message, null);
    },

    validate: function (arg) {
        return Bridge.Validation.isNotEmptyOrWhitespace(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.LengthAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (max, min) {
        this.max = max;
        this.min = min || 0;
    },

    validate: function (arg) {
        return Bridge.Validation.isNull(arg.parameter) || (arg.parameter.length >= this.min && arg.parameter.length <= this.max);
    }
});

Bridge.Class.extend('Bridge.Aspects.RegularExpressionAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (pattern, flags) {
        this.regExp = new RegExp(pattern, flags);
    },

    validate: function (arg) {
        return Bridge.Validation.isNull(arg.parameter) || this.regExp.test(arg.parameter);
    }
});

Bridge.Class.extend('Bridge.Aspects.ValidatorAttribute', {
    $extend: [Bridge.Aspects.ParameterContractAspectAttribute],

    $init: function (fn) {
        this.fn = Bridge.isString(fn) ? eval(fn) : fn;
    },

    validate: function (arg) {
        return this.fn(arg.parameter);
    }
});
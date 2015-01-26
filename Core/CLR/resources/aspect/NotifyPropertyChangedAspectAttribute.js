Bridge.Class.extend('Bridge.Aspects.NotifyPropertyChangedAttribute', {
    $extend: [Bridge.Aspects.MulticastAspectAttribute],

    $statics: {
        globalSuspendCounter: 0,

        resumeEvents: function (instance) {
            if (Bridge.isDefined(instance, true)) {
                var a = instance.$$aspects;
                if (a && Bridge.isDefined(a.$$notifyPropertySuspendCounter)) {
                    a.$$notifyPropertySuspendCounter--;
                }
            }
            else {
                this.globalSuspendCounter--;
            }
        },

        suspendEvents: function (instance) {
            if (Bridge.isDefined(instance, true)) {
                var a = instance.$$aspects;
                if (a && Bridge.isDefined(a.$$notifyPropertySuspendCounter)) {
                    a.$$notifyPropertySuspendCounter++;
                }
            }
            else {
                this.globalSuspendCounter++;
            }
        },

        raiseEvents: function (instance) {
            if (instance && instance.$$aspects) {
                var i,
                    aspects = instance.$$aspects[this.$$name];

                for (i = 0; i < aspects.length; i++) {
                    aspects[i].raiseEvent();
                }
            }
        }
    },

    $initMembers: function () {
        this.raiseOnChange = true;
    },

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

        if (!Bridge.isDefined(this.scope.$$aspects.$$notifyPropertySuspendCounter)) {
            this.scope.$$aspects.$$notifyPropertySuspendCounter = 0;
        }

        this.$$setAspect();
    },

    $$setAspect: function () {        
        if (this.setter) {
            var me = this;
            this.scope["set" + this.propertyName] = function (value) {
                var args = {
                    value: value,
                    propertyName: me.propertyName,
                    scope: me.scope,
                    flow: 0,
                    force: false
                };

                if (me.raiseOnChange) {
                    args.lastValue = me.getter();
                }

                me.setter.call(me.scope, args.value);
                me.onSetValue(args);                
            };
        }
    },

    beforeEvent: Bridge.emptyFn,

    onSetValue: function (args) {
        this.beforeEvent(args);

        if (args.flow === 3) {
            return;
        }

        if (args.flow !== 1) {
            if (Bridge.Aspects.NotifyPropertyChangedAttribute.globalSuspendCounter) {
                return;
            }
            
            if (this.scope.$$aspects.$$notifyPropertySuspendCounter) {
                return;
            }
        }

        var raise = true;
        if (this.raiseOnChange) {
            raise = args.value !== args.lastValue;
        }
        
        if (raise) {
            if (this.scope.onPropertyChanged) {
                this.scope.onPropertyChanged(args.propertyName);
            }
            else if (this.scope.propertyChanged) {
                this.scope.propertyChanged(this.scope, { propertyName: args.propertyName });
            }
        }
    },

    raiseEvent: function () {
        var args = {
            propertyName: this.propertyName,
            scope: this.scope,
            flow: 0,
            force: true,
            value: null
        };
        
        this.beforeEvent(args);

        if (args.flow === 3) {
            return;
        }

        if (this.scope.onPropertyChanged) {
            this.scope.onPropertyChanged(args.propertyName);
        }
        else if (this.scope.propertyChanged) {
            this.scope.propertyChanged(this.scope, { propertyName: args.propertyName });
        }
    },

    runTimeValidate: function () {
        return true;
    }
});
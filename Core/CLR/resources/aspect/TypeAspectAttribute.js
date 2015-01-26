Bridge.Class.extend('Bridge.Aspects.TypeAspectAttribute', {
    $extend: [Bridge.Aspects.MulticastAspectAttribute],

    onInstance: Bridge.emptyFn,
    onAfterInstance: Bridge.emptyFn,

    init: function (instance, arguments) {
        this.instance = instance;
        this.typeName = instance.$$name;
        this.arguments = arguments;

        if (!this.runTimeValidate(instance)) {
            return;
        }

        this.instance.$$aspects = this.instance.$$aspects || {};
        if (!this.instance.$$aspects[this.$$name]) {
            this.instance.$$aspects[this.$$name] = [];
        }
        this.instance.$$aspects[this.$$name].push(this);
        this.$$setAspect();
    },

    $$setAspect: function () {
        var me = this;
        this.$$targetInitCtor = this.instance.$$initCtor;
        this.instance.$$initCtor = function () {
            var args = {
                instance: me.instance,
                typeName: me.typeName,
                arguments: arguments
            };

            me.onInstance(args);

            me.$$targetInitCtor.apply(me.instance, args.arguments)

            me.onAfterInstance(args);
        };
    },

    runTimeValidate: function () {
        return true;
    }
});
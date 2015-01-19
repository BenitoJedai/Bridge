Bridge.Class.extend('Bridge.TypeAspectAttribute', {
    $extend: [Bridge.MulticastAspectAttribute],

    onInstance: Bridge.emptyFn,

    init: function (instance) {
        this.instance = instance;
        this.typeName = instance.$$name;

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
        this.onInstance({instance: this.instance});
    },

    runTimeValidate: function () {
        return true;
    }
});
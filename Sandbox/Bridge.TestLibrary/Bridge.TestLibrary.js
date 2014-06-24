Bridge.Class.extend('TestProject.Class1', {
    $statics: {
        $init: function () {
            this.onEvent2 = Bridge.fn.bind(TestProject.Class1, TestProject.Class1.class1_Event);
        },
        add_onEvent2 : Bridge.fn.combine, 
        remove_onEvent2 : Bridge.fn.remove,
        class1_Event: function (i) {
            console.log("OnStaticEvent " + i);
        },
        start: function () {
            new TestProject.Class1("$init").instanceStart();
        }
    },
    $initMembers: function () {
        this.onEvent = Bridge.fn.bind(TestProject.Class1, TestProject.Class1.class1_Event);
    },
    add_onEvent : Bridge.fn.combine, 
    remove_onEvent : Bridge.fn.remove,
    add_OnPropEvent: function (value) {
        this.remove_OnEvent(this.onEvent = (this.add_OnEvent(this.onEvent, value);
    },
    remove_OnPropEvent: function (value) {
        this.add_OnEvent(this.onEvent = (this.remove_OnEvent(this.onEvent, value);
    },
    instanceStart: function () {
        this.remove_OnEvent(this.onEvent = (this.add_OnEvent(this.onEvent, Bridge.fn.bind(this, this.class1_OnEvent));
        add_OnPropEvent(this.onPropEvent = (add_OnPropEvent(this.onPropEvent, Bridge.fn.bind(this, this.class1_OnPropEvent));
        if (this.onEvent !== null) {
            this.onEvent(1);
        }
        this.add_OnEvent(this.onEvent = (this.remove_OnEvent(this.onEvent, Bridge.fn.bind(this, this.class1_OnEvent));
        remove_OnPropEvent(this.onPropEvent = (remove_OnPropEvent(this.onPropEvent, Bridge.fn.bind(this, this.class1_OnPropEvent));
        if (this.onEvent !== null) {
            this.onEvent(2);
        }
    },
    class1_OnEvent: function (i) {
        console.log("OnEvent " + i);
    },
    class1_OnPropEvent: function (i) {
        console.log("OnPropEvent " + i);
    }
});

Bridge.Class.extend('TestProject.Class2', {
    $extend: [TestProject.Class1],
    $init: function () {
        this.base();

    }
});



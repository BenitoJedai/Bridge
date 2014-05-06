Bridge.Class.extend('TestProject.App', {
    $statics: {
        start: function () {
        }
    },
    $init: function () {
    },
    app_ActionEvent: function (arg) {
        throw new System.NotImplementedException();
    },
    fire: function () {
        this.ActionEvent += this.app_ActionEvent;
        if (this.ActionEvent != ) {
            this.ActionEvent("test");
        }
    }
});


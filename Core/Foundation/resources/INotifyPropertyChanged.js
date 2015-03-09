Bridge.Class.define('Bridge.INotifyPropertyChanged', {});
Bridge.Class.define('Bridge.PropertyChangedEventArgs', {
    constructor: function (propertyName) {
        this.propertyName = propertyName;
    }
});
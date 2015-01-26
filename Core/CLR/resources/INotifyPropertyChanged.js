Bridge.Class.extend('Bridge.INotifyPropertyChanged', {});
Bridge.Class.extend('Bridge.PropertyChangedEventArgs', {
    $init: function (propertyName) {
        this.propertyName = propertyName;
    }
});
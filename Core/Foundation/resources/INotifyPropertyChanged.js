Bridge.Class.extend('Bridge.INotifyPropertyChanged', {});
Bridge.Class.extend('Bridge.PropertyChangedEventArgs', {
    $ctor: function (propertyName) {
        this.propertyName = propertyName;
    }
});
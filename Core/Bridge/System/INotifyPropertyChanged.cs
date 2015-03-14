using Bridge;

namespace System.ComponentModel
{
    [Ignore]
    [Namespace("Bridge")]
    public interface INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    [Name("Function")]
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);

    [Ignore]
    [Namespace("Bridge")]
    public class PropertyChangedEventArgs
    {
        public PropertyChangedEventArgs(string propertyName)
        {
        }

        public PropertyChangedEventArgs(string propertyName, object newValue)
        {
        }

        public PropertyChangedEventArgs(string propertyName, object newValue, object oldValue)
        {
        }

        public readonly string PropertyName;
        public readonly object OldValue;
        public readonly object NewValue;
    }
}

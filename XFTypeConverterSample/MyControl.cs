using System;
using Xamarin.Forms;

namespace XFTypeConverterSample
{
    public class MyControl : Label
    {
        public static readonly BindableProperty MyTextProperty =
            BindableProperty.Create(nameof(MyText),
            typeof(string),
            typeof(MyControl),
            propertyChanged: MyTextChanged);

        private static void MyTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // TODO better error handling
            ((MyControl)bindable).Text = newValue.ToString();
        }

        [TypeConverter(typeof(EnumToTextTypeConverter))]
        public string MyText
        {
            get => (string)GetValue(MyTextProperty);
            set => SetValue(MyTextProperty, value);
        }
    }

    public enum SubscribedStatus
    {
        Subbed = 0,
        NotSubbedBoo = 1,
        WillSubNow = 2
    }

    public class EnumToTextTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            string description = Enum.Parse(typeof(SubscribedStatus), value).ToString();
            return description;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace vp.TriggerActions
{
    public class SetPropertyAction : TriggerAction<DependencyObject>
    {
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(SetPropertyAction), new PropertyMetadata());
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(SetPropertyAction), new PropertyMetadata());
        public static readonly DependencyProperty TargetObjectProperty = DependencyProperty.Register("TargetObject", typeof(DependencyObject), typeof(SetPropertyAction), new PropertyMetadata());

        public DependencyObject TargetObject
        {
            get => (DependencyObject)GetValue(TargetObjectProperty);
            set => SetValue(TargetObjectProperty, value);
        }

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            if (TargetObject == null || PropertyName == null || Value == null) throw new ArgumentNullException($"{nameof(TargetObject)}, {nameof(PropertyName)} and {nameof(Value)} properties can't be null");

            PropertyInfo targetPropertyInfo = TargetObject.GetType().GetProperty(PropertyName,
                BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Static);

            if (targetPropertyInfo == null) return;

            var converter = TypeDescriptor.GetConverter(targetPropertyInfo.PropertyType);
            if (converter.CanConvertFrom(Value.GetType()))
            {
                targetPropertyInfo.SetValue(TargetObject, converter.ConvertFrom(Value));
            }
            else if (Value.GetType().IsSubclassOf(targetPropertyInfo.PropertyType))
            {
                targetPropertyInfo.SetValue(TargetObject, Value);
            }
            else if (Value.GetType() == targetPropertyInfo.PropertyType)
            {
                targetPropertyInfo.SetValue(TargetObject, Value);
            }
        }
    }
}

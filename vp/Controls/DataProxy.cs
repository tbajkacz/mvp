using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace vp.Controls
{
    /// <summary>
    /// Freezable somehow inherits the visual tree, so it can be used to pass data from other controls to elements such as ContextMenu
    /// </summary>
    [Obsolete("x:Reference is probably better to use")]
    public class DataProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty = 
            DependencyProperty.Register("Data", typeof(object), typeof(DataProxy), new PropertyMetadata());

        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new DataProxy();
        }
    }
}

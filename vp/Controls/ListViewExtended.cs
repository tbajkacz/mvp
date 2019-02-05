using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace vp.Controls
{
    public class ListViewExtended : ListView
    {
        public static readonly DependencyProperty IsItemSelectedProperty = 
            DependencyProperty.Register("IsItemSelected", typeof(bool), typeof(ListViewExtended), new FrameworkPropertyMetadata{BindsTwoWayByDefault = true, DefaultValue = false});

        public bool IsItemSelected
        {
            get => (bool)GetValue(IsItemSelectedProperty);
            set => SetValue(IsItemSelectedProperty, value);
        }

        public ListViewExtended()
        {
            SelectionChanged += (s, e) => IsItemSelected = (SelectedItem != null);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Input;

namespace vp
{
    /// <summary>
    /// Class containing attached properties which can be used to bind a command to a routed event in style
    /// </summary>
    public static class AttachedCommand
    {
        /// <summary>
        /// The event which, when invoked, will cause the <see cref="CommandProperty"/> to be executed
        /// </summary>
        public static readonly DependencyProperty EventProperty = DependencyProperty.RegisterAttached("Event",
            typeof(RoutedEvent), typeof(AttachedCommand), new UIPropertyMetadata(PropertyChanged));

        /// <summary>
        /// The command which will be executed when the event in <see cref="EventProperty"/> is invoked
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
            typeof(ICommand), typeof(AttachedCommand), new UIPropertyMetadata(PropertyChanged));

        /// <summary>
        /// Parameters which should be passed to the command, set to null if none
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(AttachedCommand),
                new UIPropertyMetadata(PropertyChanged));

        public static void SetEvent(UIElement ui, RoutedEvent @event)
        {
            ui.SetValue(EventProperty, @event);
        }

        public static RoutedEvent GetEvent(UIElement ui)
        {
            return (RoutedEvent) ui.GetValue(EventProperty);
        }

        public static void SetCommand(UIElement ui, ICommand command)
        {
            ui.SetValue(CommandProperty, command);
        }

        public static ICommand GetCommand(UIElement ui)
        {
            return (ICommand) ui.GetValue(CommandProperty);
        }

        public static void SetCommandParameter(UIElement ui, object par)
        {
            ui.SetValue(CommandParameterProperty, par);
        }

        public static object GetCommandParameter(UIElement ui)
        {
            return ui.GetValue(CommandParameterProperty);
        }

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement ui && GetEvent(ui) != null && GetCommand(ui) != null)
            {
                ui.AddHandler(GetEvent(ui), new RoutedEventHandler(InvokeCommand(ui)));
            }
        }

        private static Action<object, EventArgs> InvokeCommand(UIElement ui)
        {
            return (s, e) =>
            {
                var param = GetCommandParameter(ui);
                var command = GetCommand(ui);
                if (command != null && param != null && command.CanExecute(param))
                {
                    command.Execute(param);
                }
                else if (command != null && param == null && command.CanExecute(null))
                {
                    command.Execute(null);
                }
            };
        }

    }
}

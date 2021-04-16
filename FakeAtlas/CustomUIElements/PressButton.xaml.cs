using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FakeAtlas.CustomUIElements
{
    /// <summary>
    /// Логика взаимодействия для PressButton.xaml
    /// </summary>
    public partial class PressButton : UserControl, INotifyPropertyChanged
    {
        public PressButton()
        {
            InitializeComponent();
            ClickHereCommand = new RoutedCommand();
            CommandBindings.Add(new CommandBinding(ClickHereCommand, ClickHereExecuted));
        }
        public static readonly DependencyProperty TextProperty;

        public static readonly DependencyProperty CommandProperty;
        static PressButton()
        {
            // Регистрация свойства
            TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(PressButton));

            CommandProperty = DependencyProperty.Register(
                        "Command",
                        typeof(string),
                        typeof(PressButton));

            ClickEvent = EventManager.RegisterRoutedEvent(
               "Click", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(PressButton));
            // остальной код
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly RoutedEvent ClickEvent;
        // обертка над событием
        public event RoutedEventHandler Click
        {
            add
            {
                // добавление обработчика
                base.AddHandler(ClickEvent, value);
            }
            remove
            {
                // удаление обработчика
                base.RemoveHandler(ClickEvent, value);
            }
        }
        public static RoutedCommand ClickHereCommand { get; set; }
        public void ClickHereExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("hello from UserControl1");
        }


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ClickEvent);
            RaiseEvent(newEventArgs);
        }
        // For demonstration purposes we raise the event when the MyButtonSimple is clicked
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            RaiseTapEvent();
        }
    }
}

using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CloseButton.xaml
    /// </summary>
    public partial class CloseButton : UserControl
    {
        public CloseButton()
        {
            InitializeComponent();
        }

        static CloseButton()
        {
            ClickEvent = EventManager.RegisterRoutedEvent(
               "Click", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CloseButton));
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

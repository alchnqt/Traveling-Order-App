using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FakeAtlas.Context.UnitOfWork;
using Microsoft.VisualStudio.PlatformUI;

namespace FakeAtlas.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private List<Order> orders = new List<Order>();

        private ObservableCollection<Order> _selectedOrders = new();

        public ObservableCollection<Order> SelectedOrders
        {
            get { return _selectedOrders; }
            set
            {
                _selectedOrders = value;
                OnPropertyChanged(nameof(SelectedOrders));
            }
        }

        private Order _selectedOrder = new();

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set 
            { 
                _selectedOrder = value;
                OnPropertyChanged(nameof(SelectedOrder));
            }
        }



        public SearchViewModel()
        {
            SelectedOrders = new();
            orders.Add(new Order { PathFrom = "Марьина Горка", PathTo = "Минск", OrderTime = DateTime.Now, DepartureTime = DateTime.Now});
            orders.Add(new Order { PathFrom = "Минск", PathTo = "Марьина Горка", OrderTime = DateTime.Now, DepartureTime = DateTime.Now});
            orders.Add(new Order { PathFrom = "Минск", PathTo = "Гродно", OrderTime = DateTime.Now, DepartureTime = DateTime.Now});

        }

        private ICommand selectCommand;
        public ICommand SelectCommand => selectCommand ??= new DelegateCommand(Select);

        private void Select()
        {
            MessageBox.Show("ALL RIGHT");
        }

        private ICommand findCommand;
        public ICommand FindCommand => findCommand ??= new DelegateCommand(Find);

        private void Find()
        {
            try
            {
                using (UnitOfWork unit = new())
                {
                 //   var availableOrders = from order in unit.OrdersRepository.Get()
                 //                         where order.DepartureTime.GetValueOrDefault().Year == SelectedOrder.DepartureTime.GetValueOrDefault().Year &&
                 //order.DepartureTime.GetValueOrDefault().Month == SelectedOrder.DepartureTime.GetValueOrDefault().Month &&
                 //order.DepartureTime.GetValueOrDefault().Day == SelectedOrder.DepartureTime.GetValueOrDefault().Day &&
                 //order.PathFrom.Equals(SelectedOrder.PathFrom) && order.PathTo.Equals(SelectedOrder.PathTo)
                 //                         select order;

                //SelectedOrders = new ObservableCollection<Order>(availableOrders);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace FakeAtlas.ViewModels
{
    public class OrdersViewModel : ViewModelBase
    {

        private IEnumerable<Order> _orders;

        public IEnumerable<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged(nameof(Orders));

            }
        }

        public OrdersViewModel()
        {
            Orders = new List<Order> {
                new Order { PathFrom = "Марьина Горка", PathTo = "Минск", OrderTime = DateTime.Now},
                new Order { PathFrom = "Минск", PathTo = "Марьина Горка", OrderTime = DateTime.Now },
                new Order { PathFrom = "Минск", PathTo = "Гродно", OrderTime = DateTime.Now }
                };
        }
    }
}

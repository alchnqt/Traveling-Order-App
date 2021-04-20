using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.ViewModels.Management
{
    public class ViewManager
    {
        public void ShowView<T>(BaseViewModel<T> viewModel)
        where T : IViewModel, new()
        {
            T view = new T();

        }
    }
}


using FakeAtlas.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FakeAtlasTests
{
    public class ViewModelTests
    {
        [Fact]
        public void SignInTest()
        {
            SignInViewModel view = new();
            Assert.True(view.SignInCommand.CanExecute(null));
            Assert.Throws<InvalidOperationException>(() => view.SignInCommand.Execute(null));
        }

        [Fact]
        public void SignUpTest()
        {
            SignUpViewModel view = new();
            Assert.True(view.SignUpCommand.CanExecute(null));
            Assert.Throws<InvalidOperationException>(() => view.SignUpCommand.Execute(null));
        }

        [Fact]
        public void SearchTest()
        {
            SearchViewModel view = new();
            Assert.True(view.FindCommand.CanExecute(null));
        }

        [Fact]
        public void RemoveOrderTest()
        {
            OrdersViewModel view = new();
            Assert.True(view.RemoveOrderCommand.CanExecute(null));
        }
    }
}

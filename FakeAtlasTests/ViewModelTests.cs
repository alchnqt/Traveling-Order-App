
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
        public void SignInCommand_CanExeuteNThrowException_ReturnsTrue()
        {
            SignInViewModel view = new();
            Assert.True(view.SignInCommand.CanExecute(null));
            Assert.Throws<InvalidOperationException>(() => view.SignInCommand.Execute(null));
        }

        [Fact]
        public void SignUpCommand_CanExeuteNThrowException_ReturnsTrue()
        {
            SignUpViewModel view = new();
            Assert.True(view.SignUpCommand.CanExecute(null));
            Assert.Throws<InvalidOperationException>(() => view.SignUpCommand.Execute(null));
        }

        [Fact]
        public void FindCommand__CanExeuteNThrowException_ReturnsTrue()
        {
            SearchViewModel view = new();
            Assert.True(view.FindCommand.CanExecute(null));
        }

        [Fact]
        public void RemoveOrderCommand__CanExeuteNThrowException_ReturnsTrue()
        {
            OrdersViewModel view = new();
            Assert.True(view.RemoveOrderCommand.CanExecute(null));
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.Models
{
    public class SignInModel : ModelBase
    {
        public string UserLogin { get; set; }
        public SecureString UserPassword { get; set; }
    }
}

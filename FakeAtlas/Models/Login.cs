﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FakeAtlas.Models
{
    public class Login : ObjectModel
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.API.Models.User.Auth
{
    public class LoginDto
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

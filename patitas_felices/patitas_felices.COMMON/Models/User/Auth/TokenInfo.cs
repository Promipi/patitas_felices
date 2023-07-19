using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Models.User.Auth
{
    public class TokenInfo
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}

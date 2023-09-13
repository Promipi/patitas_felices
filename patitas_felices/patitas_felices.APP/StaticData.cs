using patitas_felices.Common.Models.User.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.APP
{
    public static class StaticData
    {
        public static string ConnectionApi = "http://192.168.19.177:5000";
        public static TokenInfo TokenUser { get; set; }
    }
}

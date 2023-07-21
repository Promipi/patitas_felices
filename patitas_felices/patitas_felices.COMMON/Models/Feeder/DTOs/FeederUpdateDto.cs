using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Models.Feeder.DTOs
{
    public class FeederUpdateDto
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string Location { get; set; }

        public string Image { get; set; }
    }
}

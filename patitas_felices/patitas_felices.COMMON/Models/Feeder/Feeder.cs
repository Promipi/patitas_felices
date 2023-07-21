using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Models.Feeder
{
    public class Feeder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public string Location { get; set; }

        public string Image { get; set; }
    }
}

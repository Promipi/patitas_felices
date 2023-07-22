using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Models.Photo
{
   public class Photo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Link { get; set; }

        [ForeignKey("FeederId")]
        public string FeederId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

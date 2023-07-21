using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Models.Photo.DTOs
{
    public class PhotoGetDto
    {
        public string Id { get; set; }
        public string Link { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

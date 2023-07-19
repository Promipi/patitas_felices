using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.Common.Responses
{
    public class PostResponseDto<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Entity { get; set; }
    }
}

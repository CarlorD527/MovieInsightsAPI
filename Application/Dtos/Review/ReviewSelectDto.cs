using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Review
{
    public class ReviewSelectDto
    {
        public string? username { get; set; }

        public string? score { get; set; }

        public string? reviewcontent { get; set; }

        public string? datedCreated { get; set; }
    }
}


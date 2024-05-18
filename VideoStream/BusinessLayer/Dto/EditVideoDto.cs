using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class EditVideoDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public Guid CategoryId { get; set; }
        public string? ImagePath { get; set; }
    }
}

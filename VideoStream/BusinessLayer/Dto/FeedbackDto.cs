using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dto
{
    public class FeedbackDto
    {
        public Guid Id { get; set; }
        public bool IsPositive { get; set; }
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

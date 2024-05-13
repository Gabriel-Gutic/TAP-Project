using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface IFeedbackService
    {
        public IEnumerable<FeedbackDto> GetAll();

        public FeedbackDto? Get(Guid id);

        public int CountPositive(Guid videoId);
        public int CountNegative(Guid videoId);

        public FeedbackDto? FindFeedback(Guid userId, Guid videoId);

        public void Insert(FeedbackDto viewDto);

        public void Update(Guid id, FeedbackDto viewDto);

        public void Edit(Guid userId, Guid videoId, bool feedback);

        public void Delete(Guid id);
    }
}

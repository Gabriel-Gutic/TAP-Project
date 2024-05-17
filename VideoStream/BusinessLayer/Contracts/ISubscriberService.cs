using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ISubscriberService
    {
        public IEnumerable<SubscriberDto> GetAll();

        public IEnumerable<SubscriberDto> GetCreators(Guid subscriberId);

        public SubscriberDto? Get(Guid id);

        public int Count(Guid creatorId);

        public IEnumerable<SubscriberDto> GetSubscribers(Guid creatorId);

        public bool IsSubscriber(Guid creatorId, Guid subscriberId);

        public void Subscribe(Guid creatorId, Guid subscriberId);
        public void Unsubscribe(Guid creatorId, Guid subscriberId);
    }
}

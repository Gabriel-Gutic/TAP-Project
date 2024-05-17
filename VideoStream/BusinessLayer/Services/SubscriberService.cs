using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Logger;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly IRepository<Subscriber> _subscriberRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IAppLogger _logger;


        public SubscriberService(IRepository<Subscriber> subscriberRepository, IRepository<User> userRepository, IAppLogger logger)
        {
            _subscriberRepository = subscriberRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public IEnumerable<SubscriberDto> GetAll()
        {
            IEnumerable<SubscriberDto> subscribers =
                from subscriber in _subscriberRepository.GetAll()
                join creator in _userRepository.GetAll() on subscriber.CreatorUserId equals creator.Id
                join follower in _userRepository.GetAll() on subscriber.SubscriberUserId equals follower.Id
                select new SubscriberDto()
                {
                    CreatorId = creator.Id,
                    CreatorUsername = creator.Username,
                    SubscriberId = follower.Id,
                    SubscriberUsername = follower.Username,
                };
            return subscribers;
        }

        public IEnumerable<SubscriberDto> GetCreators(Guid subscriberId)
        {
            IEnumerable<SubscriberDto> subscribers =
                from subscriber in _subscriberRepository.GetAll()
                where subscriber.SubscriberUserId == subscriberId
                join creator in _userRepository.GetAll() on subscriber.CreatorUserId equals creator.Id
                join follower in _userRepository.GetAll() on subscriber.SubscriberUserId equals follower.Id
                select new SubscriberDto()
                {
                    CreatorId = creator.Id,
                    CreatorUsername = creator.Username,
                    SubscriberId = follower.Id,
                    SubscriberUsername = follower.Username,
                };
            return subscribers;
        }

        public SubscriberDto? Get(Guid id)
        {
            var subscriber = _subscriberRepository.GetById(id);
            if (subscriber == null)
            {
                return null;
            }

            var creator = _userRepository.GetById(subscriber.CreatorUserId);
            var follower = _userRepository.GetById(subscriber.SubscriberUserId);

            if (creator == null || follower == null)
            {
                return null;
            }

            return new SubscriberDto()
            {
                CreatorId = creator.Id,
                CreatorUsername = creator.Username,
                SubscriberId = follower.Id,
                SubscriberUsername = follower.Username,
            };
        }

        public bool IsSubscriber(Guid creatorId, Guid subscriberId)
        {
            return _subscriberRepository.Contains(s => s.CreatorUserId == creatorId && s.SubscriberUserId == subscriberId);
        }

        public int Count(Guid creatorId)
        {
            return _subscriberRepository.Count(s => s.CreatorUserId == creatorId);
        }

        public void Subscribe(Guid creatorId, Guid subscriberId)
        {
            if (creatorId == subscriberId)
            {
                throw new ArgumentException("A user can not subscriber to itself");
            }

            if (_subscriberRepository.Contains(s => s.CreatorUserId == creatorId && s.SubscriberUserId == subscriberId))
            {
                throw new EntityAlreadyExistException();
            }

            _subscriberRepository.Add(new Subscriber()
            {
                CreatorUserId = creatorId,
                SubscriberUserId = subscriberId,
            });

            _subscriberRepository.SaveChanges();
            _logger.Info("New subscriber");
        }

        public void Unsubscribe(Guid creatorId, Guid subscriberId)
        {
            var subscriber = _subscriberRepository.Find(s => s.CreatorUserId == creatorId && s.SubscriberUserId == subscriberId).FirstOrDefault();
            if (subscriber == null)
            {
                throw new EntityNotFoundException("Subscriber not found");
            }
            _subscriberRepository.Delete(subscriber.Id);
            _subscriberRepository.SaveChanges();

            _logger.Info("Subscriber deleted");
        }

        public IEnumerable<SubscriberDto> GetSubscribers(Guid creatorId)
        {
            IEnumerable<SubscriberDto> subscribers =
                from subscriber in _subscriberRepository.GetAll()
                where subscriber.CreatorUserId == creatorId
                join creator in _userRepository.GetAll() on subscriber.CreatorUserId equals creator.Id
                join follower in _userRepository.GetAll() on subscriber.SubscriberUserId equals follower.Id
                select new SubscriberDto()
                {
                    CreatorId = creator.Id,
                    CreatorUsername = creator.Username,
                    SubscriberId = follower.Id,
                    SubscriberUsername = follower.Username,
                };
            return subscribers;
        }
    }
}

using BusinessLayer.Contracts;
using BusinessLayer.Dto;
using BusinessLayer.Exceptions;
using BusinessLayer.Logger;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<Feedback> _feedbackRepository;
        private readonly IAppLogger _logger;

        public FeedbackService(IRepository<Feedback> feedbackRepository, IAppLogger logger)
        {
            _feedbackRepository = feedbackRepository;
            _logger = logger;
        }
        
        public IEnumerable<FeedbackDto> GetAll()
        {
            return _feedbackRepository.GetAll()
                .Select(f => new FeedbackDto()
                {
                    Id = f.Id,
                    IsPositive = f.IsPositive,
                    UserId = f.UserId,
                    VideoId = f.VideoId,
                    CreatedAt = f.CreatedAt,
                });
        }

        public FeedbackDto? Get(Guid id)
        {
            var feedback = _feedbackRepository.GetById(id);
            if (feedback == null)
            {
                return null;
            }
            return new FeedbackDto()
            {
                Id = feedback.Id,
                IsPositive = feedback.IsPositive,
                UserId = feedback.UserId,
                VideoId = feedback.VideoId,
                CreatedAt = feedback.CreatedAt,
            };
        }

        public int CountPositive(Guid videoId)
        {
            return _feedbackRepository.Count(f => f.VideoId == videoId && f.IsPositive);
        }

        public int CountNegative(Guid videoId)
        {
            return _feedbackRepository.Count(f => f.VideoId == videoId && !f.IsPositive);
        }

        public void Insert(FeedbackDto viewDto)
        {
            if (_feedbackRepository.Contains(f => f.UserId == viewDto.UserId && f.VideoId == viewDto.VideoId))
            {
                throw new Exception("Feedback already exist");
            }

            _feedbackRepository.Add(new Feedback()
            {
                IsPositive = viewDto.IsPositive,
                UserId = viewDto.UserId,
                VideoId = viewDto.VideoId,
            });

            _feedbackRepository.SaveChanges();

            _logger.Info("New item inserted in Feedback Table");
        }

        public void Update(Guid id, FeedbackDto viewDto)
        {
            Feedback feedback = _feedbackRepository.GetById(id);
            if (feedback == null) 
            {
                throw new EntityNotFoundException("Feedback not found");
            }

            feedback.IsPositive = viewDto.IsPositive;
            feedback.UserId = viewDto.UserId;
            feedback.VideoId = viewDto.VideoId;

            _feedbackRepository.SaveChanges();

            _logger.Info("Item updated in Feedback Table");
        }

        public void Edit(Guid userId, Guid videoId, bool feedback)
        {
            Feedback? _feedback = _feedbackRepository.Find(f => f.UserId == userId && f.VideoId == videoId).FirstOrDefault();
            if (_feedback == null)
            {
                throw new EntityNotFoundException("Feedback not found");
            }

            _feedback.IsPositive = feedback;

            _feedbackRepository.SaveChanges();

            _logger.Info("Item edited in Feedback Table");
        }

        public void Delete(Guid id)
        {
            if (!_feedbackRepository.Delete(id))
            {
                throw new EntityNotFoundException("Feedback not found");
            }
            _feedbackRepository.SaveChanges();

            _logger.Info("Item deleted from Feedback Table");
        }

        public FeedbackDto? FindFeedback(Guid userId, Guid videoId)
        {
            var feedback = _feedbackRepository.Find(f => f.UserId ==  userId && f.VideoId == videoId);
        
            if (feedback.IsNullOrEmpty())
            {
                return null;
            }

            var f = feedback.First();

            return new FeedbackDto() 
            { 
                Id = f.Id,
                IsPositive = f.IsPositive,
                UserId = f.UserId,
                VideoId = f.VideoId,
                CreatedAt = f.CreatedAt,
            };
        }
    }
}

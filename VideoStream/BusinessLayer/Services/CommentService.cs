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
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IAppLogger _logger;

        public CommentService(IRepository<Comment> commentRepository, IRepository<User> userRepository, IAppLogger logger)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public IEnumerable<CommentDto> GetAll()
        {
            return _commentRepository.GetAll()
                .Select(c => new CommentDto()
                {
                    Id = c.Id,
                    Message = c.Message,
                    UserId = c.UserId,
                    VideoId = c.VideoId,
                    CreatedAt = c.CreatedAt
                });
        }

        public CommentDto? Get(Guid id)
        {
            var comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                return null;
            }
            return new CommentDto()
            { 
                Id = comment.Id,
                Message = comment.Message,
                UserId = comment.UserId,
                VideoId = comment.VideoId,
                CreatedAt = comment.CreatedAt
            };
        }

        public void Insert(CommentDto userDto)
        {
            _commentRepository.Add(new Comment()
            {
                Message = userDto.Message,
                UserId = userDto.UserId,
                VideoId = userDto.VideoId,
            });

            _commentRepository.SaveChanges();

            _logger.Info("New item inserted in Comment Table");
        }

        public void Update(Guid id, CommentDto userDto)
        {
            Comment comment = _commentRepository.GetById(id);
            if (comment == null)
            {
                throw new EntityNotFoundException("Comment not found");
            }

            comment.Message = userDto.Message;
            comment.UserId = userDto.UserId;
            comment.VideoId = userDto.VideoId;
            _commentRepository.SaveChanges();
            _logger.Info("Item updated in Comment Table");
        }

        public void Delete(Guid id)
        {
            if (!_commentRepository.Delete(id))
            {
                throw new EntityNotFoundException("Comment not found");
            }
            _commentRepository.SaveChanges();

            _logger.Info("Item deleted from Comment Table");
        }

        public IEnumerable<CommentDto> GetForVideo(Guid videoId)
        {
            IEnumerable<CommentDto> comments =
                from comment in _commentRepository.GetAll()
                where comment.VideoId == videoId
                join user in _userRepository.GetAll() on comment.UserId equals user.Id
                select new CommentDto()
                {
                    Id = comment.Id,
                    Message = comment.Message,
                    UserId = comment.UserId,
                    Username = user.Username,
                    VideoId = videoId,
                    CreatedAt = comment.CreatedAt,
                };

            return comments;
        }
    }
}

using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Contracts
{
    public interface ICommentService
    {
        public IEnumerable<CommentDto> GetAll();

        public CommentDto? Get(Guid id);

        public IEnumerable<CommentDto> GetForVideo(Guid videoId);

        public void Insert(CommentDto userDto);

        public void Update(Guid id, CommentDto userDto);

        public void Delete(Guid id);
    }
}

using BusinessLayer.Dto;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.VideoSelector
{
    public interface IVideoSelector
    {
        public IEnumerable<VideoDto> Select(UserDto user, int count);
    }
}

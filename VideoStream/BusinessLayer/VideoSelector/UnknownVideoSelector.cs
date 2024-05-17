using BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.VideoSelector
{
    public class UnknownVideoSelector : IVideoSelector
    {
        public IEnumerable<VideoDto> Select(UserDto user, int count)
        {
            return new List<VideoDto>();
        }
    }
}

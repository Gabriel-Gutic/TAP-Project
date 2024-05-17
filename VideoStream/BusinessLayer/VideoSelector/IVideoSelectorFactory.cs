using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.VideoSelector
{
    public interface IVideoSelectorFactory
    {
        public IVideoSelector Create(string name);
    }
}

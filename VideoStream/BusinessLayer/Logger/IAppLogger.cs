using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logger
{
    public interface IAppLogger
    {
        public void Info(string info);
        public void Error(string error);
        public void Warning(string warning);
    }
}

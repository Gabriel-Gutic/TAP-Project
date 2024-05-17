using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Types
{
    public abstract class BaseNotification
    {
        // User that receives the notification
        protected Guid _userId;

        protected BaseNotification(Guid userId)
        {
            _userId = userId;
        }

        public Guid UserId
        {
            get { return _userId; }
        }

        public abstract string Message
        {
            get;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Notifications.Types
{
    public class VideoUploadNotification : BaseNotification
    {
        protected string _creator;

        public VideoUploadNotification(Guid userId, string creator)
            : base(userId)
        {
            _creator = creator;
        }

        public override string Message
        {
            get
            {
                return $"{_creator} just uploaded a new video!";
            }
        }
    }
}

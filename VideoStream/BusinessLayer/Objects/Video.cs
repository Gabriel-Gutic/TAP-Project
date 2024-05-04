using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Objects
{
    public class Video
    {
        private Guid _id;
        private string _title;
        private string _description;
        private byte[] _image;
        private byte[] _data;
        private bool _isPublic;

        private Guid _userId;
        private Guid _categoryId;

        private ICollection<Feedback> _feedback;
        private ICollection<Comment> _comments;
        private ICollection<View> _views;

        public Video(Guid id, string title, string description, byte[] image, byte[] data, bool isPublic, Guid userId, Guid categoryId, ICollection<Feedback> feedback, ICollection<Comment> comments, ICollection<View> views) 
        { 
            _id = id;
            _title = title;
            _description = description;
            _image = image;
            _data = data;
            _isPublic = isPublic;
            _userId = userId;
            _categoryId = categoryId;
            _feedback = feedback;
            _comments = comments;
            _views = views;
        }

        public Guid Id { get { return _id; } }

        public string Title { get { return _title; } }
        public string Description { get { return _description; } }
        public byte[] Image { get { return _image; } }
        public byte[] Data { get { return _data; } }
        public bool IsPublic { get { return _isPublic; } }
        public Guid UserId { get { return _userId;} }
        public Guid CategoryId { get { return _categoryId; } }
    }
}

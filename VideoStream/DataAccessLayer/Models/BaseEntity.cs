namespace DataAccessLayer.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public BaseEntity() 
        { 
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }
}

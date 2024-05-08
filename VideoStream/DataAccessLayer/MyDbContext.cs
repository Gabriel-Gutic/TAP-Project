using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer
{
    public class MyDbContext : DbContext
    {
        private readonly string _windowsConnectionString = @"Server=localhost\SQLEXPRESS;Database=TAPProject;Trusted_Connection=True;TrustServerCertificate=True;";

        public DbSet<User> Users { get; set; }
        public DbSet<VideoCategory> VideoCategories { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Comment> Comments { get; set; }
		public DbSet<View> Views { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Notification> Notifications { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

			CreateRelations(builder);

			InsertData(builder);
		}

		private void CreateRelations(ModelBuilder builder)
		{
			// Video >- VideoCategory
			builder.Entity<Video>()
				.HasOne(v => v.Category)
				.WithMany(vc => vc.Videos)
				.HasForeignKey(v => v.CategoryId);

			// User -< Video
			builder.Entity<Video>()
				.HasOne(v => v.User)
				.WithMany(u => u.Videos)
				.HasForeignKey(v => v.UserId);

			// User >-< User
			builder.Entity<User>()
				.HasMany(u => u.Subscribers)
				.WithMany(u => u.Creators)
				.UsingEntity<Subscriber>(
					s1 => s1.HasOne(s => s.SubscriberUser).WithMany().HasForeignKey(s => s.SubscriberUserId),
					s2 => s2.HasOne(s => s.CreatorUser).WithMany().HasForeignKey(s => s.CreatorUserId)
				);

			// User -< Feedback
			builder.Entity<User>()
				.HasMany(u => u.Feedback)
				.WithOne(f => f.User)
				.HasForeignKey(f => f.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// Video -< Feedback
			builder.Entity<Video>()
				.HasMany(v => v.Feedback)
				.WithOne(f => f.Video)
				.HasForeignKey(f => f.VideoId);

			// User -< View
			builder.Entity<User>()
				.HasMany(u => u.Views)
				.WithOne(w => w.User)
				.HasForeignKey(w => w.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// Video -< View
			builder.Entity<Video>()
				.HasMany(v => v.Views)
				.WithOne(w => w.Video)
				.HasForeignKey(w => w.VideoId);

			// User -< Comment
			builder.Entity<User>()
				.HasMany(u => u.Comments)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.NoAction);

			// Video -< Comment
			builder.Entity<Video>()
				.HasMany(v => v.Comments)
				.WithOne(c => c.Video)
				.HasForeignKey(c => c.VideoId);

			// User -< Notification
			builder.Entity<User>()
				.HasMany(u => u.Notifications)
				.WithOne(n => n.User)
				.HasForeignKey(n => n.UserId);
		}

		private void InsertData(ModelBuilder builder)
		{
			builder.Entity<VideoCategory>().HasData(Array.Empty<VideoCategory>());
			builder.Entity<User>().HasData(Array.Empty<User>());
			builder.Entity<Video>().HasData(Array.Empty<Video>());
			builder.Entity<Feedback>().HasData(Array.Empty<Feedback>());
			builder.Entity<View>().HasData(Array.Empty<View>());
			builder.Entity<Comment>().HasData(Array.Empty<Comment>());
			builder.Entity<Subscriber>().HasData(Array.Empty<Subscriber>());

			DateTime startDate = new DateTime(2010, 1, 1);

			// Insert data into the tables 
			// VideoCategory Table
			VideoCategory[] videoCategories =
			{
				new VideoCategory("Action"){ CreatedAt = startDate },
				new VideoCategory("Gaming"){ CreatedAt = startDate },
				new VideoCategory("Vloging"){ CreatedAt = startDate },
			};

			builder.Entity<VideoCategory>().HasData(videoCategories);
		}
	}
}

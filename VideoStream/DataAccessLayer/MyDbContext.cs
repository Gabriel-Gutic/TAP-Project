using DataAccessLayer.Models;
using DataAccessLayer.Tools;
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

			Random random = new Random();

			DateTime startDate = new DateTime(2010, 1, 1);
			IDateTimeHandler dateTimeHandler = new DateTimeHandler();

			// Insert data into the tables 
			// VideoCategory Table
			VideoCategory[] videoCategories =
			{
				new VideoCategory("Action"){ CreatedAt = startDate },
				new VideoCategory("Gaming"){ CreatedAt = startDate },
				new VideoCategory("Vloging"){ CreatedAt = startDate },
			};

			builder.Entity<VideoCategory>().HasData(videoCategories);

			// User Table
			string[] usernames =
			{
				"Gabi",
				"Ana",
				"Mihai",
				"Maria",
				"Ioana",
				"Tudor",
				"Alex",
				"Mario",
				"Beatrice"
			};

			IPasswordHandler passwordHandler = new PasswordHandler();
			IImageHandler imageHandler = new ImageHandler();

			User[] users = new User[usernames.Length];
			for (int i = 0; i < usernames.Length; i++)
			{
				imageHandler.Read(@"SeedData\Images", usernames[i], out byte[]? image);

				User user = new User()
				{
					Username = usernames[i],
					Email = $"{usernames[i].ToLower()}@gmail.com",
					// The password of each user is the lowercase username
					Password = passwordHandler.Hash(usernames[i].ToLower()),
					Image = image,
					CreatedAt = dateTimeHandler.RandomBetween(startDate, new DateTime(2015, 1, 1))
				};

				users[i] = user;
			}

			users[0].IsAdmin = true;
			users[users.Length - 1].IsActive = false;

			builder.Entity<User>().HasData(users);

			// Subscriber Table
			List<Subscriber> subscribers = new List<Subscriber>();

			for (int i = 1; i < users.Length; i++)
			{
				subscribers.Add(new Subscriber()
				{
					SubscriberUserId = users[i].Id,
					CreatorUserId = users[0].Id,
				});
			}

			for (int i = 2; i < users.Length; i += 2)
			{
				subscribers.Add(new Subscriber()
				{
					SubscriberUserId = users[i].Id,
					CreatorUserId = users[3].Id,
				});
			}

			for (int i = 3; i < users.Length; i += 3)
			{
				subscribers.Add(new Subscriber()
				{
					SubscriberUserId = users[i].Id,
					CreatorUserId = users[5].Id,
				});
			}

			builder.Entity<Subscriber>().HasData(subscribers);

			return;

			// This code is way too slow. Is way too hard too load even a single 3 seconds
			// video.

			/*
			// Video Table
			// Initial code for loading all the videos:
			
			// int[] creators = { 0, 3, 4 };
			// List<Video> videos = new List<Video>();
			// 
			// IVideoHandler videoHandler = new VideoHandler();
			// 
			// foreach (var category in videoCategories)
			// {
			// 	int i = 0;
			// 	while (true)
			// 	{
			// 		string name = category.Name + i;
			// 
			// 		if (!videoHandler.Read(@"SeedData\Videos", name, out byte[]? video) ||
			// 			!imageHandler.Read(@"SeedData\VideoImages", name, out byte[]? image))
			// 		{
			// 			break;
			// 		}
			// 
			// 		int creator = creators[i % 3];
			// 
			// 		videos.Add(new Video()
			// 		{
			// 			Title = $"{category.Name} Video Episode {i}",
			// 			Description = $"This is an episode with '{category.Name}' Name\n Episode Number: {i}",
			// 			Data = video,
			// 			Image = image,
			// 			IsPublic = true,
			// 			UserId = users[creator].Id,
			// 			CategoryId = category.Id,
			// 			CreatedAt = dateTimeHandler.RandomAfter(users[creator].CreatedAt),
			// 		});
			// 
			// 		i++;
			// 	}
			// }
			// builder.Entity<Video>().HasData(videos);
			

			
			// New code that loads only a few videos
			List<Video> videos = new List<Video>();

			IVideoHandler videoHandler = new VideoHandler();

			VideoCategory videoCategory = videoCategories.First(c => c.Name == "Gaming");

			for (int i = 0; i < 1; i++)
			{
				string name = videoCategory.Name + i;
				if (!videoHandler.Read(@"SeedData\Videos", name, out byte[]? video) ||
					!imageHandler.Read(@"SeedData\VideoImages", name, out byte[]? image))
				{
					continue;
				}

				videos.Add(new Video()
				{
					Title = $"{videoCategory.Name} Video Episode {i}",
					Description = $"This is an episode with '{videoCategory.Name}' \n Episode Number: {i}",
					Data = video,
					Image = image,
					IsPublic = true,
					UserId = users[0].Id,
					CategoryId = videoCategory.Id,
					CreatedAt = dateTimeHandler.RandomAfter(users[0].CreatedAt),
				});
			}
			builder.Entity<Video>().HasData(videos);

			// Comment Table
			string[] randomComments =
			{
				"Wow, this video made me smile again. Thanks for the moments of joy!",
				"Can't believe I stumbled upon this channel! Keep delighting us with your amazing content!",
				"I love how well-structured this video is. It's informative and engaging!",
				"When's the next episode coming out? Can't wait for the continuation of this captivating story!",
				"Great job! You did an awesome job with the editing and montage of this video.",
				"Truly inspiring! Thanks for sharing your story and motivating all of us!",
				"This video made me laugh and cry at the same time. A true emotional masterpiece!",
				"How talented do you have to be to create something like this? Congratulations on your creative skills!",
				"Can't believe I've missed this video until now! It's absolutely fantastic!",
				"I can't stop watching this video! It's so addictive and full of surprises!",
			};

			List<Comment> comments = new List<Comment>();

			// Generate 20 random comments
			for (int i = 0; i < 20; i++)
			{
				User user = users[random.Next(1, users.Length)];
				Video video = videos[random.Next(videos.Count)];

				comments.Add(new Comment()
				{
					Message = randomComments[random.Next(randomComments.Length)],
					UserId = user.Id,
					VideoId = video.Id,
					CreatedAt = dateTimeHandler.RandomAfter(dateTimeHandler.Max(user.CreatedAt, video.CreatedAt)),
				});
			}

			builder.Entity<Comment>().HasData(comments);

			// View Table
			List<View> views = new List<View>();
			for (int i = 0; i < 2000; i++)
			{
				User user = users[random.Next(users.Length)];
				Video video = videos[random.Next(videos.Count)];

				views.Add(new View()
				{
					UserId = user.Id,
					VideoId = video.Id,
					CreatedAt = dateTimeHandler.RandomAfter(dateTimeHandler.Max(user.CreatedAt, video.CreatedAt)),
				});
			}

			builder.Entity<View>().HasData(views);

			// Feedback Table
			List<Feedback> feedback = new List<Feedback>();
			for (int i = 0; i < users.Length; i++)
			{
				int min = random.Next(videos.Count / 2);
				int max = random.Next(videos.Count / 2, videos.Count);
				for (int j = min; j <= max; j++)
				{
					feedback.Add(new Feedback()
					{
						UserId = users[i].Id,
						VideoId = videos[j].Id,
						IsPositive = random.Next(4) == 0 ? false : true,
						CreatedAt = dateTimeHandler.RandomAfter(dateTimeHandler.Max(users[i].CreatedAt, videos[j].CreatedAt)),
					});
				}
			}

			builder.Entity<Feedback>().HasData(feedback);

			*/
		}
	}
}

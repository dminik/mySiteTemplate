namespace Trackyt.Core.DAL.DataModel
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	public class TrackytDataContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Credential> Credentials { get; set; }

		public DbSet<Project> Projects { get; set; }
		public DbSet<SpentTime> SpentTimes { get; set; }

		public TrackytDataContext() { }

		public TrackytDataContext(string connection) : base(connection) { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			//modelBuilder.Entity<Instructor>()
			//	.HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);

			//modelBuilder.Entity<Course>()
			//	.HasMany(c => c.Instructors).WithMany(i => i.Courses)
			//	.Map(t => t.MapLeftKey("CourseID")
			//		.MapRightKey("PersonID")
			//		.ToTable("CourseInstructor"));

			//modelBuilder.Entity<Department>()
			//	.HasOptional(x => x.Administrator);
		}
	}
}
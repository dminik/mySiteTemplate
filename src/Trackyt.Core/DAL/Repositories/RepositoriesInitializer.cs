namespace Trackyt.Core.DAL.Repositories
{	
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Data.Entity;

	using Trackyt.Core.DAL.DataModel;

	public class RepositoriesInitializer : DropCreateDatabaseIfModelChanges<TrackytDataContext>
	{
		protected override void Seed(TrackytDataContext context)
		{					
			var projects = new List<Project>
            {
                new Project { Name = "Project1", SpentTimes = new List<SpentTime>()},
				new Project { Name = "Project2", SpentTimes = new List<SpentTime>()}
              
            };
			projects.ForEach(s => context.Projects.Add(s));

			context.SaveChanges();

			var spentTimes = new List<SpentTime>
            {
                new SpentTime {  ProjectId = projects[0].Id, Amount = 30, Description = "Description 1_1"},
				new SpentTime {  ProjectId = projects[0].Id, Amount = 40, Description = "Description 1_2"},

				new SpentTime {  ProjectId = projects[1].Id, Amount = 30, Description = "Description 2_1"},
				new SpentTime {  ProjectId = projects[1].Id, Amount = 40, Description = "Description 2_2"},
				new SpentTime {  ProjectId = projects[1].Id, Amount = 40, Description = "Description 2_3"}				              
            };

			spentTimes.ForEach(s => context.SpentTimes.Add(s));
			context.SaveChanges();
		}

		public static void InitDB(TrackytDataContext context)
		{
			Database.SetInitializer<TrackytDataContext>(new RepositoriesInitializer());
			context.Database.Initialize(true);
		}
	}
}
namespace Trackyt.Core.DAL.Repositories
{	
	using System.Collections.Generic;
	using System.Data.Entity;

	using Trackyt.Core.DAL.DataModel;

	public class RepositoriesInitializer : DropCreateDatabaseIfModelChanges<TrackytDataContext>
	{
		protected override void Seed(TrackytDataContext context)
		{
			var projects = new List<Project>
            {
                new Project { Name = "Project1"},
				new Project { Name = "Project2"}
              
            };
			projects.ForEach(s => context.Projects.Add(s));
			
			var spentTimes = new List<SpentTime>
            {
                new SpentTime {  Project = projects[0], Amount = 30, Description = "Description 1_1"},
				new SpentTime {  Project = projects[0], Amount = 40, Description = "Description 1_2"},

				new SpentTime {  Project = projects[1], Amount = 30, Description = "Description 2_1"},
				new SpentTime {  Project = projects[1], Amount = 40, Description = "Description 2_2"},
				new SpentTime {  Project = projects[1], Amount = 40, Description = "Description 2_3"}				              
            };
			spentTimes.ForEach(s => context.SpentTimes.Add(s));
			context.SaveChanges();
		}
	}
}
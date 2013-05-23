using System;
using System.Linq;
using Trackyt.Core.DAL.Repositories;

namespace Trackyt.Core.Services.Impl
{
	using Trackyt.Core.DAL.DataModel;
	using Trackyt.Core.DAL.Repositories.Impl;
	using Trackyt.Core.Exceptions;

	public class WorkLoggerServices //: IApiService
	{
		private IProjectsRepository ProjectsRepository { get; set; }
		private ISpentTimesRepository SpentTimesRepository { get; set; }


		public WorkLoggerServices(IProjectsRepository projects, ISpentTimesRepository spentTimes)
		{
			ProjectsRepository = projects;
			SpentTimesRepository = spentTimes;
		}

		public void AddProject(uint amount, int projectId)
		{
			var project = ProjectsRepository.Projects.(u => u.Id == projectId).SingleOrDefault();
			if (project == null)
			{
				throw new DataLayerException(Resources.ProjectDoNotExists);
			}

			SpentTimesRepository.Save(new SpentTime() { Amount = amount, ProjectId = projectId });
		}

		public void AddTime(uint amount, int projectId)
		{
			var project = ProjectsRepository.Projects.Where(u => u.Id == projectId).SingleOrDefault();
			if (project == null)
			{
				throw new DataLayerException(Resources.ProjectDoNotExists);
			}

			SpentTimesRepository.Save(new SpentTime() { Amount = amount, ProjectId = projectId });
		}

		//public string GetApiToken(string email, string password)
		//{
		//	var user = _users.Users.Where(u => u.Email == email).SingleOrDefault();
		//	if (user != null && _hash.ValidateMD5Hash(password, user.PasswordHash))
		//	{
		//		return user.ApiToken;
		//	}

		//	return null;
		//}
	}
}

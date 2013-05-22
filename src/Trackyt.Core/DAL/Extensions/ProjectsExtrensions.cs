using System.Linq;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Extensions
{
	public static class ProjectsExtensions
    {       
		public static Project WithEmail(this IQueryable<Project> projects, string name)
		{
			return projects.Where(u => u.Name == name).SingleOrDefault();
		}
    }
}

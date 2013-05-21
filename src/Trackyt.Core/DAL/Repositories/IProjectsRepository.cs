namespace Trackyt.Core.DAL.Repositories.Impl
{
	using System.Linq;

	using Trackyt.Core.DAL.DataModel;

	public interface IProjectsRepository {
		IQueryable<Project> Projects { get; }

		void Save(Project project);

		void Delete(Project project);
	}
}
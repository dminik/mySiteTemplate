namespace Trackyt.Core.DAL.Repositories
{
	using System.Linq;

	using Trackyt.Core.DAL.DataModel;

	public interface ISpentTimesRepository
	{
		IQueryable<SpentTime> SpentTimes { get; }

		void Save(SpentTime spentTime);

		void Delete(SpentTime spentTime);
	}
}
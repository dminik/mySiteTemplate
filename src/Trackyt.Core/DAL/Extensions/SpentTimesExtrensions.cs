using System.Linq;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Extensions
{
	public static class SpentTimesExtensions
    {
		public static SpentTime WithId(this IQueryable<SpentTime> spentTimes, int id)
		{
			return spentTimes.Where(u => u.Id == id).SingleOrDefault();
		}
    }
}

using System.Linq;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class SpentTimesRepository : ISpentTimesRepository
    {
        private TrackytDataContext _context;

        public SpentTimesRepository()
            : this(new TrackytDataContext())
        {

        }

        //used in unit tests
		public SpentTimesRepository(TrackytDataContext context)
        {
            _context = context;
        }

        #region IUsersRepository Members

		public IQueryable<SpentTime> SpentTimes
        {
            get
            {
				return _context.SpentTimes.AsQueryable();
            }
        }

		public void Save(SpentTime spentTime)
        {
			if (spentTime.Id == 0)
            {				
				_context.SpentTimes.Add(spentTime);
            }

            _context.SaveChanges();
        }

		public void Delete(SpentTime spentTime)
        {
			_context.SpentTimes.Remove(spentTime);
            _context.SaveChanges();
        }

        #endregion
    }
}

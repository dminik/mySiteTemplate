using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using System.Data.Linq;
using System.Configuration;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class ProjectsRepository : IProjectsRepository
    {
        private TrackytDataContext _context;

        public ProjectsRepository()
            : this(new TrackytDataContext())
        {

        }

        //used in unit tests
		public ProjectsRepository(TrackytDataContext context)
        {
            _context = context;
        }

        #region IUsersRepository Members

		public IQueryable<Project> Projects
        {
            get
            {
				return _context.Projects.AsQueryable();
            }
        }

		public void Save(Project project)
        {
			if (project.Id == 0)
            {
				if (Projects.WithName(project.Name) != null)
					throw new DuplicateKeyException(project);

				_context.Projects.Add(project);
            }

            _context.SaveChanges();
        }

		public void Delete(Project project)
        {
			_context.Projects.Remove(project);
            _context.SaveChanges();
        }

        #endregion
    }
}

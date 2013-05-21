using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using System.Configuration;

namespace Trackyt.Core.DAL.Repositories.Impl
{
    public class TasksRepository : ITasksRepository
    {
        private TrackytDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public TasksRepository() : this(
            new TrackytDataContext())
        {

        }

        /// <summary>
        /// Constructor used in unit tests
        /// </summary>
        /// <param name="context">Context</param>
        public TasksRepository(TrackytDataContext context)
        {
            _context = context;
        }

        public IQueryable<Task> Tasks
        {
            get
            {
                return _context.Tasks.OrderBy(t => t.Position);
            }
        }

        public void Save(Task task) 
        {
            if (task.Id == 0)
            {
                task.CreatedDate = DateTime.UtcNow;
                _context.Tasks.Add(task);
            }
			_context.SaveChanges();
        }

        public void Delete(Task task)
        {

            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }
    }
}

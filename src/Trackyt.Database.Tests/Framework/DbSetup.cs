using System;
using System.Transactions;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Tests.Framework
{
	using System.Configuration;

	using Trackyt.Core.DAL.Repositories;

	public class DbSetup : IDisposable
	{
		private TrackytDataContext _dbContext;
		private TransactionScope _transaction;
 
		public DbSetup()
		{
			Init();
		}

		public TrackytDataContext Context { get { return _dbContext; } }

		private void Init()
		{			
			_dbContext = new TrackytDataContext(ConfigurationManager.ConnectionStrings["testdb"].ConnectionString);
			RepositoriesInitializer.InitDB(_dbContext);

			_dbContext.Database.Connection.Open();
			_transaction = new TransactionScope();

			
			AddTestUser();
			AddTestProject();
		}

		private void AddTestUser()
		{
			User = new User()
			{
				Email = "exists@test.com",
				//Password = "test_pass2"
			};

			_dbContext.Users.Add(User);
			_dbContext.SaveChanges();
		}

		private void AddTestProject()
		{
			Project = new Project()
			{
				Name = "Test Project"				
			};

			_dbContext.Projects.Add(Project);
			_dbContext.SaveChanges();
		}

		public User User { get; private set; }

		public Project Project { get; private set; }

		#region IDisposable Members

		public void Dispose()
		{
			if(_transaction != null)
				_transaction.Dispose();

			_transaction = null;
		}

		#endregion
	}
}

using System;
using System.Transactions;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Tests.Framework
{
	using System.Configuration;
	using System.Data.Entity;

	using Trackyt.Core.DAL.Repositories;

	public class DbSetup : IDisposable
	{
		private TrackytDataContext _model;
		private TransactionScope _transaction;
 
		public DbSetup()
		{
			Init();
		}

		public TrackytDataContext Context { get { return _model; } }

		private void Init()
		{			
			_model = new TrackytDataContext(ConfigurationManager.ConnectionStrings["testdb"].ConnectionString);
			RepositoriesInitializer.InitDB(_model);
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

			_model.Users.Add(User);
			_model.SaveChanges();
		}

		private void AddTestProject()
		{
			Project = new Project()
			{
				Name = "Test Project"				
			};

			_model.Projects.Add(Project);
			_model.SaveChanges();
		}

		public User User { get; private set; }

		public Project Project { get; private set; }

		#region IDisposable Members

		public void Dispose()
		{
			_transaction.Dispose();
			_transaction = null;
		}

		#endregion
	}
}

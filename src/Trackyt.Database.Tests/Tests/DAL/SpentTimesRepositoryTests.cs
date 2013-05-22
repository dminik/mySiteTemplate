namespace Trackyt.Database.Tests.Tests.DAL
{
	using NUnit.Framework;

	using System.Data.Linq;

	using Trackyt.Core.DAL.DataModel;
	using Trackyt.Core.DAL.Repositories.Impl;
	using Trackyt.Core.DAL.Extensions;
	using Trackyt.Core.Tests.Framework;

	[TestFixture]
	public class SpentTimesRepositoryTests
	{
	   [Test]
		public void InsertSpentTime()
		{
			using (var fixture = new FixtureInit("http://localhost"))
			{
				//INIT
				var repository = new SpentTimesRepository(fixture.Setup.Context);
				
				//ACT
				var spentTime = new SpentTime()
				{
					Amount = 30,
					Description = "Test description 1",
					ProjectId = fixture.Setup.Project.Id
				};

				repository.Save(spentTime);

				//POST
				var actual = repository.SpentTimes.WithId(spentTime.Id);
				Assert.That(actual, Is.Not.Null);
				Assert.That(actual.Description, Is.EqualTo("Test description 1"));
				Assert.That(actual.ProjectId, Is.EqualTo(fixture.Setup.Project.Id));
			}
		}
	
		[Test]
		public void DeleteSpentTime()
		{
			using (var fixture = new FixtureInit("http://localhost"))
			{
				//INIT
				var repository = new SpentTimesRepository(fixture.Setup.Context);

				var spentTime = new SpentTime()
				{
					Amount = 30,
					Description = "Test description 1",
					ProjectId = fixture.Setup.Project.Id
				};

				repository.Save(spentTime);

				//ACT
				repository.Delete(spentTime);

				//POST
				var foundSpentTime = repository.SpentTimes.WithId(spentTime.Id);
				Assert.That(foundSpentTime, Is.Null);
			}
		}      
	}
}

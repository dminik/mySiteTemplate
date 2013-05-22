namespace Trackyt.Database.Tests.Tests.DAL
{
	using NUnit.Framework;

	using System.Data.Linq;

	using Trackyt.Core.DAL.DataModel;
	using Trackyt.Core.DAL.Repositories.Impl;
	using Trackyt.Core.DAL.Extensions;
	using Trackyt.Core.Tests.Framework;

	[TestFixture]
    public class ProjectsRepositoryTests
    {
       [Test]
        public void InsertProject()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new ProjectsRepository(fixture.Setup.Context);

                //ACT
                var project = new Project()
                {
					Name = "TestProject"                   
                };

                repository.Save(project);

                //POST
				var actual = repository.Projects.WithName("TestProject");
                Assert.That(actual, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertProjectTwice()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new ProjectsRepository(fixture.Setup.Context);

                //ACT / POST
				var project = new Project()
				{
					Name = "TestProject"
				};

				repository.Save(project);

				project = new Project()
				{
					Name = "TestProject"
				};

				repository.Save(project);
            }
        }
       
        [Test]
        public void DeleteProject()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new ProjectsRepository(fixture.Setup.Context);

                var project = new Project()
                {
					Name = "TestProject"
                };

                repository.Save(project);

                //ACT
                repository.Delete(project);

                //POST
				var foundProject = repository.Projects.WithName(project.Name);
                Assert.That(foundProject, Is.Null);
            }
        }      
    }
}

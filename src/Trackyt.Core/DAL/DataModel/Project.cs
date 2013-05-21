namespace Trackyt.Core.DAL.DataModel
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Project
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<SpentTime> SpentTimes { get; set; }	
	}
}
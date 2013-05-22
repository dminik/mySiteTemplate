namespace Trackyt.Core.DAL.DataModel
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class SpentTime
	{
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Description of spent time
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Spent time in minutes
		/// </summary>
		public uint Amount { get; set; }

		/// <summary>
		/// Project Id for which was spent time
		/// </summary>
		public int ProjectId { get; set; }

		/// <summary>
		/// Project for which was spent time
		/// </summary>
		public virtual Project Project { get; set; }
	}
}
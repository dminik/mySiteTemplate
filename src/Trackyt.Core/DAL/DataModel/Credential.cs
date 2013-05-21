namespace Trackyt.Core.DAL.DataModel
{
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	//using System.Data.Linq.Mapping;

	public class Credential
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Account is required.")]
		[Display(Name = "Account")]
		[MaxLength(50)]
		[Column("Account")]
		public string Account { get; set; }

		public string Email { get; set; }
		 
		public string Password { get; set; }
	}
}
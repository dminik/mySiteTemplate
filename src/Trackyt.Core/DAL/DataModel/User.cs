namespace Trackyt.Core.DAL.DataModel
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Data.Linq.Mapping;

	public class User
	{
		[Key]
		public int Id { get; set; }

		public string Email { get; set; }

		public bool Temp { get; set; }

		//System.Data.Linq.Binary _Timestamp;
		//[global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Timestamp", AutoSync = AutoSync.Always,
		//	DbType = "rowversion NOT NULL", CanBeNull = false, IsDbGenerated = true, IsVersion = true, UpdateCheck = UpdateCheck.Never)]
		//public System.Data.Linq.Binary Timestamp
		//{
		//	get
		//	{
		//		return _Timestamp;
		//	}

		//	set
		//	{
		//		_Timestamp = value;
		//	}
		//}

		public string PasswordHash { get; set; }

		public string ApiToken { get; set; }

		public ICollection<Task> Tasks { get; set; }	
	}
}
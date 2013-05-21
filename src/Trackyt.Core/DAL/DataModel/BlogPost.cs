namespace Trackyt.Core.DAL.DataModel
{
	using System.ComponentModel.DataAnnotations;
	using System.Data.Linq.Mapping;

	public class BlogPost
	{
		
		[Key]	
		public int Id { get; set; }

		public string Url { get; set; }

		public string Title { get; set; }

		public string Body { get; set; }

		public System.DateTime CreatedDate { get; set; }

		public string CreatedBy { get; set; }

		
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

		public string Site { get; set; }
	}
}
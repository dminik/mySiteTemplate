namespace Trackyt.Core.DAL.DataModel
{
	using System.ComponentModel.DataAnnotations;
	using System.Data.Linq.Mapping;

	public class Task
	{		
		public Task()
		{
			Position = 1;
		}

		[Key]
		public int Id{ get; set; }

		public int UserId{ get; set; }

		public System.Nullable<int> Number{ get; set; }

		public string Description{ get; set; }

		public int Status{ get; set; }

		public int ActualWork{ get; set; }

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

		public System.Nullable<System.DateTime> CreatedDate{ get; set; }

		public System.Nullable<System.DateTime> StartedDate{ get; set; }

		public System.Nullable<System.DateTime> StoppedDate{ get; set; }

		public string Notes{ get; set; }

		public System.Nullable<System.DateTime> PlannedDate{ get; set; }

		public System.Nullable<int> PlannedEffort{ get; set; }

		public int Position{ get; set; }

		public bool Done{ get; set; }

		public  virtual User User{ get; set; }
	}
}
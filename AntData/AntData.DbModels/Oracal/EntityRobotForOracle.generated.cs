using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AntData.ORM;
using AntData.ORM.Linq;
using AntData.ORM.Mapping;

namespace DbModels.Oracle
{
	/// <summary>
	/// Database       : orcl
	/// Data Source    : dbtest
	/// Server Version : 11.2.0.1.0
	/// </summary>
	public partial class Entitys : IEntity
	{
		/// <summary>
		/// �û���
		/// </summary>
		public ITable<Person> People  { get { return this.Get<Person>(); } }
		/// <summary>
		/// ѧУ��
		/// </summary>
		public ITable<School> Schools { get { return this.Get<School>(); } }

		private readonly IDataContext con;

		public ITable<T> Get<T>()
			 where T : class
		{
			return this.con.GetTable<T>();
		}

		public Entitys(IDataContext con)
		{
			this.con = con;
		}
	}

	/// <summary>
	/// �û���
	/// </summary>
	[Table(Schema="TEST", Comment="�û���", Name="PERSON")]
	public partial class Person : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("ID",                  DataType=DataType.Decimal,  Length=22, Precision=15, Scale=0, Comment="����"), PrimaryKey, NotNull]
		public long Id { get; set; } // NUMBER (15,0)

		/// <summary>
		/// ����
		/// </summary>
		[Column("NAME",                DataType=DataType.VarChar,  Length=50, Comment="����"),    Nullable]
		public string Name { get; set; } // VARCHAR2(50)

		/// <summary>
		/// ���
		/// </summary>
		[Column("AGE",                 DataType=DataType.Decimal,  Length=22, Precision=5, Scale=0, Comment="���"),    Nullable]
		public int? Age { get; set; } // NUMBER (5,0)

		/// <summary>
		/// School���
		/// </summary>
		[Column("SCHOOLID",            DataType=DataType.Decimal,  Length=22, Precision=15, Scale=0, Comment="School���"),    Nullable]
		public long? Schoolid { get; set; } // NUMBER (15,0)

		/// <summary>
		/// ������ʱ��
		/// </summary>
		[Column("DATACHANGE_LASTTIME", DataType=DataType.DateTime, Length=7, Comment="������ʱ��"), NotNull]
		public DateTime DatachangeLasttime // DATE
		{
			get { return _DatachangeLasttime; }
			set { _DatachangeLasttime = value; }
		}

		#endregion

		#region Field

		private DateTime _DatachangeLasttime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion

		#region Associations

		/// <summary>
		/// persons_school
		/// </summary>
		[Association(ThisKey="Schoolid", OtherKey="ID", CanBeNull=true, KeyName="persons_school", BackReferenceName="personsschools")]
		public School Personsschool { get; set; }

		#endregion
	}

	/// <summary>
	/// ѧУ��
	/// </summary>
	[Table(Schema="TEST", Comment="ѧУ��", Name="SCHOOL")]
	public partial class School : BaseEntity
	{
		#region Column

		/// <summary>
		/// ����
		/// </summary>
		[Column("ID",                  DataType=DataType.Decimal,  Length=22, Precision=15, Scale=0, Comment="����"), PrimaryKey, NotNull]
		public long Id { get; set; } // NUMBER (15,0)

		/// <summary>
		/// ѧУ����
		/// </summary>
		[Column("NAME",                DataType=DataType.VarChar,  Length=50, Comment="ѧУ����"),    Nullable]
		public string Name { get; set; } // VARCHAR2(50)

		/// <summary>
		/// ѧУ��ַ
		/// </summary>
		[Column("ADDRESS",             DataType=DataType.VarChar,  Length=100, Comment="ѧУ��ַ"),    Nullable]
		public string Address { get; set; } // VARCHAR2(100)

		/// <summary>
		/// ������ʱ��
		/// </summary>
		[Column("DATACHANGE_LASTTIME", DataType=DataType.DateTime, Length=7, Comment="������ʱ��"), NotNull]
		public DateTime DatachangeLasttime // DATE
		{
			get { return _DatachangeLasttime; }
			set { _DatachangeLasttime = value; }
		}

		#endregion

		#region Field

		private DateTime _DatachangeLasttime = System.Data.SqlTypes.SqlDateTime.MinValue.Value;

		#endregion

		#region Associations

		/// <summary>
		/// persons_school_BackReference
		/// </summary>
		[Association(ThisKey="Id", OtherKey="Schoolid", CanBeNull=true, IsBackReference=true)]
		public IEnumerable<Person> Personsschools { get; set; }

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Person FindByBk(this ITable<Person> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<Person> FindByBkAsync(this ITable<Person> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}

		public static School FindByBk(this ITable<School> table, long Id)
		{
			return table.FirstOrDefault(t =>
				t.Id == Id);
		}

		public static async Task<School> FindByBkAsync(this ITable<School> table, long Id)
		{
			return await table.FirstOrDefaultAsync(t =>
				t.Id == Id);
		}
	}
}
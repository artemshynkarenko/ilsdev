using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Interlogic.Trainings.Plugs.Kernel.SqlActions
{
	public class SqlClassField
	{
		public SqlClassField()
		{
		}
		
		public SqlClassField(SqlClassTable table, PropertyInfo property)
		{
			this.Table = table;
			this.Property = property;
		}
		
		private string _fieldName = null;

		public string FieldName
		{
			get {
				if (string.IsNullOrEmpty(_fieldName))
				{
					SqlTableFieldAttribute[] attributes = (SqlTableFieldAttribute[])this.Property.GetCustomAttributes(typeof(SqlTableFieldAttribute), true);
					foreach(SqlTableFieldAttribute attribute in attributes)
					{
						if (attribute.TableName == this.Table.TableName)
						{
							string fieldName = attribute.TableField;
							_fieldName = string.IsNullOrEmpty(fieldName) ? this.Property.Name : fieldName;
							break;
						}
					}
					if (string.IsNullOrEmpty(_fieldName))
					{
						_fieldName = this.Property.Name;
					}
				}
				return _fieldName;
			}
		}

		private SqlClassTable _table;

		public SqlClassTable Table
		{
			get { return _table;}
			set { _table = value;}
		}
		
		private PropertyInfo _property;

		public PropertyInfo Property
		{
			get { return _property; }
			set { _property = value; }
		}

		private bool? _isPrimaryKey = null;
		
		public bool IsPrimaryKey
		{
			get
			{
				if (_isPrimaryKey == null)
				{
					this.CheckPrimaryKey();
				}
				return _isPrimaryKey.Value;
			}
		}

		private bool? _isIdentity = null;
		public bool IsIdentity
		{
			get
			{
				if (_isIdentity == null)
				{
					this.CheckPrimaryKey();
				}
				return _isIdentity.Value;
			}
		}

		private void CheckPrimaryKey()
		{
			SqlPrimaryKeyFieldAttribute[] attributes = this.Property.GetCustomAttributes(typeof(SqlPrimaryKeyFieldAttribute), true) as SqlPrimaryKeyFieldAttribute[];
			foreach (SqlPrimaryKeyFieldAttribute attribute in attributes)
			{
				if (attribute.TableName == this.Table.TableName)
				{
					_isIdentity = attribute.IsIdentity;
					_isPrimaryKey = true;
					break;
				}
			}
			if (_isPrimaryKey == null)
			{
				_isIdentity = false;
				_isPrimaryKey = false;
			}
		}

	}
}

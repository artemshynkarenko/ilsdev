using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugFactory : DomainFactory
	{
		static internal PlugFactory GetInstance()
		{
			return new PlugFactory();
		}

		protected PlugFactory()
		{
		}


		#region Installation related

        string _createTableCommandText =
            @"CREATE TABLE [PlugIn]
            (
	            [PlugId] [int] IDENTITY(1,1) NOT NULL,
	            [PlugName] [dbo].[systemName] NOT NULL,
	            [PlugFriendlyName] [dbo].[name] NOT NULL,
	            [PlugDescription] [dbo].[description] NULL,
	            [PlugVersion] [dbo].[systemName] NULL,
	            [Active] [dbo].[active] NOT NULL,
                CONSTRAINT [PK_PlugIn] PRIMARY KEY CLUSTERED 
                (
	                [PlugId] ASC
                )
                WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]" 
            + SqlAction.CommandDelimiter +
            @"EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[PlugIn].[Active]' , @futureonly='futureonly'";

		public override void InstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			if (this.Context == null)
				throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

			RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
			createTableAction.CommandText = _createTableCommandText;
			this.ExecuteCommand(createTableAction);
		}

		public override void UpdateRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
		}

		public override void UninstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		#endregion

		#region Insert
		string _insertCommandText =
			@"INSERT INTO [PlugIn] ([PlugName],[PlugFriendlyName],[PlugDescription],[PlugVersion],[Active])
            VALUES (@PlugName,@PlugFriendlyName,@PlugDescription,@PlugVersion,@Active)";

		internal void InternalInsert(Plug plug)
		{
			Insert(plug);
		}

		protected void Insert(Plug plug)
		{
			RawSqlInsertAction insertAction = new RawSqlInsertAction();
			insertAction.CommandText = _insertCommandText;

			insertAction.AddParameter("@PlugName", plug.PlugName, DbType.String);
			insertAction.AddParameter("@PlugFriendlyName", plug.PlugFriendlyName, DbType.String);
			insertAction.AddParameter("@PlugDescription", plug.PlugDescription, DbType.String);
			insertAction.AddParameter("@PlugVersion", plug.PlugVersion, DbType.String);
			insertAction.AddParameter("@Active", plug.Active, DbType.Boolean);
            this.ExecuteCommand(insertAction);
            plug.PlugId = insertAction.InsertedIdentity;
        }

		#endregion

		#region Update
		string _updateCommandText =
			@"UPDATE [PlugIn] 
            SET [PlugName] = @PlugName,
                [PlugFriendlyName] = @PlugFriendlyName,
                [PlugDescription] = @PlugDescription,
                [PlugVersion] = @PlugVersion,
                [Active] = @Active 
            WHERE [PlugId] = @PlugId";

		internal void InternalUpdate(Plug plug)
		{
			this.Update(plug);
		}

		protected void Update(Plug plug)
		{
			RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
			updateAction.CommandText = _updateCommandText;

			updateAction.AddParameter("@PlugName", plug.PlugName, DbType.String);
			updateAction.AddParameter("@PlugFriendlyName", plug.PlugFriendlyName, DbType.String);
			updateAction.AddParameter("@PlugDescription", plug.PlugDescription, DbType.String);
			updateAction.AddParameter("@PlugVersion", plug.PlugVersion, DbType.String);
			updateAction.AddParameter("@Active", plug.Active ? 1 : 0, DbType.Int32);
			updateAction.AddParameter("@PlugId", plug.PlugId, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
		#endregion

		#region Delete
		string _deleteCommandText = @"DELETE [PlugIn] WHERE [PlugId] = @PlugId";

		internal void InternalDelete(Plug plug)
		{
			this.Delete(plug);
		}

		protected void Delete(Plug plug)
		{
			RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
			deleteAction.CommandText = _deleteCommandText;

			deleteAction.AddParameter("@PlugId", plug.PlugId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
		#endregion

		#region Loads

		string _loadAllCommandText = @"SELECT * FROM [PlugIn]";

		internal List<Plug> InternalLoadAll()
		{
			RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
			readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);
            
            List<Plug> plugList = new List<Plug>();
			IDataReader dataReader = readerAction.DataReader;
            try
            {
                bool getOrdinals = true;
                int[] ordinals = null;
                while (dataReader.Read())
                {
                    if (getOrdinals)
                    {
                        ordinals = GetPlugFieldOrdinals(dataReader);
                        getOrdinals = false;
                    }
                    Plug p = new Plug();
                    TranslateToPlug(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5]);
                    plugList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

			return plugList;
		}


		string _loadByIdCommandText = @"SELECT * FROM [PlugIn] WHERE [PlugId] = @PlugId";

		internal Plug InternalLoadByPrimaryKey(int plugId)
		{
			RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
			readerAction.CommandText = _loadByIdCommandText;

			readerAction.AddParameter("@PlugId", plugId, DbType.Int32);

            Plug plug = null;
            this.ExecuteCommand(readerAction);
            try
            {
                plug = TranslateToPlug(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return plug;
		}


		string _loadByNameCommandText = @"SELECT * FROM [PlugIn] WHERE [PlugName] = @PlugName";

		internal Plug InternalLoadByName(string plugName)
		{
			RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
			readerAction.CommandText = _loadByNameCommandText;

			readerAction.AddParameter("@PlugName", plugName, DbType.String);

            Plug plug = null;
			this.ExecuteCommand(readerAction);
            try
            {
                plug = TranslateToPlug(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }

			return plug;
		}

		
		protected int[] GetPlugFieldOrdinals(IDataReader dataReader)
		{
			int[] indexes = new int[6];
			indexes[0] = dataReader.GetOrdinal("PlugId");
			indexes[1] = dataReader.GetOrdinal("PlugName");
			indexes[2] = dataReader.GetOrdinal("PlugFriendlyName");
			indexes[3] = dataReader.GetOrdinal("PlugDescription");
			indexes[4] = dataReader.GetOrdinal("PlugVersion");
			indexes[5] = dataReader.GetOrdinal("Active");
			return indexes;
		}

		private Plug TranslateToPlug(IDataReader dataReader)
		{
			Plug plug = new Plug();
			TranslateToPlug(dataReader,plug);
			return plug;
		}
		protected void TranslateToPlug(IDataReader dataReader, Plug plug)
		{
			int[] indexes = GetPlugFieldOrdinals(dataReader);
			TranslateToPlug(dataReader, plug, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5]);
		}
		protected void TranslateToPlug(IDataReader dataReader, Plug plug, int idIndex, int nameIndex, int friendlyNameIndex, int descriptionIndex, int versionIndex, int activeIndex)
		{
			plug.PlugId = dataReader.GetInt32(idIndex);
			plug.PlugName = dataReader.GetString(nameIndex);
			plug.PlugFriendlyName = dataReader.GetString(friendlyNameIndex);
			if (!dataReader.IsDBNull(descriptionIndex))
				plug.PlugDescription = dataReader.GetString(descriptionIndex);
			plug.PlugVersion = dataReader.GetString(versionIndex);
			plug.Active = dataReader.GetBoolean(activeIndex);
		}
		#endregion

	}
}

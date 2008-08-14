using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
	public class PlugFactory:DomainFactory
	{
		public PlugFactory()
		{
			this.Inserted += new EventHandler(PlugFactory_Inserted);
		}
		
		public override void InstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			if (this.Context == null)
				throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");
			
			RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
			createTableAction.CommandText =
@"CREATE TABLE [PlugIn](
	[PlugId] [int] IDENTITY(1,1) NOT NULL,
	[PlugName] [dbo].[systemName] NOT NULL,
	[PlugFriendlyName] [dbo].[name] NOT NULL,
	[PlugDescription] [dbo].[description] NULL,
	[PlugVersion] [dbo].[systemName] NULL,
	[Active] [dbo].[active] NOT NULL,
 CONSTRAINT [PK_PlugIn] PRIMARY KEY CLUSTERED 
(
	[PlugId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[PlugIn].[Active]' , @futureonly='futureonly'";
			this.ExecuteCommand(createTableAction);
		}

		public override void UpdateRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void UninstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
		{
			throw new Exception("The method or operation is not implemented.");
		}
		private RawSqlInsertAction _insertAction = null;
		private Plug _currentPlug;
        
        string _insertCommandText = 
            @"INSERT INTO [PlugIn] ([PlugName],[PlugFriendlyName],[PlugDescription],[PlugVersion],[Active])
            VALUES (@PlugName,@PlugFriendlyName,@PlugDescription,@PlugVersion,@Active)";
        public void Insert(Plug plug)
		{
			_currentPlug = plug;
			_insertAction = new RawSqlInsertAction();
            _insertAction.CommandText = _insertCommandText;

            _insertAction.AddParameter("@PlugName", plug.PlugName, DbType.String);
            _insertAction.AddParameter("@PlugFriendlyName", plug.PlugFriendlyName, DbType.String);
            _insertAction.AddParameter("@PlugDescription", plug.PlugDescription, DbType.String);
            _insertAction.AddParameter("@PlugVersion", plug.PlugVersion, DbType.String);
            _insertAction.AddParameter("@Active", plug.Active ? 1 : 0, DbType.Int32);

            this.Insert(_insertAction);
		}

		void PlugFactory_Inserted(object sender, EventArgs e)
		{
			if (_insertAction == null)
				return;
			_currentPlug.PlugId = _insertAction.InsertedIdentity;
		}

        string _updateCommandText =
            @"UPDATE [PlugIn] 
            SET [PlugName] = @PlugName,
                [PlugFriendlyName] = @PlugFriendlyName,
                [PlugDescription] = @PlugDescription,
                [PlugVersion] = @PlugVersion,
                [Active] = @Active 
            WHERE [PlugId] = @PlugId";
		public void Update(Plug plug)
		{
			RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@PlugName", plug.PlugName, DbType.String);
            updateAction.AddParameter("@PlugFriendlyName", plug.PlugFriendlyName, DbType.String);
            updateAction.AddParameter("@PlugDescription", plug.PlugDescription, DbType.String);
            updateAction.AddParameter("@PlugVersion", plug.PlugVersion, DbType.String);
            updateAction.AddParameter("@Active", plug.Active ? 1 : 0, DbType.Int32);
            updateAction.AddParameter("@PlugId", plug.PlugId, DbType.Int32);

			this.Update(updateAction);
		}

        string _deleteCommandText = @"DELETE [PlugIn] WHERE [PlugId] = @PlugId";
		public void Delete(Plug plug)
		{
			RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@PlugId", plug.PlugId, DbType.Int32);

			this.Delete(deleteAction);
		}

        string _loadAllCommandText = @"SELECT * FROM [PlugIn]";
        public List<Plug> LoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.LoadAll();

            List<Plug> plugList = new List<Plug>();
            while (readerAction.DataReader.Read())
            {
                plugList.Add(ExtractPlug(readerAction.DataReader));
            }
            return plugList;
        }
        string _loadByPrimaryKeyCommandText = @"SELECT * FROM [PlugIn] WHERE [PlugId] = @PlugId";
        string _loadByNameCommandText = @"SELECT * FROM [PlugIn] WHERE [PlugName] = @PlugName";

        protected Plug ExtractPlug(IDataReader dataReader)
        {
            Plug plug = new Plug();
            plug.PlugId             = (int)dataReader["PlugId"];
            plug.PlugName           = (string)dataReader["PlugName"];
            plug.PlugFriendlyName   = (string)dataReader["PlugFriendlyName"];
            plug.PlugDescription    = (string)dataReader["PlugDescription"];
            plug.PlugVersion        = (string)dataReader["PlugVersion"];
            plug.Active             = (int)dataReader["Active"] != 0;
            return plug;
        }
    }
}

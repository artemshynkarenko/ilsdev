using System;
using System.Collections.Generic;
using System.Text;
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
		public void Insert(Plug plug)
		{
			_currentPlug = plug;
			_insertAction = new RawSqlInsertAction();

			_insertAction.CommandText = string.Format(CultureInfo.InvariantCulture,
@"INSERT INTO [PlugIn]
   ([PlugName]
   ,[PlugFriendlyName]
   ,[PlugDescription]
   ,[PlugVersion]
   ,[Active])
VALUES
   ('{0}'
   ,'{1}'
   ,'{2}'
   ,'{3}'
   ,{4})", plug.PlugName, plug.PlugFriendlyName,plug.PlugDescription, plug.PlugVersion, plug.Active ? "1" : "0");

			this.Insert(_insertAction);

		}

		void PlugFactory_Inserted(object sender, EventArgs e)
		{
			if (_insertAction == null)
				return;
			_currentPlug.PlugId = _insertAction.InsertedIdentity;
		}

		public void Update(Plug plug)
		{
			RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
			updateAction.CommandText = string.Format(CultureInfo.InvariantCulture,
@"UPDATE [PlugIn]
SET [PlugName] = '{0}'
  ,[PlugFriendlyName] = '{1}'
  ,[PlugDescription] = '{2}'
  ,[PlugVersion] = '{3}'
  ,[Active] = '{4}'
WHERE [PlugId] = {5}", plug.PlugName, plug.PlugFriendlyName,plug.PlugDescription, plug.PlugVersion, plug.Active ? "1": "0", plug.PlugId);

			this.Update(updateAction);
		}

		public void Delete(Plug plug)
		{
			RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
			deleteAction.CommandText = string.Format(CultureInfo.InvariantCulture,
@"DELETE [PlugIn]
WHERE [PlugId] = {0}", plug.PlugId);

			this.Delete(deleteAction);
		}
	}
}

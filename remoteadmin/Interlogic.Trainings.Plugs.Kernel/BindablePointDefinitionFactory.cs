using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    class BindablePointDefinitionFactory : DomainFactory
    {
        protected BindablePointDefinitionFactory()
        {
        }

        static internal BindablePointDefinitionFactory GetInstance()
        {
            return new BindablePointDefinitionFactory();
        }

        #region Installation related
        string _createTableCommandText =
            @"CREATE TABLE [BindablePointDefinition]
            (
	            [BindablePointDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	            [ClassDefinitionId] [int] NOT NULL,
	            [BindablePointName] [dbo].[systemName] NOT NULL,
	            [BindablePointFriendlyName] [dbo].[name] NOT NULL,
	            [BindablePointDescription] [dbo].[description] NULL,
	            [InterfaceId] [int] NULL,
	            CONSTRAINT [PK_BindablePointDefinition] PRIMARY KEY CLUSTERED 
	            (
		            [BindablePointDefinitionId] ASC
	            )
	            WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

        public override void InstallRequiredEnvironment(ISqlTransactionContext context)
        {
            if (this.Context == null)
                throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

            RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
            createTableAction.CommandText = _createTableCommandText;
            this.ExecuteCommand(createTableAction);
        }

        public override void UpdateRequiredEnvironment(ISqlTransactionContext context)
        {
        }

        public override void UninstallRequiredEnvironment(ISqlTransactionContext context)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Insert
        string _insertCommandText =
            @"INSERT INTO [BindablePointDefinition] ([ClassDefinitionId],[BindablePointName],[BindablePointFriendlyName],[BindablePointDescription],[InterfaceId])
            VALUES (@ClassDefinitionId,@BindablePointName,@BindablePointFriendlyName,@BindablePointDescription,@InterfaceId)";

        internal void InternalInsert(BindablePointDefinition bindPointDef)
        {
            Insert(bindPointDef);
        }

        protected void Insert(BindablePointDefinition bindPointDef)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@ClassDefinitionId", bindPointDef.ClassDefinitionId, DbType.Int32);
            insertAction.AddParameter("@BindablePointName", bindPointDef.BindablePointName, DbType.String);
            insertAction.AddParameter("@BindablePointFriendlyName", bindPointDef.BindablePointFriendlyName, DbType.String);
            insertAction.AddParameter("@BindablePointDescription", bindPointDef.BindablePointDescription, DbType.String);
            insertAction.AddParameter("@InterfaceId", bindPointDef.InterfaceId, DbType.Int32);
            this.ExecuteCommand(insertAction);
            bindPointDef.BindablePointDefinitionId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [BindablePointDefinition]
               SET [ClassDefinitionId] = @ClassDefinitionId,
                   [BindablePointName] = @BindablePointName,
                   [BindablePointFriendlyName] = @BindablePointFriendlyName,
                   [BindablePointDescription] = @BindablePointDescription,
                   [InterfaceId] = @InterfaceId
             WHERE [BindablePointDefinitionId] = @BindablePointDefinitionId";

        internal void InternalUpdate(BindablePointDefinition bindPointDef)
        {
            this.Update(bindPointDef);
        }

        protected void Update(BindablePointDefinition bindPointDef)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@BindablePointDefinitionId", bindPointDef.BindablePointDefinitionId, DbType.Int32);
            updateAction.AddParameter("@ClassDefinitionId", bindPointDef.ClassDefinitionId, DbType.Int32);
            updateAction.AddParameter("@BindablePointName", bindPointDef.BindablePointName, DbType.String);
            updateAction.AddParameter("@BindablePointFriendlyName", bindPointDef.BindablePointFriendlyName, DbType.String);
            updateAction.AddParameter("@BindablePointDescription", bindPointDef.BindablePointDescription, DbType.String);
            updateAction.AddParameter("@InterfaceId", bindPointDef.InterfaceId, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [BindablePointDefinition] WHERE [BindablePointDefinitionId] = @BindablePointDefinitionId";

        internal void InternalDelete(BindablePointDefinition bindPointDef)
        {
            this.Delete(bindPointDef);
        }

        protected void Delete(BindablePointDefinition bindPointDef)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@BindablePointDefinitionId", bindPointDef.BindablePointDefinitionId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [BindablePointDefinition]";

        internal List<BindablePointDefinition> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<BindablePointDefinition> bindPointDefList = new List<BindablePointDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindablePointDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    BindablePointDefinition res = new BindablePointDefinition();
                    TranslateToBindablePointDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5]);
                    bindPointDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindPointDefList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [BindablePointDefinition] WHERE [BindablePointDefinitionId] = @BindablePointDefinitionId";

        internal BindablePointDefinition InternalLoadByPrimaryKey(int bindPointDefId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@BindablePointDefinitionId", bindPointDefId, DbType.Int32);

            BindablePointDefinition bindPointDef = null;
            this.ExecuteCommand(readerAction);
            try
            {
                bindPointDef = TranslateToBindablePointDefinition(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return bindPointDef;
        }

        protected int[] GetBindablePointDefinitionFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[6];
            indexes[0] = dataReader.GetOrdinal("BindablePointDefinitionId");
            indexes[1] = dataReader.GetOrdinal("ClassDefinitionId");
            indexes[2] = dataReader.GetOrdinal("BindablePointName");
            indexes[3] = dataReader.GetOrdinal("BindablePointFriendlyName");
            indexes[4] = dataReader.GetOrdinal("BindablePointDescription");
            indexes[5] = dataReader.GetOrdinal("InterfaceId");
            return indexes;
        }

        private BindablePointDefinition TranslateToBindablePointDefinition(IDataReader dataReader)
        {
            BindablePointDefinition bindPointDef = new BindablePointDefinition();
            TranslateToBindablePointDefinition(dataReader, bindPointDef);
            return bindPointDef;
        }
        protected void TranslateToBindablePointDefinition(IDataReader dataReader, BindablePointDefinition bindPointDef)
        {
            int[] indexes = GetBindablePointDefinitionFieldOrdinals(dataReader);
            TranslateToBindablePointDefinition(dataReader, bindPointDef, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5]);
        }
        protected void TranslateToBindablePointDefinition(IDataReader dataReader, BindablePointDefinition bindPointDef, int idIndex, int classDefIdIndex, int nameIndex, int friendlyNameIndex, int descrIndex, int interfIndex)
        {
            bindPointDef.BindablePointDefinitionId = dataReader.GetInt32(idIndex);
            bindPointDef.ClassDefinitionId = dataReader.GetInt32(classDefIdIndex);
            bindPointDef.BindablePointName = dataReader.GetString(nameIndex);
            bindPointDef.BindablePointFriendlyName = dataReader.GetString(friendlyNameIndex);
            if (!dataReader.IsDBNull(descrIndex))
                bindPointDef.BindablePointDescription = dataReader.GetString(descrIndex);
            if (!dataReader.IsDBNull(interfIndex))
                bindPointDef.InterfaceId = dataReader.GetInt32(interfIndex);

        }
        #endregion
    }
}

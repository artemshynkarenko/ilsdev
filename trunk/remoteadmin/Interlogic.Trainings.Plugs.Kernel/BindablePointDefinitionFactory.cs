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
//        string _createTableCommandText =
//@"CREATE TABLE [dbo].[BindablePointDefinition](
//	[BindablePointDefinitionId] [int] IDENTITY(1,1) NOT NULL,
//	[ClassDefinitionId] [int] NOT NULL,
//	[BindablePointName] [dbo].[systemName] NOT NULL,
//	[BindablePointFriendlyName] [dbo].[name] NOT NULL,
//	[BindablePointDescription] [dbo].[description] NULL,
//	[InterfaceId] [int] NULL,
//	[ClassDefinitionName] [dbo].[systemName] NOT NULL,
// CONSTRAINT [PK_BindablePointDefinition] PRIMARY KEY CLUSTERED 
//(
//	[BindablePointDefinitionId] ASC
//)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
//) ON [PRIMARY]
//GO
//ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition] FOREIGN KEY([ClassDefinitionId])
//REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])
//GO
//ALTER TABLE [dbo].[BindablePointDefinition]  WITH CHECK ADD  CONSTRAINT [FK_BindablePointDefinition_ClassDefinition1] FOREIGN KEY([InterfaceId])
//REFERENCES [dbo].[ClassDefinition] ([ClassDefinitionId])";

        public override void InstallRequiredEnvironment()
        {
            if (this.Context == null)
                throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

            //RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
            //createTableAction.CommandText = _createTableCommandText;
            //this.ExecuteCommand(createTableAction);
        }

        public override void UpdateRequiredEnvironment()
        {
        }

        public override void UninstallRequiredEnvironment()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Insert
        string _insertCommandText =
            @"INSERT INTO [BindablePointDefinition] ([ClassDefinitionId],[BindablePointName],[BindablePointFriendlyName],[BindablePointDescription],[InterfaceId],[ClassDefinitionName])
            VALUES (@ClassDefinitionId,@BindablePointName,@BindablePointFriendlyName,@BindablePointDescription,@InterfaceId,@ClassDefinitionName)";

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
            insertAction.AddParameter("@ClassDefinitionName", bindPointDef.ClassDefinitionName, DbType.String);

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
                   [InterfaceId] = @InterfaceId,
                   [ClassDefinitionName] = @ClassDefinitionName
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
            updateAction.AddParameter("@ClassDefinitionName", bindPointDef.ClassDefinitionName, DbType.String);

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
                    TranslateToBindablePointDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
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
                readerAction.DataReader.Read();
                bindPointDef = TranslateToBindablePointDefinition(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return bindPointDef;
        }


        string _loadByNameCommandText = @"SELECT * FROM [BindablePointDefinition] WHERE [BindablePointName] = @BindablePointName";

        internal BindablePointDefinition InternalLoadByName(string bindPointName)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByNameCommandText;

            readerAction.AddParameter("@BindablePointName", bindPointName, DbType.String);

            BindablePointDefinition bindPointDef = null;
            this.ExecuteCommand(readerAction);
            try
            {
                readerAction.DataReader.Read();
                bindPointDef = TranslateToBindablePointDefinition(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return bindPointDef;
        }


        string _loadByClassDefCommandText = @"SELECT * FROM [BindablePointDefinition] WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal List<BindablePointDefinition> InternalLoadByClassDefinitionId(string classDefId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByClassDefCommandText;
            readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<BindablePointDefinition> bindPointDefList = new List<BindablePointDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindablePointDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    BindablePointDefinition res = new BindablePointDefinition();
                    TranslateToBindablePointDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
                    bindPointDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindPointDefList;
        }


        string _loadByInterfaceCommandText = @"SELECT * FROM [BindablePointDefinition] WHERE [InterfaceId] = @InterfaceId";

        internal List<BindablePointDefinition> InternalLoadByInterfaceId(string interfaceId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByInterfaceCommandText;
            readerAction.AddParameter("@InterfaceId", interfaceId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<BindablePointDefinition> bindPointDefList = new List<BindablePointDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindablePointDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    BindablePointDefinition res = new BindablePointDefinition();
                    TranslateToBindablePointDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
                    bindPointDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindPointDefList;
        }

        protected int[] GetBindablePointDefinitionFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[7];
            indexes[0] = dataReader.GetOrdinal("BindablePointDefinitionId");
            indexes[1] = dataReader.GetOrdinal("ClassDefinitionId");
            indexes[2] = dataReader.GetOrdinal("BindablePointName");
            indexes[3] = dataReader.GetOrdinal("BindablePointFriendlyName");
            indexes[4] = dataReader.GetOrdinal("BindablePointDescription");
            indexes[5] = dataReader.GetOrdinal("InterfaceId");
            indexes[6] = dataReader.GetOrdinal("ClassDefinitionName");
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
            TranslateToBindablePointDefinition(dataReader, bindPointDef, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5], indexes[6]);
        }
        protected void TranslateToBindablePointDefinition(IDataReader dataReader, BindablePointDefinition bindPointDef, int idIndex, int classDefIdIndex, int nameIndex, int friendlyNameIndex, int descrIndex, int interfIndex, int classDefNameIndex)
        {
            bindPointDef.BindablePointDefinitionId = dataReader.GetInt32(idIndex);
            bindPointDef.ClassDefinitionId = dataReader.GetInt32(classDefIdIndex);
            bindPointDef.BindablePointName = dataReader.GetString(nameIndex);
            bindPointDef.BindablePointFriendlyName = dataReader.GetString(friendlyNameIndex);
            if (!dataReader.IsDBNull(descrIndex))
                bindPointDef.BindablePointDescription = dataReader.GetString(descrIndex);
            if (!dataReader.IsDBNull(interfIndex))
                bindPointDef.InterfaceId = dataReader.GetInt32(interfIndex);
            bindPointDef.ClassDefinitionName = dataReader.GetString(classDefNameIndex);
        }
        #endregion
    }
}

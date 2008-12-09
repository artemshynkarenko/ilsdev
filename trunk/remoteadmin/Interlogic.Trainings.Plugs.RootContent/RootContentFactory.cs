using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data;

namespace Interlogic.Trainings.Plugs.RootContent
{
	class RootContentFactory: InstanceFactory
	{
        static internal RootContentFactory GetInstance()
        {
            return new RootContentFactory();
        }

        protected RootContentFactory()
        {
        }

        #region Installation related

        string _createTableCommandText =
            @"CREATE TABLE [dbo].[RootContent](
                [InstanceId] [int] NOT NULL,
                [ParentInstanceId] [int] NULL,
                [ContentFriendlyName] [dbo].[name] NOT NULL,
                [ContentDescription] [dbo].[description] NULL,
                [ContentImageSrc] [dbo].[name] NULL,
                CONSTRAINT [PK_RootContent] PRIMARY KEY CLUSTERED 
                (
                [InstanceId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]" 
            + SqlAction.CommandDelimiter +
                @"ALTER TABLE [dbo].[RootContent]  WITH CHECK ADD  CONSTRAINT [FK_RootContent_RootContent] FOREIGN KEY([ParentInstanceId])
                REFERENCES [dbo].[RootContent] ([InstanceId])"
            + SqlAction.CommandDelimiter +
                @"ALTER TABLE [dbo].[RootContent] CHECK CONSTRAINT [FK_RootContent_RootContent]";

        public override void InstallRequiredEnvironment(ISqlTransactionContext context)
        {
            if (this.Context == null)
                throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

            base.InstallRequiredEnvironment(context);
            
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
            @"INSERT INTO [RootContent] ([InstanceId],[ParentInstanceId],[ContentFriendlyName],[ContentDescription],[ContentImageSrc])
              VALUES (@InstanceId,@ParentInstanceId,@ContentFriendlyName,@ContentDescription,@ContentImageSrc)";

        internal void InternalInsert(RootContent rootCont)
        {
            Insert(rootCont);
        }

        protected void Insert(RootContent rootCont)
        {
            base.Insert(rootCont);

            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@InstanceId", rootCont.InstanceId, DbType.Int32);
            insertAction.AddParameter("@ParentInstanceId", rootCont.ParentContent.InstanceId, DbType.Int32);
            insertAction.AddParameter("@ContentFriendlyName", rootCont.ContentFriendlyName, DbType.String);
            insertAction.AddParameter("@ContentDescription", rootCont.ContentDescription, DbType.String);
            insertAction.AddParameter("@ContentImageSrc", rootCont.ContentImageSrc, DbType.String);

            this.ExecuteCommand(insertAction);
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [RootContent]
               SET [ParentInstanceId] = @ParentInstanceId,
                  ,[ContentFriendlyName] = @ContentFriendlyName,
                  ,[ContentDescription] = @ContentDescription,
                  ,[ContentImageSrc] = @ContentImageSrc
             WHERE [InstanceID] = @InstanceId";

        internal void InternalUpdate(RootContent rootCont)
        {
            this.Update(rootCont);
        }

        protected void Update(RootContent rootCont)
        {
            base.Update(rootCont);

            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@InstanceId", rootCont.InstanceId, DbType.Int32);
            updateAction.AddParameter("@ParentInstanceId", rootCont.ParentContent.InstanceId, DbType.Int32);
            updateAction.AddParameter("@ContentFriendlyName", rootCont.ContentFriendlyName, DbType.String);
            updateAction.AddParameter("@ContentDescription", rootCont.ContentDescription, DbType.String);
            updateAction.AddParameter("@ContentImageSrc", rootCont.ContentImageSrc, DbType.String);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [RootContent] WHERE [InstanceId] = @InstanceId";

        internal void InternalDelete(RootContent rootCont)
        {
            this.Delete(rootCont);
        }

        protected void Delete(RootContent rootCont)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@InstanceId", rootCont.InstanceId, DbType.Int32);

            this.ExecuteCommand(deleteAction);

            base.Delete(rootCont);
        }
        #endregion

        #region Loads

        //string _loadAllCommandText = @"SELECT * FROM [RootContent]";

        //internal List<Instance> InternalLoadAll()
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadAllCommandText;
        //    this.ExecuteCommand(readerAction);

        //    List<Instance> rootContList = new List<Instance>();
        //    IDataReader dataReader = readerAction.DataReader;
        //    try
        //    {
        //        int[] ordinals = GetRootContFieldOrdinals(dataReader);
        //        while (dataReader.Read())
        //        {
        //            RootCont p = new RootContent();
        //            TranslateToRootCont(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4]);
        //            rootContList.Add(p);
        //        }
        //    }
        //    finally
        //    {
        //        dataReader.Close();
        //    }
            
        //    throw new Exception("Not Implemented Yet!");
            
        //    return rootContList;
        //}


        //string _loadByIdCommandText = @"SELECT * FROM [RootContent] WHERE [InstanceId] = @InstanceId";

        //internal RootContent InternalLoadByPrimaryKey(int instanceId)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByIdCommandText;

        //    readerAction.AddParameter("@InstanceId", instanceId, DbType.Int32);

        //    RootContent rootCont = null;
        //    this.ExecuteCommand(readerAction);
        //    try
        //    {
        //        rootCont = TranslateToInstance(readerAction.DataReader);
        //    }
        //    finally
        //    {
        //        readerAction.DataReader.Close();
        //    }
        //    return rootCont;
        //}


        //string _loadByNameCommandText = @"SELECT * FROM [Instance] WHERE [InstanceName] = @InstanceName";

        //internal Instance InternalLoadByName(string instanceName)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByNameCommandText;

        //    readerAction.AddParameter("@InstanceName", instanceName, DbType.String);

        //    RootContent rootCont = null;
        //    this.ExecuteCommand(readerAction);
        //    try
        //    {
        //        rootCont = TranslateToInstance(readerAction.DataReader);
        //    }
        //    finally
        //    {
        //        readerAction.DataReader.Close();
        //    }

        //    return rootCont;
        //}


        //string _loadByClassDefinitionIdCommandText = @"SELECT * FROM [Instance] WHERE [ClassDefinitionId] = @ClassDefinitionId";

        //internal List<Instance> InternalLoadByClassDefinitionId(int classDefId)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByClassDefinitionIdCommandText;
        //    readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);
        //    this.ExecuteCommand(readerAction);

        //    List<Instance> instanceList = new List<Instance>();
        //    IDataReader dataReader = readerAction.DataReader;
        //    try
        //    {
        //        int[] ordinals = GetInstanceFieldOrdinals(dataReader);
        //        while (dataReader.Read())
        //        {
        //            Instance p = new Instance();
        //            TranslateToInstance(dataReader, p, ordinals[0], ordinals[1], ordinals[2]);
        //            instanceList.Add(p);
        //        }
        //    }
        //    finally
        //    {
        //        dataReader.Close();
        //    }

        //    return instanceList;
        //}

        protected int[] GetRootContFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[5];
            indexes[0] = dataReader.GetOrdinal("InstanceId");
            indexes[1] = dataReader.GetOrdinal("ParentInstanceId");
            indexes[2] = dataReader.GetOrdinal("ContentFriendlyName");
            indexes[3] = dataReader.GetOrdinal("ContentDescription");
            indexes[4] = dataReader.GetOrdinal("ContentImageSrc");
            return indexes;
        }

        private RootContent TranslateToRootCont(IDataReader dataReader)
        {
            RootContent rootCont = new RootContent();
            TranslateToRootCont(dataReader, rootCont);
            return rootCont;
        }
        protected void TranslateToRootCont(IDataReader dataReader, RootContent rootCont)
        {
            int[] indexes = GetInstanceFieldOrdinals(dataReader);
            TranslateToRootCont(dataReader, rootCont, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4]);
        }
        protected void TranslateToRootCont(IDataReader dataReader, RootContent rootCont, int idIndex, int idParIndex, int contFriendlyName, int contDescr, int contImgSrc)
        {
            rootCont.InstanceId = dataReader.GetInt32(idIndex);
            //if (!dataReader.IsDBNull(idParIndex)) rootCont.ParentContent = RootContentController.Load(idParIndex);
            rootCont.ContentFriendlyName = dataReader.GetString(contFriendlyName);
            if (!dataReader.IsDBNull(contDescr)) rootCont.ContentDescription = dataReader.GetString(contDescr);
            if (!dataReader.IsDBNull(contImgSrc)) rootCont.ContentImageSrc = dataReader.GetString(contImgSrc);
        }
        #endregion
	}
}

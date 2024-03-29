using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data;

namespace Interlogic.Trainings.Plugs.RootContent
{
	class StaticContentFactory: InstanceFactory
	{
        static internal StaticContentFactory GetInstance()
        {
            return new StaticContentFactory();
        }

        protected StaticContentFactory()
        {
        }

        #region Installation related

//        private static readonly string _createTableCommandText =
//            @"CREATE TABLE [dbo].[RootContent](
//                [InstanceId] [int] NOT NULL,
//                [ParentInstanceId] [int] NULL,
//                [ContentFriendlyName] [dbo].[name] NOT NULL,
//                [ContentDescription] [dbo].[description] NULL,
//                [ContentImageSrc] [dbo].[name] NULL,
//                CONSTRAINT [PK_RootContent] PRIMARY KEY CLUSTERED 
//                (
//                [InstanceId] ASC
//                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
//                ) ON [PRIMARY]" 
//            + SqlAction.CommandDelimiter +
//                @"ALTER TABLE [dbo].[RootContent]  WITH CHECK ADD  CONSTRAINT [FK_RootContent_RootContent] FOREIGN KEY([ParentInstanceId])
//                REFERENCES [dbo].[RootContent] ([InstanceId])"
//            + SqlAction.CommandDelimiter +
//                @"ALTER TABLE [dbo].[RootContent] CHECK CONSTRAINT [FK_RootContent_RootContent]";

        public override void InstallRequiredEnvironment()
        {
            if (this.Context == null)
                throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

            base.InstallRequiredEnvironment();
            
            //RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
            //createTableAction.CommandText = _createTableCommandText;
            //this.ExecuteCommand(createTableAction);
        }

        public override void UpdateRequiredEnvironment()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void UninstallRequiredEnvironment()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Insert
        private static readonly string _insertCommandText =
            @"INSERT INTO [RootContent] ([InstanceId],[ParentInstanceId],[ContentFriendlyName],[ContentDescription],[ContentImageSrc])
              VALUES (@InstanceId,@ParentInstanceId,@ContentFriendlyName,@ContentDescription,@ContentImageSrc)";

        internal void InternalInsert(StaticContent rootCont)
        {
            Insert(rootCont);
        }

        protected void Insert(StaticContent rootCont)
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
        private static readonly string _updateCommandText =
            @"UPDATE [RootContent]
               SET [ParentInstanceId] = @ParentInstanceId,
                  ,[ContentFriendlyName] = @ContentFriendlyName,
                  ,[ContentDescription] = @ContentDescription,
                  ,[ContentImageSrc] = @ContentImageSrc
             WHERE [InstanceID] = @InstanceId";

        internal void InternalUpdate(StaticContent rootCont)
        {
            this.Update(rootCont);
        }

        protected void Update(StaticContent rootCont)
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
        private static readonly string _deleteCommandText = @"DELETE [RootContent] WHERE [InstanceId] = @InstanceId";

        internal void InternalDelete(StaticContent rootCont)
        {
            this.Delete(rootCont);
        }

        protected void Delete(StaticContent rootCont)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@InstanceId", rootCont.InstanceId, DbType.Int32);

            this.ExecuteCommand(deleteAction);

            base.Delete(rootCont);
        }
        #endregion

        #region Loads

        private static readonly string _loadAllCommandText = @"SELECT * FROM [Instance] INNER JOIN [RootContent] ON [Instance].InstanceId = [RootContent].InstanceId";

        internal List<StaticContent> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<StaticContent> rootContList = new List<StaticContent>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetRootContFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    StaticContent p = new StaticContent();
                    TranslateToRootCont(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
                    rootContList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return rootContList;
        }


        private static readonly string _loadByIdCommandText = _loadAllCommandText + @" WHERE [InstanceId] = @InstanceId";

        internal StaticContent InternalLoadByPrimaryKey(int instanceId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@InstanceId", instanceId, DbType.Int32);

            StaticContent rootCont = null;
            this.ExecuteCommand(readerAction);
            try
            {
                readerAction.DataReader.Read();
                rootCont = TranslateToRootCont(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return rootCont;
        }


        private static readonly string _loadByInstanceNameCommandText = _loadAllCommandText + @" WHERE [InstanceName] = @InstanceName";

        internal StaticContent InternalLoadByInstanceName(string instanceName)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByInstanceNameCommandText;

            readerAction.AddParameter("@InstanceName", instanceName, DbType.String);

            StaticContent rootCont = null;
            this.ExecuteCommand(readerAction);
            try
            {
                readerAction.DataReader.Read();
                rootCont = TranslateToRootCont(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }

            return rootCont;
        }


        private static readonly string _loadByFriendlyNameCommandText = _loadAllCommandText + @" WHERE [ContentFriendlyName] = @ContentFriendlyName";

        internal StaticContent InternalLoadByFriendlyName(string instanceName)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByInstanceNameCommandText;

            readerAction.AddParameter("@ContentFriendlyName", instanceName, DbType.String);

            StaticContent rootCont = null;
            this.ExecuteCommand(readerAction);
            try
            {
                readerAction.DataReader.Read();
                rootCont = TranslateToRootCont(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }

            return rootCont;
        }


        private static readonly string _loadByClassDefinitionIdCommandText = _loadAllCommandText + @" WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal List<StaticContent> InternalLoadByClassDefinitionId(int classDefId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByClassDefinitionIdCommandText;
            readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<StaticContent> rootContList = new List<StaticContent>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetRootContFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    StaticContent p = new StaticContent();
                    TranslateToRootCont(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
                    rootContList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return rootContList;
        }


        private static readonly string _loadByParentInstanceIdCommandText = _loadAllCommandText + @" WHERE [ParentInstanceId] = @ParentInstanceId";

        internal List<StaticContent> InternalLoadByParentInstanceId(int parentId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByParentInstanceIdCommandText;
            readerAction.AddParameter("@ParentInstanceId", parentId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<StaticContent> rootContList = new List<StaticContent>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetRootContFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    StaticContent p = new StaticContent();
                    TranslateToRootCont(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
                    rootContList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return rootContList;
        }


        protected int[] GetRootContFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[4];
            indexes[0] = dataReader.GetOrdinal("ParentInstanceId");
            indexes[1] = dataReader.GetOrdinal("ContentFriendlyName");
            indexes[2] = dataReader.GetOrdinal("ContentDescription");
            indexes[3] = dataReader.GetOrdinal("ContentImageSrc");
            return indexes;
        }

        private StaticContent TranslateToRootCont(IDataReader dataReader)
        {
            StaticContent rootCont = new StaticContent();
            TranslateToRootCont(dataReader, rootCont);
            return rootCont;
        }
        protected void TranslateToRootCont(IDataReader dataReader, StaticContent rootCont)
        {
            int[] indexes = GetInstanceFieldOrdinals(dataReader);
            TranslateToRootCont(dataReader, rootCont, indexes[0], indexes[1], indexes[2], indexes[3]);
        }
        protected void TranslateToRootCont(IDataReader dataReader, StaticContent rootCont, int idParIndex, int contFriendlyName, int contDescr, int contImgSrc)
        {
            base.TranslateToInstance(dataReader, rootCont);
            //if (!dataReader.IsDBNull(idParIndex)) rootCont.ParentContent = this.InternalLoadByPrimaryKey(dataReader.GetInt32(idParIndex));
            //TODO: parentId ?
            //throw new NotImplementedException("Don't know what to do with parentId?");
            rootCont.ContentFriendlyName = dataReader.GetString(contFriendlyName);
            if (!dataReader.IsDBNull(contDescr)) rootCont.ContentDescription = dataReader.GetString(contDescr);
            if (!dataReader.IsDBNull(contImgSrc)) rootCont.ContentImageSrc = dataReader.GetString(contImgSrc);
        }
        #endregion
	}
}

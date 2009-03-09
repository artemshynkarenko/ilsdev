using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class ClassDefinitionFactory : DomainFactory
    {
        protected ClassDefinitionFactory()
        {
        }

        static internal ClassDefinitionFactory GetInstance()
        {
            return new ClassDefinitionFactory();
        }

        #region Installation related
        string _createTableCommandText =
            @"CREATE TABLE [ClassDefinition]
            (
	            [ClassDefinitionId] [int] IDENTITY(1,1) NOT NULL,
	            [ParentClassDefinitionId] [int] NULL,
	            [ClassName] [dbo].[name] NOT NULL,
	            [ClassDefinitionDescrition] [dbo].[description] NOT NULL,
	            [Active] [dbo].[active] NOT NULL,
	            [FileId] [int] NOT NULL,
	            [PlugId] [int] NOT NULL,
	            CONSTRAINT [PK_ClassDefinition] PRIMARY KEY CLUSTERED 
	            (
		            [ClassDefinitionId] ASC
	            )
	            WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]"
            + SqlAction.CommandDelimiter +
            @"EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[ClassDefinition].[Active]' , @futureonly='futureonly'";

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
            @"INSERT INTO [ClassDefinition] ([ClassName],[ClassDefinitionDescription],[Active],[FileId],[PlugId],[FileName])
                 VALUES (@ClassName,@ClassDefinitionDescription,@Active,@FileId,@PlugId,@FileName)";
        internal void InternalInsert(ClassDefinition classDef)
        {
            Insert(classDef);
        }

        protected void Insert(ClassDefinition classDef)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@ClassName", classDef.ClassName, DbType.String);
            insertAction.AddParameter("@ClassDefinitionDescription", classDef.ClassDefinitionDescription, DbType.String);
            insertAction.AddParameter("@Active", classDef.Active, DbType.Boolean);
            insertAction.AddParameter("@FileId", classDef.FileId, DbType.Int32);
            insertAction.AddParameter("@PlugId", classDef.PlugId, DbType.Int32);
            insertAction.AddParameter("@FileName", classDef.FileName, DbType.String);
            
            this.ExecuteCommand(insertAction);
            classDef.ClassDefinitionId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [ClassDefinition]
               SET [ClassName] = @ClassName,
                   [ClassDefinitionDescription] = @ClassDefinitionDescription,
                   [Active] = @Active,
                   [FileId] = @FileId,
                   [PlugId] = @PlugId,
                   [FileName] = @FileName 
             WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal void InternalUpdate(ClassDefinition classDef)
        {
            this.Update(classDef);
        }

        protected void Update(ClassDefinition classDef)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@ClassDefinitionId", classDef.ClassDefinitionId, DbType.Int32);
            updateAction.AddParameter("@ClassName", classDef.ClassName, DbType.String);
            updateAction.AddParameter("@ClassDefinitionDescription", classDef.ClassDefinitionDescription, DbType.String);
            updateAction.AddParameter("@Active", classDef.Active, DbType.Boolean);
            updateAction.AddParameter("@FileId", classDef.FileId, DbType.Int32);
            updateAction.AddParameter("@PlugId", classDef.PlugId, DbType.Int32);
            updateAction.AddParameter("@FileName", classDef.FileName, DbType.String);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [ClassDefinition] WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal void InternalDelete(ClassDefinition classDef)
        {
            this.Delete(classDef);
        }

        protected void Delete(ClassDefinition classDef)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@ClassDefinitionId", classDef.ClassDefinitionId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [ClassDefinition]";

        internal List<ClassDefinition> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<ClassDefinition> classDefList = new List<ClassDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetClassDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    ClassDefinition res = new ClassDefinition();
                    TranslateToClassDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
                    classDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return classDefList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [ClassDefinition] WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal ClassDefinition InternalLoadByPrimaryKey(int classDefId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);

            ClassDefinition classDef = null;
            this.ExecuteCommand(readerAction);
            try
            {
                classDef = TranslateToClassDefinition(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return classDef;
        }


        string _loadByNameCommandText = @"SELECT * FROM [ClassDefinition] WHERE [ClassName] = @ClassName";

        internal ClassDefinition InternalLoadByClassName(string className)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByNameCommandText;

            readerAction.AddParameter("@ClassName", className, DbType.String);

            ClassDefinition classDef = null;
            this.ExecuteCommand(readerAction);
            try
            {
                classDef = TranslateToClassDefinition(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return classDef;
        }


        string _loadByFileIdCommandText = @"SELECT * FROM [ClassDefinition] WHERE [FileId] = @FileId";

        internal List<ClassDefinition> InternalLoadByFileId(int fileId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByFileIdCommandText;
            readerAction.AddParameter("@FileId", fileId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<ClassDefinition> classDefList = new List<ClassDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetClassDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    ClassDefinition res = new ClassDefinition();
                    TranslateToClassDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
                    classDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return classDefList;
        }


        string _loadByPlugIdCommandText = @"SELECT * FROM [ClassDefinition] WHERE [PlugId] = @PlugId";

        internal List<ClassDefinition> InternalLoadByPlugIdName(int plugId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByPlugIdCommandText;
            readerAction.AddParameter("@PlugId", plugId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<ClassDefinition> classDefList = new List<ClassDefinition>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetClassDefinitionFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    ClassDefinition res = new ClassDefinition();
                    TranslateToClassDefinition(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5], ordinals[6]);
                    classDefList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return classDefList;
        }

        protected int[] GetClassDefinitionFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[7];
            indexes[0] = dataReader.GetOrdinal("ClassDefinitionId");
            indexes[1] = dataReader.GetOrdinal("ClassName");
            indexes[2] = dataReader.GetOrdinal("ClassDefinitionDescription");
            indexes[3] = dataReader.GetOrdinal("Active");
            indexes[4] = dataReader.GetOrdinal("FileId");
            indexes[5] = dataReader.GetOrdinal("PlugId");
            indexes[6] = dataReader.GetOrdinal("FileName");
            return indexes;
        }

        private ClassDefinition TranslateToClassDefinition(IDataReader dataReader)
        {
            ClassDefinition classDef = new ClassDefinition();
            TranslateToClassDefinition(dataReader, classDef);
            return classDef;
        }
        protected void TranslateToClassDefinition(IDataReader dataReader, ClassDefinition classDef)
        {
            int[] indexes = GetClassDefinitionFieldOrdinals(dataReader);
            TranslateToClassDefinition(dataReader, classDef, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5], indexes[6]);
        }
        protected void TranslateToClassDefinition(IDataReader dataReader, ClassDefinition classDef, int idIndex, int nameIndex, int descrIndex, int activeIndex, int fileIndex, int plugIndex, int fileNameIndex)
        {
            classDef.ClassDefinitionId = dataReader.GetInt32(idIndex);
            classDef.ClassName = dataReader.GetString(nameIndex);
            classDef.ClassDefinitionDescription = dataReader.GetString(descrIndex);
            classDef.Active = dataReader.GetBoolean(activeIndex);
            classDef.FileId = dataReader.GetInt32(fileIndex);
            classDef.PlugId = dataReader.GetInt32(plugIndex);
            classDef.FileName = dataReader.GetString(fileNameIndex);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class PlugFileFactory : DomainFactory
    {
        protected PlugFileFactory()
        {
        }

        static internal PlugFileFactory GetInstance()
        {
            return new PlugFileFactory();
        }

        #region Installation related
        string _createTableCommandText =
@"CREATE TABLE [dbo].[PlugFile](
	[PlugFileId] [int] IDENTITY(1,1) NOT NULL,
	[PlugFileName] [dbo].[name] NOT NULL,
	[RelativeIncomingPath] [dbo].[path] NOT NULL,
	[DestinationLocationId] [int] NOT NULL,
	[DestinationPath] [dbo].[path] NOT NULL,
	[PlugId] [int] NOT NULL,
 CONSTRAINT [PK_PlugFile] PRIMARY KEY CLUSTERED 
(
	[PlugFileId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugIn] FOREIGN KEY([PlugId])
REFERENCES [dbo].[PlugIn] ([PlugId])
GO
ALTER TABLE [dbo].[PlugFile]  WITH CHECK ADD  CONSTRAINT [FK_PlugFile_PlugLocation] FOREIGN KEY([DestinationLocationId])
REFERENCES [dbo].[PlugLocation] ([PlugLocationId])";

        public override void InstallRequiredEnvironment()
        {
            if (this.Context == null)
                throw new InvalidOperationException("You should set Context property before calling InstallRequiredEnvironment method");

            RawSqlExecuteNonQueryAction createTableAction = new RawSqlExecuteNonQueryAction();
            createTableAction.CommandText = _createTableCommandText;
            this.ExecuteCommand(createTableAction);
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
            @"INSERT INTO [PlugFile] ([PlugFileName],[RelativeIncomingPath],[DestinationLocationId],[DestinationPath],[PlugId])
                 VALUES (@PlugFileName,@RelativeIncomingPath,@DestinationLocationId,@DestinationPath,@PlugId)";

        internal void InternalInsert(PlugFile plugFile)
        {
            Insert(plugFile);
        }

        protected void Insert(PlugFile plugFile)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@PlugFileName", plugFile.PlugFileName, DbType.String);
            insertAction.AddParameter("@RelativeIncomingPath", plugFile.RelativeIncomingPath, DbType.String);
            insertAction.AddParameter("@DestinationLocationId", plugFile.DestinationLocationId, DbType.Int32);
            insertAction.AddParameter("@DestinationPath", plugFile.DestinationPath, DbType.String);
            insertAction.AddParameter("@PlugId", plugFile.PlugId, DbType.Int32);

            this.ExecuteCommand(insertAction);
            plugFile.PlugFileId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [PlugFile]
               SET [PlugFileName] = @PlugFileName,
                   [RelativeIncomingPath] = @RelativeIncomingPath,
                   [DestinationLocationId] = @DestinationLocationId,
                   [DestinationPath] = @DestinationPath,
                   [PlugId] = @PlugId,
             WHERE [PlugFileId] = @PlugFileId";

        internal void InternalUpdate(PlugFile plugFile)
        {
            this.Update(plugFile);
        }

        protected void Update(PlugFile plugFile)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@PlugFileId", plugFile.PlugFileId, DbType.Int32);
            updateAction.AddParameter("@PlugFileName", plugFile.PlugFileName, DbType.String);
            updateAction.AddParameter("@RelativeIncomingPath", plugFile.RelativeIncomingPath, DbType.String);
            updateAction.AddParameter("@DestinationLocationId", plugFile.DestinationLocationId, DbType.Int32);
            updateAction.AddParameter("@DestinationPath", plugFile.DestinationPath, DbType.String);
            updateAction.AddParameter("@PlugId", plugFile.PlugId, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [PlugFile] WHERE [PlugFileId] = @PlugFileId";

        internal void InternalDelete(PlugFile plugFile)
        {
            this.Delete(plugFile);
        }

        protected void Delete(PlugFile plugFile)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@PlugFileId", plugFile.PlugFileId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [PlugFile]";

        internal List<PlugFile> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<PlugFile> plugFileList = new List<PlugFile>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetPlugFileFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    PlugFile res = new PlugFile();
                    TranslateToPlugFile(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5]);
                    plugFileList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return plugFileList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [PlugFile] WHERE [PlugFileId] = @PlugFileId";

        internal PlugFile InternalLoadByPrimaryKey(int plugFileId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@PlugFileId", plugFileId, DbType.Int32);

            PlugFile plugFile = null;
            this.ExecuteCommand(readerAction);
            try
            {
                plugFile = TranslateToPlugFile(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return plugFile;
        }


        string _loadByPlugIdCommandText = @"SELECT * FROM [PlugFile] WHERE [PlugId] = @PlugId";

        internal List<PlugFile> InternalLoadByPlugId(int plugId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByPlugIdCommandText;
            readerAction.AddParameter("@PlugId", plugId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<PlugFile> plugFileList = new List<PlugFile>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetPlugFileFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    PlugFile res = new PlugFile();
                    TranslateToPlugFile(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5]);
                    plugFileList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return plugFileList;
        }


        string _loadByDestLocIdCommandText = @"SELECT * FROM [PlugFile] WHERE [DestinationLocationId] = @DestinationLocationId";

        internal List<PlugFile> InternalLoadByDestinationLocationId(int destLocId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByDestLocIdCommandText;
            readerAction.AddParameter("@DestinationLocationId", destLocId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<PlugFile> plugFileList = new List<PlugFile>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetPlugFileFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    PlugFile res = new PlugFile();
                    TranslateToPlugFile(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4], ordinals[5]);
                    plugFileList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return plugFileList;
        }

        protected int[] GetPlugFileFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[6];
            indexes[0] = dataReader.GetOrdinal("PlugFileId");
            indexes[1] = dataReader.GetOrdinal("PlugFileName");
            indexes[2] = dataReader.GetOrdinal("RelativeIncomingPath");
            indexes[3] = dataReader.GetOrdinal("DestinationLocationId");
            indexes[4] = dataReader.GetOrdinal("DestinationPath");
            indexes[4] = dataReader.GetOrdinal("PlugId");
            return indexes;
        }

        private PlugFile TranslateToPlugFile(IDataReader dataReader)
        {
            PlugFile plugFile = new PlugFile();
            TranslateToPlugFile(dataReader, plugFile);
            return plugFile;
        }
        protected void TranslateToPlugFile(IDataReader dataReader, PlugFile plugFile)
        {
            int[] indexes = GetPlugFileFieldOrdinals(dataReader);
            TranslateToPlugFile(dataReader, plugFile, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5]);
        }
        protected void TranslateToPlugFile(IDataReader dataReader, PlugFile plugFile, int idIndex, int nameIndex, int relInPathIndex, int destLocIndex, int destPathIndex, int plugIndex)
        {
            plugFile.PlugFileId = dataReader.GetInt32(idIndex);
            plugFile.PlugFileName = dataReader.GetString(nameIndex);
            plugFile.RelativeIncomingPath = dataReader.GetString(relInPathIndex);
            plugFile.DestinationLocationId = dataReader.GetInt32(destLocIndex);
            plugFile.DestinationPath = dataReader.GetString(destPathIndex);
            plugFile.PlugId = dataReader.GetInt32(plugIndex);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class PlugLocationFactory : DomainFactory
    {
        protected PlugLocationFactory()
        {
        }

        static internal PlugLocationFactory GetInstance()
        {
            return new PlugLocationFactory();
        }

        #region Installation related
        string _createTableCommandText =
            @"CREATE TABLE [PlugLocation]
            (
	            [PlugLocationId] [int] NOT NULL,
	            [PlugLocationName] [dbo].[systemName] NOT NULL,
	            [PlugLocationDescription] [dbo].[description] NULL,
	            [PlugLocationPath] [dbo].[path] NOT NULL,
	            [PlugId] [int] NOT NULL,
	            CONSTRAINT [PK_PlugLocation] PRIMARY KEY CLUSTERED 
	            (
		            [PlugLocationId] ASC
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
            @"INSERT INTO [PlugLocation] ([PlugLocationName],[PlugLocationDescription],[PlugLocationPath],[PlugId])
                 VALUES (@PlugLocationName,@PlugLocationDescription,@PlugLocationPath,@PlugId)";
         
        internal void InternalInsert(PlugLocation plugLoc)
        {
            Insert(plugLoc);
        }

        protected void Insert(PlugLocation plugLoc)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@PlugLocationName", plugLoc.PlugLocationName, DbType.String);
            insertAction.AddParameter("@PlugLocationDescription", plugLoc.PlugLocationDescription, DbType.String);
            insertAction.AddParameter("@PlugLocationPath", plugLoc.PlugLocationPath, DbType.String);
            insertAction.AddParameter("@PlugId", plugLoc.PlugId, DbType.Int32);

            this.ExecuteCommand(insertAction);
            plugLoc.PlugLocationId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [PlugLocation]
               SET [PlugLocationName] = @PlugLocationName,
                   [PlugLocationDescription] = @PlugLocationDescription,
                   [PlugLocationPath] = @PlugLocationPath,
                   [PlugId] = @PlugId,
             WHERE [PlugLocationId] = @PlugLocationId";

        internal void InternalUpdate(PlugLocation plugLoc)
        {
            this.Update(plugLoc);
        }

        protected void Update(PlugLocation plugLoc)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@PlugLocationId", plugLoc.PlugLocationId, DbType.Int32);
            updateAction.AddParameter("@PlugLocationName", plugLoc.PlugLocationName, DbType.String);
            updateAction.AddParameter("@PlugLocationDescription", plugLoc.PlugLocationDescription, DbType.String);
            updateAction.AddParameter("@PlugLocationPath", plugLoc.PlugLocationPath, DbType.String);
            updateAction.AddParameter("@PlugId", plugLoc.PlugId, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [PlugLocation] WHERE [PlugLocationId] = @PlugLocationId";

        internal void InternalDelete(PlugLocation plugLoc)
        {
            this.Delete(plugLoc);
        }

        protected void Delete(PlugLocation plugLoc)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@PlugLocationId", plugLoc.PlugLocationId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [PlugLocation]";

        internal List<PlugLocation> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<PlugLocation> plugLocList = new List<PlugLocation>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetPlugLocationFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    PlugLocation res = new PlugLocation();
                    TranslateToPlugLocation(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4]);
                    plugLocList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return plugLocList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [PlugLocation] WHERE [PlugLocationId] = @PlugLocationId";

        internal PlugLocation InternalLoadByPrimaryKey(int plugLocId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@PlugLocationId", plugLocId, DbType.Int32);

            PlugLocation plugLoc = null;
            this.ExecuteCommand(readerAction);
            try
            {
                plugLoc = TranslateToPlugLocation(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return plugLoc;
        }

        string _loadByNameCommandText = @"SELECT * FROM [PlugLocation] WHERE [PlugLocationName] = @PlugLocationName";

        internal PlugLocation InternalLoadByPrimaryKey(string plugLocName)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByNameCommandText;

            readerAction.AddParameter("@PlugLocationName", plugLocName, DbType.String);

            PlugLocation plugLoc = null;
            this.ExecuteCommand(readerAction);
            try
            {
                plugLoc = TranslateToPlugLocation(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return plugLoc;
        }


        string _loadByPlugIdCommandText = @"SELECT * FROM [PlugLocation] WHERE [PlugId] = @PlugId";

        internal List<PlugLocation> InternalLoadByPlugId(int plugId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByPlugIdCommandText;
            readerAction.AddParameter("@PlugId", plugId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<PlugLocation> plugLocList = new List<PlugLocation>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetPlugLocationFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    PlugLocation res = new PlugLocation();
                    TranslateToPlugLocation(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3], ordinals[4]);
                    plugLocList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return plugLocList;
        }


        protected int[] GetPlugLocationFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[5];
            indexes[0] = dataReader.GetOrdinal("PlugLocationId");
            indexes[1] = dataReader.GetOrdinal("PlugLocationName");
            indexes[2] = dataReader.GetOrdinal("PlugLocationDescription");
            indexes[3] = dataReader.GetOrdinal("PlugLocationPath");
            indexes[4] = dataReader.GetOrdinal("PlugId");
            return indexes;
        }

        private PlugLocation TranslateToPlugLocation(IDataReader dataReader)
        {
            PlugLocation plugLoc = new PlugLocation();
            TranslateToPlugLocation(dataReader, plugLoc);
            return plugLoc;
        }
        protected void TranslateToPlugLocation(IDataReader dataReader, PlugLocation plugLoc)
        {
            int[] indexes = GetPlugLocationFieldOrdinals(dataReader);
            TranslateToPlugLocation(dataReader, plugLoc, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4]);
        }
        protected void TranslateToPlugLocation(IDataReader dataReader, PlugLocation plugLoc, int idIndex, int nameIndex, int descrIndex, int pathIndex, int plugIndex)
        {
            plugLoc.PlugLocationId = dataReader.GetInt32(idIndex);
            plugLoc.PlugLocationName = dataReader.GetString(nameIndex);
            if (!dataReader.IsDBNull(descrIndex))
                plugLoc.PlugLocationDescription = dataReader.GetString(descrIndex);
            plugLoc.PlugLocationPath = dataReader.GetString(pathIndex);
            plugLoc.PlugId = dataReader.GetInt32(plugIndex);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    class BindablePointFactory: DomainFactory
    {
        protected BindablePointFactory()
        {
        }

        static internal BindablePointFactory GetInstance()
        {
            return new BindablePointFactory();
        }

        #region Installation related
        string _createTableCommandText =
            @"CREATE TABLE [BindablePoint]
            (
	            [BindablePointId] [int] NOT NULL,
	            [BindablePointDefinitonId] [int] NOT NULL,
	            [InstanceId] [int] NOT NULL,
	            [Active] [dbo].[active] NOT NULL,
                CONSTRAINT [PK_BindablePoint] PRIMARY KEY CLUSTERED 
                (
	                [BindablePointId] ASC
                )
                WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]" 
            + SqlAction.CommandDelimiter + 
            @"EXEC sys.sp_bindefault @defname=N'[dbo].[TRUE]', @objname=N'[dbo].[BindablePoint].[Active]' , @futureonly='futureonly'";

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
            @"INSERT INTO [BindablePoint] ([BindablePointId],[BindablePointDefinitonId],[InstanceId],[Active])
            VALUES (@BindablePointId,@BindablePointDefinitonId,@InstanceId,@Active)";

        internal void InternalInsert(BindablePoint bindPoint)
        {
            Insert(bindPoint);
        }

        protected void Insert(BindablePoint bindPoint)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@BindablePointId", bindPoint.BindablePointId, DbType.Int32);
            insertAction.AddParameter("@BindablePointDefinitonId", bindPoint.BindablePointDefinitionId, DbType.Int32);
            insertAction.AddParameter("@InstanceId", bindPoint.InstanceId, DbType.Int32);
            insertAction.AddParameter("@Active", bindPoint.Active, DbType.Boolean);
            this.ExecuteCommand(insertAction);
            bindPoint.BindablePointId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [BindablePoint]
            SET	   [BindablePointDefinitonId] = @BindablePointDefinitonId,
                   [InstanceId] = @InstanceId,
                   [Active] = @Active
            WHERE  [BindablePointId] = @BindablePointId";

        internal void InternalUpdate(BindablePoint bindPoint)
        {
            this.Update(bindPoint);
        }

        protected void Update(BindablePoint bindPoint)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@BindablePointId", bindPoint.BindablePointId, DbType.Int32);
            updateAction.AddParameter("@BindablePointDefinitonId", bindPoint.BindablePointDefinitionId, DbType.Int32);
            updateAction.AddParameter("@InstanceId", bindPoint.InstanceId, DbType.Int32);
            updateAction.AddParameter("@Active", bindPoint.Active, DbType.Boolean);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [BindablePoint] WHERE [BindablePointId] = @BindablePointId";

        internal void InternalDelete(BindablePoint bindPoint)
        {
            this.Delete(bindPoint);
        }

        protected void Delete(BindablePoint bindPoint)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@BindablePointId", bindPoint.BindablePointId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [BindablePoint]";

        internal List<BindablePoint> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<BindablePoint> bindPointList = new List<BindablePoint>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindablePointFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    BindablePoint res = new BindablePoint();
                    TranslateToBindablePoint(dataReader, res, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
                    bindPointList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindPointList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [BindablePoint] WHERE [BindablePointId] = @BindablePointId";

        internal BindablePoint InternalLoadByPrimaryKey(int bindPointId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@BindablePointId", bindPointId, DbType.Int32);

            BindablePoint bindPoint = null;
            this.ExecuteCommand(readerAction);
            try
            {
                bindPoint = TranslateToBindablePoint(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return bindPoint;
        }

        protected int[] GetBindablePointFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[4];
            indexes[0] = dataReader.GetOrdinal("BindablePointId");
            indexes[1] = dataReader.GetOrdinal("BindablePointDefinitionId");
            indexes[2] = dataReader.GetOrdinal("InstanceId");
            indexes[3] = dataReader.GetOrdinal("Active");
            return indexes;
        }

        private BindablePoint TranslateToBindablePoint(IDataReader dataReader)
        {
            BindablePoint bindPoint = new BindablePoint();
            TranslateToBindablePoint(dataReader, bindPoint);
            return bindPoint;
        }
        protected void TranslateToBindablePoint(IDataReader dataReader, BindablePoint bindPoint)
        {
            int[] indexes = GetBindablePointFieldOrdinals(dataReader);
            TranslateToBindablePoint(dataReader, bindPoint, indexes[0], indexes[1], indexes[2], indexes[3]);
        }
        protected void TranslateToBindablePoint(IDataReader dataReader, BindablePoint bindPoint, int idIndex, int defIdIndex, int instIdIndex, int activeIndex)
        {
            bindPoint.BindablePointId = dataReader.GetInt32(idIndex);
            bindPoint.BindablePointDefinitionId = dataReader.GetInt32(defIdIndex);
            bindPoint.InstanceId = dataReader.GetInt32(instIdIndex);
            bindPoint.Active = dataReader.GetBoolean(activeIndex);
        }
        #endregion
    }
}

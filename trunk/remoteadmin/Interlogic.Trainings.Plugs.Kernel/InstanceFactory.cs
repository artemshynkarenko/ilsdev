using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class InstanceFactory : DomainFactory
    {
        static internal InstanceFactory GetInstance()
        {
            return new InstanceFactory();
        }

        protected InstanceFactory()
        {
        }


        #region Installation related

        string _createTableCommandText =
            @"CREATE TABLE [dbo].[Instance](
	            [InstanceId] [int] IDENTITY(1,1) NOT NULL,
	            [ClassDefinitionId] [int] NOT NULL,
	            [InstanceName] [dbo].[systemName] NOT NULL,
             CONSTRAINT [PK_Intance] PRIMARY KEY CLUSTERED 
            (
	            [InstanceId] ASC
            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]";

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
            @"INSERT INTO [Instance] ([ClassDefinitionId], [InstanceName])
                 VALUES (@ClassDefinitionId, @InstanceName)";

        internal void InternalInsert(Instance instance)
        {
            Insert(instance);
        }

        protected void Insert(Instance instance)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@ClassDefinitionId", instance.ClassDefinitionId, DbType.Int32);
            insertAction.AddParameter("@InstanceName", instance.InstanceName, DbType.String);

            this.ExecuteCommand(insertAction);
            instance.InstanceId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [Instance] 
            SET [ClassDefinitionId] = @ClassDefinitionId,
                [InstanceName] = @InstanceName,
            WHERE [InstanceId] = @InstanceId";

        internal void InternalUpdate(Instance instance)
        {
            this.Update(instance);
        }

        protected void Update(Instance instance)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@InstanceId", instance.InstanceId, DbType.Int32);
            updateAction.AddParameter("@ClassDefinitionId", instance.ClassDefinitionId, DbType.Int32);
            updateAction.AddParameter("@InstanceName", instance.InstanceName, DbType.String);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [Instance] WHERE [InstanceId] = @InstanceId";

        internal void InternalDelete(Instance instance)
        {
            this.Delete(instance);
        }

        protected void Delete(Instance instance)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@InstanceId", instance.InstanceId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [Instance]";

        internal List<Instance> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<Instance> instanceList = new List<Instance>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetInstanceFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    Instance p = new Instance();
                    TranslateToInstance(dataReader, p, ordinals[0], ordinals[1], ordinals[2]);
                    instanceList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return instanceList;
        }


        string _loadByIdCommandText = @"SELECT * FROM [Instance] WHERE [InstanceId] = @InstanceId";

        internal Instance InternalLoadByPrimaryKey(int instanceId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@InstanceId", instanceId, DbType.Int32);

            Instance instance = null;
            this.ExecuteCommand(readerAction);
            try
            {
                instance = TranslateToInstance(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return instance;
        }


        string _loadByNameCommandText = @"SELECT * FROM [Instance] WHERE [InstanceName] = @InstanceName";

        internal Instance InternalLoadByName(string instanceName)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByNameCommandText;

            readerAction.AddParameter("@InstanceName", instanceName, DbType.String);

            Instance instance = null;
            this.ExecuteCommand(readerAction);
            try
            {
                instance = TranslateToInstance(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }

            return instance;
        }


        string _loadByClassDefinitionIdCommandText = @"SELECT * FROM [Instance] WHERE [ClassDefinitionId] = @ClassDefinitionId";

        internal List<Instance> InternalLoadByClassDefinitionId(int classDefId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByClassDefinitionIdCommandText;
            readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);
            this.ExecuteCommand(readerAction);

            List<Instance> instanceList = new List<Instance>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetInstanceFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    Instance p = new Instance();
                    TranslateToInstance(dataReader, p, ordinals[0], ordinals[1], ordinals[2]);
                    instanceList.Add(p);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return instanceList;
        }

        protected int[] GetInstanceFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[3];
            indexes[0] = dataReader.GetOrdinal("InstanceId");
            indexes[1] = dataReader.GetOrdinal("ClassDefinitionId");
            indexes[2] = dataReader.GetOrdinal("InstanceName");
            return indexes;
        }

        private Instance TranslateToInstance(IDataReader dataReader)
        {
            Instance instance = new Instance();
            TranslateToInstance(dataReader, instance);
            return instance;
        }
        protected void TranslateToInstance(IDataReader dataReader, Instance instance)
        {
            int[] indexes = GetInstanceFieldOrdinals(dataReader);
            TranslateToInstance(dataReader, instance, indexes[0], indexes[1], indexes[2]);
        }
        protected void TranslateToInstance(IDataReader dataReader, Instance instance, int idIndex, int classDefIndex, int instNameIndex)
        {
            instance.InstanceId = dataReader.GetInt32(idIndex);
            instance.ClassDefinitionId = dataReader.GetInt32(classDefIndex);
            instance.InstanceName = dataReader.GetString(instNameIndex);
        }
        #endregion

    }
}

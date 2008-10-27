using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Interlogic.Trainings.Plugs.Kernel.DomainModel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Globalization;

namespace Interlogic.Trainings.Plugs.Kernel
{
    class BindingFactory : DomainFactory
    {
        protected BindingFactory()
        {
        }

        static internal BindingFactory GetInstance()
        {
            return new BindingFactory();
        }

        #region Installation related
        string _createTableCommandText =
            @"CREATE TABLE [Binding]
            (
	            [BindingId] [int] IDENTITY(1,1) NOT NULL,
	            [BindablePointId] [int] NOT NULL,
	            [ImplementationId] [int] NOT NULL,
	            CONSTRAINT [PK_Binding] PRIMARY KEY CLUSTERED 
	            (
		            [BindingId] ASC
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
            @"INSERT INTO [Binding] ([BindablePointId],[ImplementationId])
            VALUES (@BindablePointId,@ImplementationId)";

        internal void InternalInsert(Binding binding)
        {
            Insert(binding);
        }

        protected void Insert(Binding binding)
        {
            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@ImplementationId", binding.ImplementationId, DbType.Int32);
            insertAction.AddParameter("@BindablePointId", binding.BindablePointId, DbType.Int32);
            this.ExecuteCommand(insertAction);
            binding.BindingId = insertAction.InsertedIdentity;
        }

        #endregion

        #region Update
        string _updateCommandText =
            @"UPDATE [Binding]
               SET [BindablePointId] = @BindablePointId,
                   [ImplementationId] = @ImplementationId
             WHERE [BindingId] = @BindingId";

        internal void InternalUpdate(Binding binding)
        {
            this.Update(binding);
        }

        protected void Update(Binding binding)
        {
            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@BindingId", binding.BindingId, DbType.Int32);
            updateAction.AddParameter("@ImplementationId", binding.ImplementationId, DbType.Int32);
            updateAction.AddParameter("@BindablePointId", binding.BindablePointId, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        string _deleteCommandText = @"DELETE [Binding] WHERE [BindingId] = @BindingId";

        internal void InternalDelete(Binding binding)
        {
            this.Delete(binding);
        }

        protected void Delete(Binding binding)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@BindingId", binding.BindingId, DbType.Int32);

            this.ExecuteCommand(deleteAction);
        }
        #endregion

        #region Loads

        string _loadAllCommandText = @"SELECT * FROM [Binding]";

        internal List<Binding> InternalLoadAll()
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadAllCommandText;
            this.ExecuteCommand(readerAction);

            List<Binding> bindingList = new List<Binding>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindingFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    Binding res = new Binding();
                    TranslateToBinding(dataReader, res, ordinals[0], ordinals[1], ordinals[2]);
                    bindingList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindingList;
        }

        string _loadByIdCommandText = @"SELECT * FROM [Binding] WHERE [BindingId] = @BindingId";

        internal Binding InternalLoadByPrimaryKey(int bindingId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByIdCommandText;

            readerAction.AddParameter("@BindingId", bindingId, DbType.Int32);

            Binding binding = null;
            this.ExecuteCommand(readerAction);
            try
            {
                binding = TranslateToBinding(readerAction.DataReader);
            }
            finally
            {
                readerAction.DataReader.Close();
            }
            return binding;
        }

        string _loadByBindablePointIdCommandText = @"SELECT * FROM [Binding] WHERE [BindingId] = @BindingId";

        public List<Binding> LoadByBindablePointId(int bindablePointId)
        {
            RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
            readerAction.CommandText = _loadByBindablePointIdCommandText;
            this.ExecuteCommand(readerAction);

            List<Binding> bindingList = new List<Binding>();
            IDataReader dataReader = readerAction.DataReader;
            try
            {
                int[] ordinals = GetBindingFieldOrdinals(dataReader);
                while (dataReader.Read())
                {
                    Binding res = new Binding();
                    TranslateToBinding(dataReader, res, ordinals[0], ordinals[1], ordinals[2]);
                    bindingList.Add(res);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return bindingList;
        }

        protected int[] GetBindingFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[5];
            indexes[0] = dataReader.GetOrdinal("BindingId");
            indexes[1] = dataReader.GetOrdinal("ImplementationId");
            indexes[2] = dataReader.GetOrdinal("BindablePointId");
            return indexes;
        }

        private Binding TranslateToBinding(IDataReader dataReader)
        {
            Binding binding = new Binding();
            TranslateToBinding(dataReader, binding);
            return binding;
        }
        protected void TranslateToBinding(IDataReader dataReader, Binding binding)
        {
            int[] indexes = GetBindingFieldOrdinals(dataReader);
            TranslateToBinding(dataReader, binding, indexes[0], indexes[1], indexes[2]);
        }
        protected void TranslateToBinding(IDataReader dataReader, Binding binding, int idIndex, int implIdIndex, int bindPointIdIndex)
        {
            binding.BindingId = dataReader.GetInt32(idIndex);
            binding.ImplementationId = dataReader.GetInt32(implIdIndex);
            binding.BindablePointId = dataReader.GetInt32(bindPointIdIndex);
        }
        #endregion
    }
}

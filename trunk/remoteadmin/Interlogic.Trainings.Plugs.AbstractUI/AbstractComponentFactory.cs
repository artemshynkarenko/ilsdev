using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel;
using Interlogic.Trainings.Plugs.Kernel.SqlActions;
using System.Data;

namespace Interlogic.Trainings.Plugs.AbstractUI
{
    public abstract class AbstractComponentFactory : InstanceFactory
    {
        #region Installation related

        private static readonly string _createTableCommandText =
            @"CREATE TABLE [dbo].[AbstractComponent](
                [InstanceId] [int] NOT NULL,
                [ParentComponentId] [int] NULL,
                [TopVal] [int] NOT NULL,
                [TopUnit] [dbo].[measureUnit] NOT NULL,
                [LeftVal] [int] NOT NULL,
                [LeftUnit] [dbo].[measureUnit] NOT NULL,
                [HeightVal] [int] NOT NULL,
                [HeightUnit] [dbo].[measureUnit] NOT NULL,
                [WidthVal] [int] NOT NULL,
                [WidthUnit] [dbo].[measureUnit] NOT NULL,
                [PositioningMethod] [dbo].[positioningType] NULL,
             CONSTRAINT [PK_AbstractComponent] PRIMARY KEY CLUSTERED 
            (
                [InstanceId] ASC
            )WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
            ) ON [PRIMARY]
            GO
            ALTER TABLE [dbo].[AbstractComponent]  WITH CHECK ADD  CONSTRAINT [FK_AbstractComponent_AbstractComponent] FOREIGN KEY([ParentComponentId])
            REFERENCES [dbo].[AbstractComponent] ([InstanceId])
            GO
            ALTER TABLE [dbo].[AbstractComponent]  WITH CHECK ADD  CONSTRAINT [FK_AbstractComponent_Instance] FOREIGN KEY([InstanceId])
            REFERENCES [dbo].[Instance] ([InstanceId])";
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
            throw new Exception("The method or operation is not implemented.");
        }

        public override void UninstallRequiredEnvironment(Interlogic.Trainings.Plugs.Kernel.SqlActions.ISqlTransactionContext context)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        #endregion

        #region Insert
        private static readonly string _insertCommandText =
            @"INSERT INTO [AbstractComponent] ([InstanceId],[ParentComponentId],[TopVal],[TopUnit],[LeftVal],[LeftUnit],[HeightVal],[HeightUnit],[WidthVal],[WidthUnit],[PositioningMethod])
                    VALUES (@InstanceId,@ParentComponentId,@TopVal,@TopUnit,@LeftVal,@LeftUnit,@HeightVal,@HeightUnit,@WidthVal,@WidthUnit,@PositioningMethod)";

        internal void InternalInsert(AbstractComponent abstrComp)
        {
            Insert(abstrComp);
        }

        protected void Insert(AbstractComponent abstrComp)
        {
            base.Insert(abstrComp);

            RawSqlInsertAction insertAction = new RawSqlInsertAction();
            insertAction.CommandText = _insertCommandText;

            insertAction.AddParameter("@InstanceId", abstrComp.InstanceId, DbType.Int32);
            throw new NotImplementedException("What to do with ParentComponentId?");
            //if (abstrComp.ParentComponent != null)
            //    insertAction.AddParameter("@ParentComponentId", abstrComp.ParentComponent.InstanceId, DbType.Int32);
            //else
            //    insertAction.AddParameter("@ParentComponentId", null, DbType.Int32);
            insertAction.AddParameter("@TopVal", abstrComp.Top.Value, DbType.Int32);
            insertAction.AddParameter("@TopUnit", (int)abstrComp.Top.Unit, DbType.Int32);
            insertAction.AddParameter("@LeftVal", abstrComp.Left.Value, DbType.Int32);
            insertAction.AddParameter("@LeftUnit", (int)abstrComp.Left.Unit, DbType.Int32);
            insertAction.AddParameter("@HeightVal", abstrComp.Height.Value, DbType.Int32);
            insertAction.AddParameter("@HeightUnit", (int)abstrComp.Height.Unit, DbType.Int32);
            insertAction.AddParameter("@WidthVal", abstrComp.Width.Value, DbType.Int32);
            insertAction.AddParameter("@WidthUnit", (int)abstrComp.Width.Unit, DbType.Int32);

            this.ExecuteCommand(insertAction);
        }

        #endregion

        #region Update
        private static readonly string _updateCommandText =
            @"UPDATE [AbstractComponent]
               SET [ParentComponentId] = @ParentComponentId,
                  ,[TopVal] = @TopVal,
                  ,[TopUnit] = @TopUnit,
                  ,[LeftVal] = @LeftVal,
                  ,[LeftUnit] = @LeftUnit,
                  ,[HeightVal] = @HeightVal,
                  ,[HeightUnit] = @HeightUnit,
                  ,[WidthVal] = @WidthVal,
                  ,[WidthUnit] = @WidthUnit,
                  ,[PositioningMethod] = @PositioningMethod
             WHERE [InstanceId] = @InstanceId";

        internal void InternalUpdate(AbstractComponent abstrComp)
        {
            this.Update(abstrComp);
        }

        protected void Update(AbstractComponent abstrComp)
        {
            base.Update(abstrComp);

            RawSqlExecuteNonQueryAction updateAction = new RawSqlExecuteNonQueryAction();
            updateAction.CommandText = _updateCommandText;

            updateAction.AddParameter("@InstanceId", abstrComp.InstanceId, DbType.Int32);
            throw new NotImplementedException("What to do with InstanceId?");
            //if (abstrComp.ParentComponent != null)
            //    updateAction.AddParameter("@ParentComponentId", abstrComp.ParentComponent.InstanceId, DbType.Int32);
            //else
            //    updateAction.AddParameter("@ParentComponentId", null, DbType.Int32);
            updateAction.AddParameter("@TopVal", abstrComp.Top.Value, DbType.Int32);
            updateAction.AddParameter("@TopUnit", (int)abstrComp.Top.Unit, DbType.Int32);
            updateAction.AddParameter("@LeftVal", abstrComp.Left.Value, DbType.Int32);
            updateAction.AddParameter("@LeftUnit", (int)abstrComp.Left.Unit, DbType.Int32);
            updateAction.AddParameter("@HeightVal", abstrComp.Height.Value, DbType.Int32);
            updateAction.AddParameter("@HeightUnit", (int)abstrComp.Height.Unit, DbType.Int32);
            updateAction.AddParameter("@WidthVal", abstrComp.Width.Value, DbType.Int32);
            updateAction.AddParameter("@WidthUnit", (int)abstrComp.Width.Unit, DbType.Int32);

            this.ExecuteCommand(updateAction);
        }
        #endregion

        #region Delete
        private static readonly string _deleteCommandText = @"DELETE [AbstractComponent] WHERE [InstanceId] = @InstanceId";

        internal void InternalDelete(AbstractComponent abstrComp)
        {
            this.Delete(abstrComp);
        }

        protected void Delete(AbstractComponent abstrComp)
        {
            RawSqlExecuteNonQueryAction deleteAction = new RawSqlExecuteNonQueryAction();
            deleteAction.CommandText = _deleteCommandText;

            deleteAction.AddParameter("@InstanceId", abstrComp.InstanceId, DbType.Int32);

            this.ExecuteCommand(deleteAction);

            base.Delete(abstrComp);
        }
        #endregion

        #region Loads

        //private static readonly string _loadAllCommandText = @"SELECT * FROM [Instance] INNER JOIN [AbstractComponent] ON [Instance].InstanceId = [AbstractComponent].InstanceId";

        //internal List<AbstractComponent> InternalLoadAll()
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadAllCommandText;
        //    this.ExecuteCommand(readerAction);

        //    List<AbstractComponent> abstrCompList = new List<AbstractComponent>();
        //    IDataReader dataReader = readerAction.DataReader;
        //    try
        //    {
        //        int[] ordinals = GetAbstractComponentFieldOrdinals(dataReader);
        //        while (dataReader.Read())
        //        {
        //            AbstractComponent p = new AbstractComponent();
        //            TranslateToAbstractComponent(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
        //            abstrCompList.Add(p);
        //        }
        //    }
        //    finally
        //    {
        //        dataReader.Close();
        //    }

        //    return abstrCompList;
        //}


        //private static readonly string _loadByIdCommandText = _loadAllCommandText + @"WHERE [InstanceId] = @InstanceId";

        //internal AbstractComponent InternalLoadByPrimaryKey(int instanceId)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByIdCommandText;

        //    readerAction.AddParameter("@InstanceId", instanceId, DbType.Int32);

        //    AbstractComponent abstrComp = null;
        //    this.ExecuteCommand(readerAction);
        //    try
        //    {
        //        abstrComp = TranslateToabstrComp(readerAction.DataReader);
        //    }
        //    finally
        //    {
        //        readerAction.DataReader.Close();
        //    }
        //    return abstrComp;
        //}


        //private static readonly string _loadByInstanceNameCommandText = _loadAllCommandText + @" WHERE [InstanceName] = @InstanceName";

        //internal AbstractComponent InternalLoadByInstanceName(string instanceName)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByInstanceNameCommandText;

        //    readerAction.AddParameter("@InstanceName", instanceName, DbType.String);

        //    AbstractComponent abstrComp = null;
        //    this.ExecuteCommand(readerAction);
        //    try
        //    {
        //        abstrComp = TranslateToabstrComp(readerAction.DataReader);
        //    }
        //    finally
        //    {
        //        readerAction.DataReader.Close();
        //    }

        //    return abstrComp;
        //}


        //private static readonly string _loadByFriendlyNameCommandText = _loadAllCommandText + @" WHERE [ContentFriendlyName] = @ContentFriendlyName";

        //internal AbstractComponent InternalLoadByFriendlyName(string instanceName)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByInstanceNameCommandText;

        //    readerAction.AddParameter("@ContentFriendlyName", instanceName, DbType.String);

        //    AbstractComponent abstrComp = null;
        //    this.ExecuteCommand(readerAction);
        //    try
        //    {
        //        abstrComp = TranslateToabstrComp(readerAction.DataReader);
        //    }
        //    finally
        //    {
        //        readerAction.DataReader.Close();
        //    }

        //    return abstrComp;
        //}


        //private static readonly string _loadByClassDefinitionIdCommandText = _loadAllCommandText + @" WHERE [ClassDefinitionId] = @ClassDefinitionId";

        //internal List<AbstractComponent> InternalLoadByClassDefinitionId(int classDefId)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByClassDefinitionIdCommandText;
        //    readerAction.AddParameter("@ClassDefinitionId", classDefId, DbType.Int32);
        //    this.ExecuteCommand(readerAction);

        //    List<AbstractComponent> abstrCompList = new List<AbstractComponent>();
        //    IDataReader dataReader = readerAction.DataReader;
        //    try
        //    {
        //        int[] ordinals = GetabstrCompFieldOrdinals(dataReader);
        //        while (dataReader.Read())
        //        {
        //            AbstractComponent p = new AbstractComponent();
        //            TranslateToabstrComp(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
        //            abstrCompList.Add(p);
        //        }
        //    }
        //    finally
        //    {
        //        dataReader.Close();
        //    }

        //    return abstrCompList;
        //}


        //private static readonly string _loadByParentInstanceIdCommandText = _loadAllCommandText + @" WHERE [ParentInstanceId] = @ParentInstanceId";

        //internal List<AbstractComponent> InternalLoadByParentInstanceId(int parentId)
        //{
        //    RawSqlExecuteReaderAction readerAction = new RawSqlExecuteReaderAction();
        //    readerAction.CommandText = _loadByParentInstanceIdCommandText;
        //    readerAction.AddParameter("@ParentInstanceId", parentId, DbType.Int32);
        //    this.ExecuteCommand(readerAction);

        //    List<AbstractComponent> abstrCompList = new List<AbstractComponent>();
        //    IDataReader dataReader = readerAction.DataReader;
        //    try
        //    {
        //        int[] ordinals = GetabstrCompFieldOrdinals(dataReader);
        //        while (dataReader.Read())
        //        {
        //            AbstractComponent p = new AbstractComponent();
        //            TranslateToabstrComp(dataReader, p, ordinals[0], ordinals[1], ordinals[2], ordinals[3]);
        //            abstrCompList.Add(p);
        //        }
        //    }
        //    finally
        //    {
        //        dataReader.Close();
        //    }

        //    return abstrCompList;
        //}


        protected int[] GetAbstractComponentFieldOrdinals(IDataReader dataReader)
        {
            int[] indexes = new int[10];
            indexes[0] = dataReader.GetOrdinal("ParentComponentId");
            indexes[1] = dataReader.GetOrdinal("TopVal");
            indexes[2] = dataReader.GetOrdinal("TopUnit");
            indexes[3] = dataReader.GetOrdinal("LeftVal");
            indexes[4] = dataReader.GetOrdinal("LeftUnit");
            indexes[5] = dataReader.GetOrdinal("HeightVal");
            indexes[6] = dataReader.GetOrdinal("HeightUnit");
            indexes[7] = dataReader.GetOrdinal("WidthVal");
            indexes[8] = dataReader.GetOrdinal("WidthUnit");
            indexes[9] = dataReader.GetOrdinal("PositioningMethod");
            return indexes;
        }

        //private AbstractComponent TranslateToAbstractComponent(IDataReader dataReader)
        //{
        //    AbstractComponent abstrComp = new AbstractComponent();
        //    TranslateToabstrComp(dataReader, abstrComp);
        //    return abstrComp;
        //}

        protected void TranslateToAbstractComponent(IDataReader dataReader, AbstractComponent abstrComp)
        {
            int[] indexes = GetInstanceFieldOrdinals(dataReader);
            TranslateToAbstractComponent(dataReader, abstrComp, indexes[0], indexes[1], indexes[2], indexes[3], indexes[4], indexes[5], indexes[6], indexes[7], indexes[8], indexes[9]);
        }
        protected void TranslateToAbstractComponent(IDataReader dataReader, AbstractComponent abstrComp, 
            int idParIndex, int topValInd, int topUnitInd, int leftValInd, int leftUnitInd, int heightValInd, int heightUnitInd, 
            int widthValInd, int widthUnitInd, int posMethodInd)
        {
            base.TranslateToInstance(dataReader, abstrComp);
            throw new NotImplementedException("What to do here with parent?");
            //if (!dataReader.IsDBNull(idParIndex)) abstrComp.ParentContent = AbstractComponentController.Load(idParIndex);

            int tVal;
            ComponentMeasurementUnit tUnit;

            tVal = dataReader.GetInt32(topValInd);
            tUnit = (ComponentMeasurementUnit)dataReader.GetInt32(topUnitInd);
            abstrComp.Top = new ComponentMeasurement(tVal, tUnit);

            tVal = dataReader.GetInt32(leftValInd);
            tUnit = (ComponentMeasurementUnit)dataReader.GetInt32(leftUnitInd);
            abstrComp.Left = new ComponentMeasurement(tVal, tUnit);

            tVal = dataReader.GetInt32(heightValInd);
            tUnit = (ComponentMeasurementUnit)dataReader.GetInt32(heightUnitInd);
            abstrComp.Height = new ComponentMeasurement(tVal, tUnit);

            tVal = dataReader.GetInt32(widthValInd);
            tUnit = (ComponentMeasurementUnit)dataReader.GetInt32(widthUnitInd);
            abstrComp.Width = new ComponentMeasurement(tVal, tUnit);

            abstrComp.PositioningMethod = (ComponentPositioning)dataReader.GetInt32(posMethodInd);
        }
        #endregion
    }
}

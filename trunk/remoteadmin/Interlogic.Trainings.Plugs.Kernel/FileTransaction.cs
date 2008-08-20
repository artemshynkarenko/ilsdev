using System;
using System.Collections.Generic;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.FileActions;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class FileTransaction : ITransactionContext, IDisposable
    {
        private List<IFileAction> _actions= new List<IFileAction>();
        private bool isTransaction;

        public FileTransaction()
        {

        }
        public void Add(FileActions.IFileAction action)
        {
            if (action != null)
            {
                action.TransactionContext = this;
                action.BeginTransaction();
                _actions.Add(action);
                
            }
            else 
                throw new NullReferenceException();

        }

        #region ITransactionContext Members

        bool ITransactionContext.ExecutingInTransaction
        {
            get { return isTransaction; }
        }

        public void BeginTransaction()
        {
         isTransaction = true;
         
        }

        public void Commit()
        {
            isTransaction = false;
            foreach (IFileAction action in _actions)
            {
                action.Commit();
                action.TransactionContext = null;
            }
        }

        public void RollBack()
        {
            isTransaction = false;
            foreach (IFileAction action in _actions)
            {
                action.RollBack();
                action.TransactionContext = null;
            }
        }

        #endregion

        public void Dispose()
        {
            if(isTransaction) RollBack();
        }
    }
}

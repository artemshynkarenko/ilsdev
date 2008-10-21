using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Interlogic.Trainings.Plugs.Kernel.FileActions;

namespace Interlogic.Trainings.Plugs.Kernel
{
    public class FileTransaction : IFileTransactionContext, IDisposable
    {

        private List<FileAction> _actions = new List<FileAction>();
        private bool _isTransaction;
        private FileLocker _locker = new FileLocker();


        public FileTransaction()
        {

        }


        #region ITransactionContext Members

        bool ITransactionContext.ExecutingInTransaction
        {
            get { return _isTransaction; }
        }

        public void BeginTransaction()
        {
            _isTransaction = true;

        }

        public void Commit()
        {
            _isTransaction = false;
            foreach (FileAction action in _actions)
            {
                action.Commit();
                action.TransactionContext = null;
            }
        }

        public void RollBack()
        {
            _isTransaction = false;
            foreach (FileAction action in _actions)
            {
                action.RollBack();
                action.TransactionContext = null;
            }
        }

        #endregion

        public void Dispose()
        {
            if (_isTransaction)
                RollBack();
        }




        #region IFileTransactionContext Members

        public void AddAction(FileAction action)
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

        public void Execute()
        {
            foreach (FileAction action in _actions)
            {
                action.Execute();
            }
        }

        public FileLocker Locker
        {
            get
            {
                return _locker;
            }

        }

        #endregion


    }
}

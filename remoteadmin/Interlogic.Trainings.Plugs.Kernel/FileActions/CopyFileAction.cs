using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class CopyFileAction : IFileAction
    {
        private ITransactionContext _transactionContext;
        private string _sourceFile = "";
        private string _destinationFile = "";
        private bool _overwrite;

        public CopyFileAction(string sourceFile, string destinationFile, bool overwrite)
        {
            _sourceFile = sourceFile;
            _destinationFile = destinationFile;
            _overwrite = overwrite;
        }

        #region ITransactionAction Members

        ITransactionContext ITransactionAction.TransactionContext
        {
            get { return _transactionContext; }
            set { _transactionContext = value; }
        }

        #endregion

        #region IAction Members

        void IAction.Execute()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region ITransactionContext Members

        public bool ExecutingInTransaction
        {
            get { return true; }
        }

        public void BeginTransaction()
        {
            // TODO: write normal checking 
            if (!File.Exists(_sourceFile)) 
                throw new FileNotFoundException(_sourceFile);
            UserFileAccessRightsChecker sourceChecker = new UserFileAccessRightsChecker(_sourceFile);
            if (!sourceChecker.canRead())
                throw new AccessDeniedException(_sourceFile );
            UserFileAccessRightsChecker destChecker =
                new UserFileAccessRightsChecker(Path.GetDirectoryName(_destinationFile));
            if (!destChecker.canCreateFiles())
                throw new AccessDeniedException(_destinationFile); 
            
                if (File.Exists(_destinationFile))
                {
                    if (_overwrite)
                    {
                        UserFileAccessRightsChecker destFileChecker = new UserFileAccessRightsChecker(_destinationFile);
                        if (!destFileChecker.canModify())
                            throw new AccessDeniedException(_destinationFile);
                    }
                    else
                    {
                        throw new FileAlreadyExistException(_destinationFile);
                    }

                }
        }

        public void Commit()
        {
            // copy files
            File.Copy(_sourceFile, _destinationFile, _overwrite);
        }

        public void RollBack()
        {
            // do nothing because BeginTransaction just check permissions
        }

        #endregion
    }
}
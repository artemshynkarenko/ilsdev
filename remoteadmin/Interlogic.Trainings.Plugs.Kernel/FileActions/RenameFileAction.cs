using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class RenameFileAction : IFileAction
    {
        private ITransactionContext _transactionContext;
        private string _sourceFile = "";
        private string _newFile = "";
        public RenameFileAction(string sourceFileName, string newFileName)
        {
            _sourceFile = sourceFileName;
            _newFile = Path.GetDirectoryName(_sourceFile) + newFileName;
        }
        #region ITransactionAction Members
        ITransactionContext ITransactionAction.TransactionContext
        {
            get
            {
                return _transactionContext;
            }
            set
            {
                _transactionContext = value;
            }
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

            if (File.Exists(_newFile))
                throw new FileAlreadyExistException(_newFile);

            UserFileAccessRightsChecker sourceFileChecker = new UserFileAccessRightsChecker(_sourceFile);

            if (!sourceFileChecker.canDelete())
                throw new AccessDeniedException(_sourceFile);

            UserFileAccessRightsChecker sourceDirChecker =
                new UserFileAccessRightsChecker(Path.GetDirectoryName(_sourceFile));
            if (!sourceDirChecker.canCreateFiles())
                throw new AccessDeniedException(_sourceFile);
        }

        public void Commit()
        {
            // copy files
            
            File.Move( _sourceFile, _newFile);

        }

        public void RollBack()
        {
            // do nothing because BeginTransaction just check permissions
        }

        #endregion
    }
}

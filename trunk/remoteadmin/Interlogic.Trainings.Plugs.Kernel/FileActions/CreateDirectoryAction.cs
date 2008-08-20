using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class CreateDirectoryAction : IFileAction
    {
        private ITransactionContext _transactionContext;
        private string _directoryPath = "";
        
        public CreateDirectoryAction(string directoryName)
        {
            _directoryPath = directoryName;
            
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
            if (File.Exists(_directoryPath) || Directory.Exists(_directoryPath))
                throw new FileAlreadyExistException(_directoryPath);

            string rootDirectoryPath="";
            if( _directoryPath.Contains(Path.DirectorySeparatorChar.ToString()))
            {
                rootDirectoryPath = _directoryPath.Substring(0, _directoryPath.LastIndexOf(Path.DirectorySeparatorChar));
            }
            
            UserFileAccessRightsChecker directoryChecker = new UserFileAccessRightsChecker(rootDirectoryPath );

            if (!directoryChecker.canCreateDirectories() )
                throw new AccessDeniedException(_directoryPath);
        }

        public void Commit()
        {
            // copy files

            Directory.CreateDirectory(_directoryPath);

        }

        public void RollBack()
        {
            // do nothing because BeginTransaction just check permissions
        }

        #endregion
    }
}

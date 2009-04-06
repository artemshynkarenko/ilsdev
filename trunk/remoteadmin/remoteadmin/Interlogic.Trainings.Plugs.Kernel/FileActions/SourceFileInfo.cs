using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class SourceFileInfo : IFileActionInfo
    {

        #region IFileActionInfo Members
        protected  string _sourceFileName;

        public SourceFileInfo(string sourceFileName)
        {
            _sourceFileName = sourceFileName;
        }

        public string SourceFileName
        {
            get { return _sourceFileName; }
            set { _sourceFileName = value; }
        }
        protected virtual void Lock(FileLocker locker)
        {
            locker.LockFile(_sourceFileName);
        }
        protected virtual void Unlock(FileLocker locker)
        {
            locker.UnlockFile(_sourceFileName);
        }
        public void UnlockOnCommit(FileLocker locker)
        {
            Unlock(locker);
        }

        public void UnlockOnRollback(FileLocker locker)
        {
            Unlock(locker);
        }

        public void LockOnExecute(FileLocker locker)
        {
            Lock(locker);
        }

        #endregion
    }
}

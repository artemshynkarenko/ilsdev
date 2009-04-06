using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public interface IFileActionInfo
    {
        void UnlockOnCommit(FileLocker locker);
        void UnlockOnRollback(FileLocker locker);
        void LockOnExecute(FileLocker locker);        
    }
}

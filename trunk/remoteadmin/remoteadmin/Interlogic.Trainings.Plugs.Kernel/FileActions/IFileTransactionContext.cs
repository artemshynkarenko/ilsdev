using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public interface IFileTransactionContext:ITransactionContext
    {

        void AddAction(FileAction action);
        void Execute();
        FileLocker Locker { get;  }
    }
}

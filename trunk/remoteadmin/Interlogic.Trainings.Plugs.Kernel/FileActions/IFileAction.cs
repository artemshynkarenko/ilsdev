using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
	public interface IFileAction:ITransactionAction
	{
        IFileTransactionContext Context { get; set;}
        void BeginTransaction();
        void Commit();
        void RollBack();
	    bool IsExecuted{ get;}
	}
}

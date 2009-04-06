using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class RenameFileAction : MoveFileAction
    {
        
        public RenameFileAction(string sourceFileName, string newFileName):base(sourceFileName ,Path.GetDirectoryName(sourceFileName) + newFileName)
        {
        }      
    }
}

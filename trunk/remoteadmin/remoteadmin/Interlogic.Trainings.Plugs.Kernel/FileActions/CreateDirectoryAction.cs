using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class CreateDirectoryAction : FileAction
    {
        

        public CreateDirectoryAction(string directoryPath)
        {
            _fileActionInfo = new SourceFileInfo(directoryPath);
        }
        protected override void ExecuteAction(IFileActionInfo fileActionInfo)
        {
            SourceFileInfo info = (SourceFileInfo)fileActionInfo;
            Check(info);
            Directory.CreateDirectory(info.SourceFileName);
        }
        protected override void RollbackAction(IFileActionInfo fileActionInfo)
        {
            SourceFileInfo info = (SourceFileInfo)fileActionInfo;
            if(Directory.Exists(info.SourceFileName ))
                Directory.Delete(info.SourceFileName,true );

        }
        private static void Check(SourceFileInfo info)
        {
            // TODO: write normal checking 
            if (File.Exists(info.SourceFileName) || Directory.Exists(info.SourceFileName))
                throw new FileAlreadyExistException(info.SourceFileName);

            string rootDirectoryPath="";
            if( info.SourceFileName.Contains(Path.DirectorySeparatorChar.ToString()))
            {
                rootDirectoryPath = info.SourceFileName.Substring(0, info.SourceFileName.LastIndexOf(Path.DirectorySeparatorChar));
            }
            
            UserFileAccessRightsChecker directoryChecker = new UserFileAccessRightsChecker(rootDirectoryPath );

            if (!directoryChecker.CanCreateDirectories() )
                throw new AccessDeniedException(info.SourceFileName);
        }
  
        

        public override void BeginTransaction()
        {

        }
    }
}

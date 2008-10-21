using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class MoveFileAction : FileAction
    {
        

        public MoveFileAction(string sourceFilePath, string destFilePath)
        {
            _fileActionInfo = new SourceDestFileInfo(sourceFilePath, destFilePath);
            //_overwrite = overwrite;
        }
        protected override void ExecuteAction(IFileActionInfo fileActionInfo)
        {
            SourceDestFileInfo info = (SourceDestFileInfo)fileActionInfo;
            Check(info);
            File.Move(info.SourceFileName, info.DestinationFileName);
        }
        protected override void RollbackAction(IFileActionInfo fileActionInfo)
        {
            SourceDestFileInfo info = (SourceDestFileInfo)fileActionInfo;
            
            if (File.Exists(info.DestinationFileName))
                File.Move( info.DestinationFileName,info.SourceFileName);
            
            //Warning: Hm, what we will do if destenation file already exists

        }
        private void Check(SourceDestFileInfo info)
        {
            if (Locker.IsLocked(info.DestinationFileName))
                throw new FileIsLockedException(info.DestinationFileName);
         
            
 // TODO: write normal checking 
            if (!File.Exists(info.SourceFileName)) 
                throw new FileNotFoundException(info.SourceFileName);

            UserFileAccessRightsChecker sourceChecker = new UserFileAccessRightsChecker(info.SourceFileName );
            if(!sourceChecker.CanRead()) 
                throw new AccessDeniedException(info.SourceFileName);
            if(!sourceChecker.CanDelete())
                throw new AccessDeniedException(info.SourceFileName);
            
            UserFileAccessRightsChecker destDirChecker = new UserFileAccessRightsChecker(Path.GetDirectoryName(info.DestinationFileName ));
            if (!destDirChecker.CanCreateFiles())
                throw new AccessDeniedException(info.DestinationFileName );
            
                
               if(File.Exists(info.DestinationFileName ))
               {
                   /*if (_overwrite)
                   {
                   UserFileAccessRightsChecker destFileChecker =  new UserFileAccessRightsChecker(info.DestinationFileName );
                   if (!destFileChecker.CanDelete())
                       throw new AccessDeniedException(info.DestinationFileName );
                    }
                   else
                   {*/
                       throw new FileAlreadyExistException(info.DestinationFileName );
                   //}
               }
                
            }

        public override void BeginTransaction()
        {
            
        }
    }
}

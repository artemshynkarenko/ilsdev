using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.IO;
using Interlogic.Trainings.Plugs.Kernel.Exceptions;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class CopyFileAction : FileAction
    {
        private bool _overwrite;

        public CopyFileAction(string sourceFilePath, string destFilePath, bool overwrite)
        {
            _fileActionInfo = new SourceDestFileInfo(sourceFilePath, destFilePath);
            _overwrite = overwrite;
        }
        protected override void ExecuteAction(IFileActionInfo fileActionInfo)
        {
            SourceDestFileInfo info = (SourceDestFileInfo)fileActionInfo;
            Check(info, _overwrite);
            File.Copy(info.SourceFileName, info.DestinationFileName, _overwrite);
            
        }
        protected override void RollbackAction(IFileActionInfo fileActionInfo)
        {
            SourceDestFileInfo info = (SourceDestFileInfo)fileActionInfo;
            if (File.Exists(info.DestinationFileName))
                File.Delete(info.DestinationFileName);
        }
        private void Check(SourceDestFileInfo info, bool overwrite)
        {
            if (Locker.IsLocked(info.DestinationFileName))
                throw new FileIsLockedException(info.DestinationFileName);

            // TODO: write normal checking 
            if (!File.Exists(info.SourceFileName))
                throw new FileNotFoundException(info.SourceFileName);
            UserFileAccessRightsChecker sourceChecker = new UserFileAccessRightsChecker(info.SourceFileName);
            if (!sourceChecker.CanRead())
                throw new AccessDeniedException(info.SourceFileName);
            UserFileAccessRightsChecker destChecker =
                new UserFileAccessRightsChecker(Path.GetDirectoryName(info.DestinationFileName));
            if (!destChecker.CanCreateFiles())
                throw new AccessDeniedException(info.DestinationFileName);

            if (File.Exists(info.DestinationFileName))
            {
                if (overwrite)
                {
                    UserFileAccessRightsChecker destFileChecker = new UserFileAccessRightsChecker(info.DestinationFileName);
                    if (!destFileChecker.CanModify())
                        throw new AccessDeniedException(info.DestinationFileName);
                }
                else
                {
                    throw new FileAlreadyExistException(info.DestinationFileName);
                }

            }
        }

        public override void BeginTransaction()
        {
            
        }
    }
}
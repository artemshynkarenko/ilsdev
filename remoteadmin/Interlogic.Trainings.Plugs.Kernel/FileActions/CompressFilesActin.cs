using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    class CompressFilesAction : FileAction
    {

        FastZipEvents zipEvents = new FastZipEvents();
        ICSharpCode.SharpZipLib.Zip.FastZip zip;
        bool _recurse = false;
        private void onCompletedFile(object sender, ICSharpCode.SharpZipLib.Core.ScanEventArgs e)
        {

        }


        private void initZipEvents()
        {
            zip = new FastZip(zipEvents);
            zip.CreateEmptyDirectories = true;
            zipEvents.CompletedFile += onCompletedFile;
        }
        public CompressFilesAction(string zipFileName, string targetPath, string fileFilter, bool recurse)
        {
            _recurse = recurse;
            CompressFilesAction(zipFileName, targetPath, fileFilter, recurse);
        }
        public CompressFilesAction(string zipFileName, string targetPath, string fileFilter)
        {
            initZipEvents();
            _fileActionInfo = new SourceTargetFilterFileInfo(zipFileName, targetPath, fileFilter);

        }

        protected override void ExecuteAction(IFileActionInfo fileActionInfo)
        {   

            SourceTargetFilterFileInfo zipFile = fileActionInfo as SourceTargetFilterFileInfo;
            zip.CreateZip(zipFile.SourceFileName, zipFile.TargetPath, recurse, zipFile.FilterFiles);

        }
    }
}
        

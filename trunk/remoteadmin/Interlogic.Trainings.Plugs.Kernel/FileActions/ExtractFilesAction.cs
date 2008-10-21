using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
    public class ExtractFilesAction : FileAction
    {
        FastZipEvents zipEvents = new FastZipEvents();        
        ICSharpCode.SharpZipLib.Zip.FastZip zip;
        List<SourceFileInfo> files = new List<SourceFileInfo>();
        Dictionary <string,SourceFileInfo> directories = new Dictionary <string,SourceFileInfo>();
        private static int pathDepth(string path)
        {
            int result=0;
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] == '/') result++;
            }
            return result;
        }
        private void onCompletedFile(object sender, ICSharpCode.SharpZipLib.Core.ScanEventArgs e)
        {
            string fileName =e.Name;
            SourceFileInfo file = new SourceFileInfo(System.IO.Path.Combine((_fileActionInfo as SourceTargetFilterFileInfo).TargetPath, fileName.Replace('/', System.IO.Path.DirectorySeparatorChar)));
            files.Add(file);
            _isExecuted = true;
            int pos=fileName.LastIndexOf('/');
            string path = "";
            if(pos > 0 && pathDepth(fileName) <= 1)
            {
               path = fileName.Substring(0,pos+1);
               if (!directories.ContainsKey(path))
               {
                   directories.Add(path,new SourceFileInfo(System.IO.Path.Combine((_fileActionInfo as SourceTargetFilterFileInfo).TargetPath, path.Replace('/', System.IO.Path.DirectorySeparatorChar))));
               }
            }
         }
        
  
        private void  initZipEvents()
        {
            zip = new FastZip(zipEvents);
            zip.CreateEmptyDirectories = true;
            zipEvents.CompletedFile += onCompletedFile;
            //zipEvents.ProcessDirectory   += onCompletedDirectory;
        }
        public ExtractFilesAction(string zipFileName,string targetPath ,string fileFilter)
        {
            initZipEvents();
            _fileActionInfo = new SourceTargetFilterFileInfo(zipFileName ,targetPath ,fileFilter);
            
        }

        protected override void ExecuteAction(IFileActionInfo fileActionInfo)
        {

            SourceTargetFilterFileInfo zipFile = fileActionInfo as SourceTargetFilterFileInfo;            
            zip.ExtractZip(zipFile.SourceFileName, zipFile.TargetPath,zipFile.FilterFiles );
            foreach (SourceFileInfo f in files)
            {
                f.LockOnExecute(Locker);
            }
        }
        public  override void Commit()
        {
            foreach (SourceFileInfo file in files)
            {
                file.UnlockOnCommit(Locker);
            }

        }
        
        public  override void RollBack()
        {
            base.RollBack();
            foreach (SourceFileInfo file in files)
            {
                file.UnlockOnRollback(Locker);
                System.IO.File.Delete(file.SourceFileName);
                
            }
            
            foreach (KeyValuePair<string,SourceFileInfo> directory in directories)
            {
                System.IO.Directory.Delete(directory.Value.SourceFileName ,true );
            }
            

        }
        public override void BeginTransaction()
        {
          
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

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
			initZipEvents();
			_fileActionInfo = new SourceTargetFilterFileInfo(zipFileName, targetPath, fileFilter);
			_recurse = recurse;
		}
		public CompressFilesAction(string zipFileName, string targetPath, string fileFilter)
			: this(zipFileName, targetPath, fileFilter, false)
		{
		}

		protected override void ExecuteAction(IFileActionInfo fileActionInfo)
		{

			SourceTargetFilterFileInfo zipFile = fileActionInfo as SourceTargetFilterFileInfo;
			zip.CreateZip(zipFile.SourceFileName, zipFile.TargetPath, _recurse, zipFile.FilterFiles);

		}

		public override void BeginTransaction()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}


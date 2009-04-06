using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.FileActions
{
   public  class SourceTargetFilterFileInfo : SourceFileInfo 
    {
        protected string _targetPath, _filterFiles;

        public string FilterFiles
        {
            get { return _filterFiles; }
            set { _filterFiles = value; }
        }

        public string TargetPath
        {
            get { return _targetPath; }
            set { _targetPath = value; }
        }
        public SourceTargetFilterFileInfo(string sourceFileName, string targetPath, string filterFiles):base(sourceFileName)
        {
            _targetPath = targetPath;
            _filterFiles = filterFiles;
        }
        
        

    }
}

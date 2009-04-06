using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.Exceptions
{
    public class FileIsLockedException : Exception
    {
        string _filename;
        public FileIsLockedException(string filename)
        {
            _filename = filename;
        }
    }
}
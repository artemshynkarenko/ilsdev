using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.Exceptions
{
    public class FileAlreadyExistException : Exception
    {
        string _filename;
        public FileAlreadyExistException(string filename)
        {
            _filename = filename;
        }
    }
}

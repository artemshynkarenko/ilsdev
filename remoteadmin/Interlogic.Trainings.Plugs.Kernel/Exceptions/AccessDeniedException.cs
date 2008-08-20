using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.Exceptions
{
    public class AccessDeniedException:Exception
    { 
        string _filename;
        public AccessDeniedException(string filename)
        {
            _filename = filename;
        }
    }
}

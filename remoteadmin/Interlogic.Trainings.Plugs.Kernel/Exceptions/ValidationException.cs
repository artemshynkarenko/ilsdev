using System;
using System.Collections.Generic;
using System.Text;

namespace Interlogic.Trainings.Plugs.Kernel.Exceptions
{
	public class ValidationException : Exception
	{
        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException() : base("") 
        {
        }
	}
}

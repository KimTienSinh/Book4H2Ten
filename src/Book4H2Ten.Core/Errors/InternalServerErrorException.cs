using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book4H2Ten.Core.Errors
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message)
            : base(message)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeFootball.Domain.Exceptions
{
    public class NoRecordsInCsvException : Exception
    {
        public NoRecordsInCsvException()
        {
        }

        public NoRecordsInCsvException(string message) : base(message)
        {
        }
    }
}

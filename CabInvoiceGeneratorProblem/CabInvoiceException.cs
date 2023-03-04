using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CabInvoiceGeneratorProblem
{
    public class CabInvoiceException : Exception//uc2
    {
        /// <summary>
        /// Enum for defining different type of custom exception
        /// </summary>

        ExceptionType type;
        public enum ExceptionType
        {
            INVALID_DISTANCE,
            INVALID_TIME,
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CabInvoiceException"/> class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public CabInvoiceException(ExceptionType type, string message) : base(message)
        {
            this.type = type;
        }
    }
}

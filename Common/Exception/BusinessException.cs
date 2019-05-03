using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class BusinessException : System.Exception
    {
        public bool ShowMessage { get; set; }
        public BusinessExceptionReasone Reasone { get; set; }

        public BusinessException() : base()
        {
            ShowMessage = false;
        }
        public BusinessException(string message) : base(message)
        {
            ShowMessage = true;
        }

        public BusinessException(BusinessExceptionReasone reasone) : base()
        {
            ShowMessage = false;
            Reasone = reasone;
        }


    }
}

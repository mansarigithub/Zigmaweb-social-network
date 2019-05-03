using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exception
{
    public class PageNotFoundException : System.Exception
    {
        public PageNotFoundException()
        {
        }

        public PageNotFoundException(string message) : base(message)
        {
        }
    }
}

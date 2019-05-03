using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigmaWeb.Model.Domain.Core
{
    public class ExceptionLog
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string HttpRequestUrl { get; set; }
    }
}

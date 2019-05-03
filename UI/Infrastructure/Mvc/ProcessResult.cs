using System;

namespace Common.Web.Mvc
{
    public class ProcessResult
    {
        public ProcessResultType Type { get; set; }
        public string Message { get; set; }

        public object Data { get; set; }

        public ProcessResult()
        {
            Type = ProcessResultType.Success;
        }

        public ProcessResult(ProcessResultType resultType)
        {
            Type = resultType;
        }

        public ProcessResult(object data)
        {
            Type = ProcessResultType.Success;
            Data = data;
        }

        public ProcessResult(ProcessResultType resultType, object data)
        {
            Type = resultType;
            Data = data;
        }
    }
}

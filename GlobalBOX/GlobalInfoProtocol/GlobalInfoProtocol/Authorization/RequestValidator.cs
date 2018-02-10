using System;
using System.Collections.Generic;
using System.Web;

namespace GlobalInfoProtocol.Authorization
{
    public class RequestValidator
    {
        public Action<string> LoggingAction { get; set; }

        public RequestValidator(Action<string> loggingAction)
        {
            LoggingAction = loggingAction;
        }

        public bool ValidateDataFieldsInRequest(HttpRequest request, List<string> fields)
        {
            var errorFields = fields.FindAll(f => string.IsNullOrEmpty(request[f]));
            errorFields.ForEach(LoggingAction);

            return errorFields.Count == 0;
        }
    }
}
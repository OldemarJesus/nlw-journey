
using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class JourneyOnValidationErrorException : JourneyException
    {
        private readonly IList<string> _errors;

        public JourneyOnValidationErrorException(IList<string> messages) : base(string.Empty)
        {
            _errors = messages;
        }

        public override IList<string> GetErrorMessages()
        {
            return _errors;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}

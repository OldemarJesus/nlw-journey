
using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class JourneyOnValidationErrorException : JourneyException
    {
        public JourneyOnValidationErrorException(string message) : base(message)
        {
            
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}

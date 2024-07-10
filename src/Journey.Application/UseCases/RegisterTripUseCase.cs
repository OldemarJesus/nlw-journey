using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application.UseCases
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDbContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Id = entity.Id,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };
        }

        public void Validate(RequestRegisterTripJson request)
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new JourneyOnValidationErrorException(Exception.ResourceErrorMessages.NAME_NULL_OR_EMPTY);
            }

            if(request.StartDate.Date < DateTime.Now.Date)
            {
                throw new JourneyOnValidationErrorException(Exception.ResourceErrorMessages.START_DATE_IN_PAST);
            }

            if(request.EndDate < request.StartDate)
            {
                throw new JourneyOnValidationErrorException(Exception.ResourceErrorMessages.END_DATE_BEFORE_START_DATE);
            }
        }
    }
}

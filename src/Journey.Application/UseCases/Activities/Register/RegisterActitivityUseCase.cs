using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActitivityUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext
                .Trips
                .FirstOrDefault(trip => trip.Id == tripId);

            Validate(trip, request);

            var activity = new Activity
            {
                Date = request.Date,
                Name = request.Name,
                TripId = trip!.Id,
            };

            dbContext.Activities.Add(activity);
            dbContext.SaveChanges();

            return new ResponseActivityJson
            {
                Id = activity.Id,
                Date = activity.Date,
                Name = activity.Name,
                Status = (ActivityStatus) activity.Status
            };
        }

        public void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if (trip == null)
            {
                throw new JourneyNotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);

            if (!(request.Date >= trip.StartDate && request.Date <= trip.EndDate))
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.ACTIVITY_DATE_NOT_IN_TRIP_DATE_RANGE));
            }

            if (!result.IsValid)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

                throw new JourneyOnValidationErrorException(errorMessages);
            }
        }
    }
}

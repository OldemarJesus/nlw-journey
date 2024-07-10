using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases
{
    public class DeleteTripByIdUseCase
    {
        public void Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext
                .Trips
                .Include(trip => trip.Activities)
                .FirstOrDefault(x => x.Id == id);

            if (trip == null)
                throw new JourneyNotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            dbContext.Trips.Remove(trip);
            dbContext.SaveChanges();
        }
    }
}

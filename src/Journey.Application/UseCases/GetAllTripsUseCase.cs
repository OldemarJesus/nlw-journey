using Journey.Communication.Responses;
using Journey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases
{
    public class GetAllTripsUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbContext = new JourneyDbContext();
            var trips = dbContext.Trips.ToList();

            return new ResponseTripsJson
            {
                Trips = trips.Select(trip => new ResponseShortTripJson
                {
                    Id = trip.Id,
                    Name = trip.Name,
                    StartDate = trip.StartDate,
                    EndDate = trip.EndDate,
                }).ToList(),
            };
        }
    }
}

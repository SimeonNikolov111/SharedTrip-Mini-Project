using System;
using System.Collections.Generic;
using System.Text;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    public interface ITripsService
    {
        void Add(AddTripViewModel input);

        IEnumerable<Trip> GetAll();

        TripDetailsViewModel GetById(string id);

        bool AddUserToTrip(string tripId, string user);
    }
}

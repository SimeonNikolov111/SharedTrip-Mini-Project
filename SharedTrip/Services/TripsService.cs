using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;
using SharedTrip.Models;
using SharedTrip.ViewModels.Trips;

namespace SharedTrip.Services
{
    class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public TripDetailsViewModel GetById(string tripId)
        {
            var trip = this.db.Trips.Where(x => x.Id == tripId)
                .Select(x => new TripDetailsViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToString("MM/dd/yyyy HH:mm"),
                    Seats = x.Seats,
                    Description = x.Description,
                    ImagePath = x.ImagePath

                }).FirstOrDefault();

            return trip;
        }


        public void Add(AddTripViewModel input)
        {
            var trip = new Trip()
            {
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = input.DepartureTime,
                ImagePath = input.ImagePath,
                Seats = input.Seats,
                Description = input.Description,
            };

            this.db.Trips.Add(trip);
            db.SaveChanges();
        }



        public IEnumerable<Trip> GetAll()
            => this.db.Trips.Select(x => new Trip
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                DepartureTime = x.DepartureTime,
                Seats = x.Seats,
                ImagePath = x.ImagePath
            })
                .ToArray();


        public bool AddUserToTrip(string tripId, string user)
        {
            var targetTrip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);
            var userTrip = new UserTrip
            {
                TripId = tripId,
                UserId = user
            };

            // Make mapping table validation
            if (!this.db.UserTrips.Any(x => x.TripId == userTrip.TripId && x.UserId == userTrip.UserId) && targetTrip.Seats > 0)
            {
                targetTrip.Seats -= 1;
                targetTrip.UserTrips.Add(userTrip);
                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

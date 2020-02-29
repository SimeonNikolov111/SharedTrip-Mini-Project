using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SharedTrip.App.Controllers;
using SharedTrip.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.User;
using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip.Controllers
{
    class TripsController : Controller
    {
        private readonly ITripsService tripsService;
        private readonly ApplicationDbContext db;

        public TripsController(ITripsService tripsService, ApplicationDbContext db)
        {
            this.tripsService = tripsService;
            this.db = db;
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            tripsService.Add(input);

            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewModel = tripsService.GetAll();
            return this.View(viewModel, "All");
        }

        [HttpGet]
        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewmodel = this.tripsService.GetById(tripId);

            return this.View(viewmodel, "Details");
        }


        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var isAdded = this.tripsService.AddUserToTrip(tripId, this.User);

            if (isAdded)
            {
                return this.Redirect("/Trips/All");
            }

            return this.Redirect($"/Trips/Details?tripId={tripId}");
        }
    }
}

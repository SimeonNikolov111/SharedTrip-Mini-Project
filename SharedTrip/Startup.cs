using SharedTrip.Models;
using SharedTrip.Services;

namespace SharedTrip
{
    using System.Collections.Generic;

    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        //Testing change for Git
        public void Configure(IList<Route> routeTable)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Database.EnsureCreated();
            }
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ITripsService,TripsService>();
        }
    }
}

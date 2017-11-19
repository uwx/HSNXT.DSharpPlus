using System.Collections.Generic;
using HSNXT.ComLib.Application;
using HSNXT.ComLib.Entities;
using HSNXT.ComLib.LocationSupport;

namespace HSNXT.ComLib.Samples
{
    /// <summary>
    /// Example for the LocationSupport namespace.
    /// </summary>
    public class Example_Location : App
    {

        /// <summary>
        /// Run the application.
        /// </summary>
        public override BoolMessageItem Execute()
        {
            // Example 1:
            // This actually parses the location names and looks it up the various repositories.
            // This is typically what's entered into the location text box for sites like 
            // yelp, eventful, upcoming.org.

            Print(Location.Parse("brooklyn"));
            Print(Location.Parse("miami,Florida"));
            Print(Location.Parse("new jersey"));
            Print(Location.Parse("bronx"));
            Print(Location.Parse("bronx,new york"));
            Print(Location.Parse("bronx,connecticut"));
            Print(Location.Parse("venice,italy"));
            Print(Location.Parse("venice,united states"));

            // Example 2:
            // Get all the countries
            var countries = Location.Countries.GetAll();
            
            // Example 3:
            // Get all the states for a specific country.
            var unitedStates = Location.StatesFor("USA").Item;

            // Example 4:
            // Get all the cities for a specific state, country combination.
            var citiesInNewYork = Location.CitiesFor("USA", "New York").Item;

            // Example 5:
            // Get a country lookup which can be lookup countries by both Id and Name.
            var countriesLookup = Location.CountriesLookup;

            return BoolMessageItem.True;
        }


        /// <summary>
        /// Initialize with data.
        /// </summary>
        public override void Init()
        {
            base.Init();

            // Using the In-Memory repository for testing purposes.
            IRepository<City> cityRepo = new RepositoryInMemory<City>("Id,Name,StateId,CountryId");
            IRepository<State> stateRepo = new RepositoryInMemory<State>("Id,Name,StateId,CountryId");
            IRepository<Country> countryRepo = new RepositoryInMemory<Country>("Id,Name,StateId,CountryId");
            ILocationService locationService = new LocationService(() => countryRepo, () => stateRepo, () => cityRepo);
            Location.Init(locationService);


            Location.CreateCountry("Italy");
            Location.CreateCountry("United States");
            Location.CreateCountry("Spain");
            Location.CreateCountry("India");
            Location.CreateCountry("United States", "USA", true);
            Location.CreateCountry("United States", "America", true);
            Location.CreateCountry("United States", "U.S.A", true);
            var countries = Location.Countries.GetAll();

            Location.CreateState("New York", "NY", "USA");
            Location.CreateState("New Jersey", "NJ", "USA");
            Location.CreateState("Connecticut", "CT", "USA");
            Location.CreateState("ItalianState1", "IT1", "Italy");
            Location.CreateState("Florida", "FL", "USA");
            Location.CreateState("California", "CA", "USA");
            var states = Location.States.GetAll();

            Location.CreateCity("New York", "New York", "USA");
            Location.CreateCity("Brooklyn", "New York", "USA");
            Location.CreateCity("Bronx", "Connecticut", "USA");
            Location.CreateCity("Bronx", "New York", "USA");
            Location.CreateCity("San Francisco", "California", "USA");
            Location.CreateCity("Trenton", "New Jersey", "USA");
            Location.CreateCity("Miami", "Florida", "USA");
            Location.CreateCity("Venice", "ItalianState1", "Italy");
        }


        private void Print(LocationLookUpResult result)
        {
            var format1 = "City/State/Country = '{0}', '{1}', '{2}'";
            var format2 = "LookupBy : {0} , IsValid = {1}";
            var line1 = string.Format(format2, result.LookUpType.ToString(), result.IsValid);
            var line2 = string.Format(format1, result.City, result.State, result.Country);
            Log.Info(line1 + "    " + line2);
        }
    }
}

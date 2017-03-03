using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Venue> AllVenues = Venue.GetAll();
                return View["index.cshtml", AllVenues];
            };

            Post["/venue/new"] = _ => {
                Venue newVenue = new Venue(Request.Form["name"]);
                newVenue.Save();
                List<Venue> AllVenues = Venue.GetAll();
                return View["index.cshtml", AllVenues];
            };

            Get["/venue/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                var SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueBands = SelectedVenue.GetBands();
                model.Add("venue", SelectedVenue);
                model.Add("bands", VenueBands);
                return View["venue.cshtml", model];
            };
        }
    }
}

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

            Get["/bands"] = _ => {
                List<Band> AllBands = Band.GetAll();
                return View["bands.cshtml", AllBands];
            };

            Post["/venue/new"] = _ => {
                Venue newVenue = new Venue(Request.Form["name"]);
                newVenue.Save();
                List<Venue> AllVenues = Venue.GetAll();
                return View["index.cshtml", AllVenues];
            };

            Post["/band/new"] = _ => {
                Band newBand = new Band(Request.Form["name"]);
                newBand.Save();
                List<Band> AllBands = Band.GetAll();
                return View["bands.cshtml", AllBands];
            };

            Get["/venue/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                var SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueBands = SelectedVenue.GetBands();
                model.Add("venue", SelectedVenue);
                model.Add("bands", VenueBands);
                return View["venue.cshtml", model];
            };

            Delete["/venue/delete/{id}"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.Delete();
                List<Venue> AllVenues = Venue.GetAll();
                return View["index.cshtml", AllVenues];
            };

            Get["/venue/edit/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                var SelectedVenue = Venue.Find(parameters.id);
                List<Band> VenueBands = SelectedVenue.GetBands();
                model.Add("venue", SelectedVenue);
                model.Add("bands", VenueBands);
                return View["venue_edit.cshtml", model];
            };

            Patch["/venue/edit/{id}"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.Update(Request.Form["venue-name"]);
                List<Venue> AllVenues = Venue.GetAll();
                return View["index.cshtml", AllVenues];
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.RawModels;
using Newtonsoft.Json.Linq;

namespace Logic
{
    public class Mapper
    {
        private readonly string _mapboxurl = "https://api.mapbox.com/";
        private readonly string _mapboxtoken = "access_token=sk.eyJ1Ijoibm90YXNhbGFkIiwiYSI6ImNrbmFucHY5MDBmNWgydmxsZTBsdmZrY3oifQ._ZzlvInwLBJvA6gGIqdLPA";
        public Mapper()
        {

        }

        internal User RawToUser(RawUser rUser)
        {
            User newUser = new User();
            using (var hmac = new HMACSHA512())
            {
                newUser.FName = rUser.firstName;
                newUser.LName = rUser.lastName;
                newUser.IsEventManager = false;
                newUser.Email = rUser.email.ToLower();
                newUser.Events = new List<Event>();
                newUser.PasswordSalt = hmac.Key;
                newUser.Password = PasswordHash(rUser.password, newUser.PasswordSalt);
            }
            return newUser;
        }

        internal async Task<Location> AddressToLocation(RawEvent userEvent)
        {
            HttpClient client = new HttpClient();
            string addressLocation = userEvent.Street + " " + userEvent.ZipCode + " " + userEvent.City + " " + userEvent.State;
            string correct = addressLocation.Replace(" ", "%20");
            string path = _mapboxurl + "geocoding/v5/mapbox.places/"+correct+".json?country=US&types=address&"+_mapboxtoken;
            HttpResponseMessage response = await client.GetAsync(path);
            if(response.IsSuccessStatusCode)
            {
                Location newLoc = new Location();
                string jsonContent = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(jsonContent);
                JToken token = json["features"];
                if(!token.HasValues)
                {
                    return null;
                }
                newLoc.Name = userEvent.Street;
                newLoc.Address = userEvent.City + " " + userEvent.State + " " + userEvent.ZipCode;
                newLoc.Longtitude = (double)json["features"][0]["center"][0];
                newLoc.Latitude = (double)json["features"][0]["center"][1];
                return newLoc;
            }
            else
            {
                return null;
            }
        }

        internal RawPreviewEvent EventToPreview(Event e)
        {
            RawPreviewEvent newPreview = new RawPreviewEvent();
            newPreview.Id = e.Id;
            newPreview.Name = e.Name;
            newPreview.Date = e.Date;
            newPreview.Location = e.Location.Name + " " + e.Location.Address;
            return newPreview;
        }

        internal RawUserLogin UserToRawLogin(User getUser)
        {
            RawUserLogin returnUser = new RawUserLogin();
            returnUser.id = getUser.Id;
            returnUser.firstName = getUser.FName;
            returnUser.lastName = getUser.LName;
            returnUser.email = getUser.Email;
            returnUser.IsEventManager = getUser.IsEventManager;
            if(getUser.IsEventManager)
            {
                returnUser.Role = 1;
            }
            else
            {
                returnUser.Role = 0;
            }
            return returnUser;
        }

        internal RawDetailEvent EventToDetail(Event getEvent, int totalAttending)
        {
            RawDetailEvent detailEvent = new RawDetailEvent();
            detailEvent.Name = getEvent.Name;
            detailEvent.Date = getEvent.Date;
            detailEvent.Location = getEvent.Location.Name + " " + getEvent.Location.Address;
            detailEvent.Description = getEvent.Description;
            detailEvent.EventType = getEvent.EventType.Name;
            detailEvent.Manager = getEvent.Manager.FName + " " + getEvent.Manager.LName;
            detailEvent.Capacity = getEvent.Capacity;
            detailEvent.CurrentAttending = totalAttending;
            detailEvent.TicketPrice = getEvent.Revenue;
            detailEvent.LocationMap = GetLocationMapString(getEvent.Location);
            return detailEvent;
        }

        internal Review RawToReview(RawReview review, User theUser, Event theEvent)
        {
            Review newReview = new Review();
            newReview.Rating = review.Rating;
            newReview.Description = review.Description;
            newReview.User = theUser;
            newReview.Event = theEvent;

            return newReview;
        }

        internal RawReviewToFE ReviewToRaw(Review newReview, string user, string eventName)
        {
            RawReviewToFE review = new RawReviewToFE();
            review.User = user;
            review.Event = eventName;
            review.Rating = newReview.Rating;
            review.Description = newReview.Description;

            return review;
        }

        internal RawUser UserToRaw(User e)
        {
            RawUser newUser = new RawUser();
            newUser.firstName = e.FName;
            newUser.lastName = e.LName;
            newUser.email = e.Email;
            return newUser;
        }

        internal UsersEvent signUpById(Guid uid, Guid eid, User user, Event tEvent)
        {
            UsersEvent newUserEvent = new UsersEvent();
            newUserEvent.UserId = uid;
            newUserEvent.User = user;
            newUserEvent.EventId = eid;
            newUserEvent.Event = tEvent;

            return newUserEvent;
        }

        private string GetLocationMapString(Location location)
        {
            string longt = location.Longtitude.ToString();
            string lat = location.Latitude.ToString();

            string pin = "pin-s-l+000("+longt+","+lat+")";
            string imageString = _mapboxurl + "styles/v1/mapbox/streets-v11/static/"+pin+"/"+longt+","+lat+", 15, 0/400x400?" + _mapboxtoken;

            return imageString;
        }

        internal async Task<Event> RawToEvent(RawEvent userEvent, EventType eventType, Location loc, User manager)
        {
            Event newEvent = await Task.Run(() => new Event());
            newEvent.Name = userEvent.Name;
            newEvent.Date = DateTime.Parse(userEvent.Date);
            newEvent.Description = userEvent.Description;
            newEvent.Capacity = userEvent.Capacity;
            newEvent.EventType = eventType;
            newEvent.Location = loc;
            newEvent.Manager = manager;
            newEvent.Revenue = userEvent.TicketPrice;
            newEvent.TotalCost = 0;
            newEvent.TotalTicketsSold = 0;
            newEvent.Users = new List<User>();
            return newEvent;
        }

        public byte[] PasswordHash(string password, byte[] salt)
        {
            using HMACSHA512 hmac = new HMACSHA512(key: salt);

            var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return hashedPassword;
        }

        internal EventType StringToEventType(string t)
        {
            EventType newType = new EventType();
            newType.Name = t;
            return newType;
        }
    }
}
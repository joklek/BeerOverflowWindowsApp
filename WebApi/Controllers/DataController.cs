using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Database;
using WebApi.Models;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.DataModels;
using WebApi.Exceptions;
using WebApi.Utilities;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult Register([FromBody]User User)
        {
            var dbManager = new DatabaseManager();
            bool result = dbManager.Register(User);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult logIn([FromBody]User User)
        {
            var dbManager = new DatabaseManager();
            bool result = dbManager.LogIn(User);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetAllBarData([FromBody]UserAndBarsModel userAndBars)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetAllBarData(userAndBars.Bars, userAndBars.User.Username);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBarsAround([FromBody]LocationRequestModel locationRequest)
        {
            try
            {
                InputDataValidator.LocationDataIsCorrect(locationRequest);
            }
            catch (ArgumentsForProvidersException e)
            {
                return BadRequest("Invalid arguments:" + e.InvalidArguments);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
            List<BarData> result = await BarFetcher.RequestBarsAroundCoords(locationRequest);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult SaveBarRating([FromBody] RatingModel barObject)
        {
            var dbManager = new DatabaseManager();
            if (!dbManager.LogIn(barObject.User))
                return BadRequest("User authentication failed");
            dbManager.SaveBarRating(barObject.BarID, barObject.User.Username , barObject.Rating);
            return Ok("Success");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarRatings([FromBody]UserAndBarModel userAndBar)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetBarRatings(userAndBar.Bar, userAndBar.User.Username);
            return Ok(result);
        }
        //TEMPORARY: i think we only need 2 methods in WebApi for now: SaveBarRating(username, rating, barId) and GetBarsByCoordinates(username?, latitude, longitude, radius?)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBarsByCoordinates([FromBody]Coordinate coordinate)
        {
            var locationRequest = new LocationRequestModel { Latitude = coordinate.lat, Longitude = coordinate.lng, Radius = 150, User =  "test"  };
            try
            {
                InputDataValidator.LocationDataIsCorrect(locationRequest);
            }
            catch (ArgumentsForProvidersException e)
            {
                return BadRequest("Invalid arguments:" + e.InvalidArguments);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
            List<BarData> result = await BarFetcher.RequestBarsAroundCoords(locationRequest);
            result = new DatabaseManager().GetAllBarData(result, "Jonas");
            new DatabaseManager().SaveBars(result);
            return Ok(result);

        }
    }
}

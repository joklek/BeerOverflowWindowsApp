using System;
using System.Collections.Generic;
using System.Linq;
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
        public IHttpActionResult Register([FromBody]string nameAndPass)
        {
            var list = nameAndPass.Split(',');
            var dbManager = new DatabaseManager();
            var result = dbManager.Register(list[0], list[1]);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult Register([FromBody]User user)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.Register(user.Username, user.Password);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetAllBarData([FromBody]UserAndBarsModel userAndBars)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetAllBarData(userAndBars.BarIds);
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
            var names = result.Select(x => x.BarId).ToList();
            var dbMgr = new DatabaseManager();
            result = dbMgr.GetAllBarData(names);
            dbMgr.SaveBars(result);
            return Ok(BarDataToResponseModel(result));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult SaveBarRating([FromBody]RatingModel barObject)
        {
            var dbManager = new DatabaseManager();
            // TODO: improve the logic of this thingie
            var user = new User
            {
                Username = "test"
            };
            barObject.User = user;
            if (dbManager.UserCanVote(barObject.BarID, user))
            {
                try
                {
                    dbManager.SaveBarRating(barObject.BarID, barObject.User, barObject.Rating);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e);
                    return InternalServerError();
                }
                return Ok("Success");
            }
            return BadRequest("Cannot vote");
        }

        /*[EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarRatings([FromBody]UserAndBarModel userAndBar)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetBarRatings(userAndBar.BarId, userAndBar.User);
            return Ok(result);
        }*/

        // TEMPORARY: i think we only need 2 methods in WebApi for now: SaveBarRating(username, rating, barId) and GetBarsByCoordinates(username?, latitude, longitude, radius?)
        // DO NOT USE THIS
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public async Task<IHttpActionResult> GetBarsByCoordinates([FromBody] Coordinate coordinate)
        {
            var locationRequest = new LocationRequestModel
            {
                Latitude = coordinate.lat,
                Longitude = coordinate.lng,
                Radius = 150,
                User = "test"
            };
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

            var names = result.Select(x => x.BarId).ToList();
            var dbMgr = new DatabaseManager();
            result = dbMgr.GetAllBarData(names);
            dbMgr.SaveBars(result);
            return Ok(BarDataToResponseModel(result));
        }

        private static List<BarResponseModel> BarDataToResponseModel(IEnumerable<BarData> barList)
        {
            var responseModelList = barList.Select(bar => new BarResponseModel
                {
                    BarId = bar.BarId,
                    Title = bar.Title,
                    AvgRating = bar.AvgRating,
                    Categories = bar.Categories,
                    City = bar.City,
                    StreetAddress = bar.StreetAddress
                }).ToList();
            return responseModelList;
        }
    }
}

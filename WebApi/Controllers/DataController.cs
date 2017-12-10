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
        public IHttpActionResult Register([FromBody]User userToRegister)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.Register(userToRegister);
            return Ok(result);
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult LogIn([FromBody]User userAttemptingToLogin)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.LogIn(userAttemptingToLogin);
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
            // Sending the results from databases causes an exception, as the virtual fields no longer exist.
            // If someone knows a better solution - please tell me @joklek
            return Ok(BarDataToResponseModel(result));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult SaveBarRating([FromBody]RatingModel barObject)
        {
            var dbManager = new DatabaseManager();
            if (!dbManager.LogIn(barObject.User))
            {
                return BadRequest("User authentication failed");
            }
                
            if (dbManager.UserCanVote(barObject.BarID, barObject.User, out var cooldown))
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
            return BadRequest("User cannot vote for " + cooldown.Minutes + " min. on this bar");
        }

        private static List<BarResponseModel> BarDataToResponseModel(IEnumerable<BarData> barList)
        {
            var responseModelList = barList.Select(bar => new BarResponseModel
                {
                    BarId = bar.BarId,
                    Title = bar.Title,
                    Latitude = bar.Latitude,
                    Longitude = bar.Longitude,
                    AvgRating = bar.AvgRating,
                    Categories = bar.Categories,
                    City = bar.City,
                    StreetAddress = bar.StreetAddress
                }).ToList();
            return responseModelList;
        }
    }
}

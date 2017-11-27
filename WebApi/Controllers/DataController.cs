using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Database;
using WebApi.DataModels;
using WebApi.Exceptions;
using WebApi.Utilities;
using BarData = WebApi.DataModels.BarData;
using RatingModel = WebApi.DataModels.RatingModel;
using UserAndBarModel = WebApi.DataModels.UserAndBarModel;
using UserAndBarsModel = WebApi.DataModels.UserAndBarsModel;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetAllBarData([FromBody]UserAndBarsModel userAndBars)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetAllBarData(userAndBars.Bars, userAndBars.User);            
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
            dbManager.SaveBarRating(barObject.Bar, barObject.User, barObject.Rating);
            return Ok("Success");
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarRatings([FromBody]UserAndBarModel userAndBar)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetBarRatings(userAndBar.Bar, userAndBar.User);
            return Ok(result);
        }
    }
}

using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Database;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class DataController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetAllBarData([FromBody]UserAndBarsModel UserAndBars)
        {
            var dbManager = new DatabaseManager();
            List<BarData> result = dbManager.GetAllBarData(UserAndBars.Bars, UserAndBars.User);            
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
        //TEMPORARY: should not send unneccesary data
        //TEMPORARY: probably should save empty bars so it wouldn't be needed to send them through Api when trying to save rating
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarRatings([FromBody]UserAndBarModel userAndBar)
        {
            var dbManager = new DatabaseManager();
            var result = dbManager.GetBarRatings(userAndBar.Bar, userAndBar.User);
            return Ok(result);
        }
        //TEMPORARY: i think we only need 2 methods in WebApi for now: SaveBarRating(username, rating, barId) and GetBarsByCoordinates(username?, latitude, longitude, radius?)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public IHttpActionResult GetBarsByCoordinates([FromBody]string coordinates)
        {
            var result = new List<BarModel> { };
            //TEMPORARY: create fake data
            for (var i = 0; i < 10; i++)
            {
                var bar = new BarModel { Id = i.ToString(), Category = "category" + i, Rating = i / 2, Title = "title" + i * 5 };
                result.Add(bar);
            }
            return Ok(result);
        }
        
    }
}

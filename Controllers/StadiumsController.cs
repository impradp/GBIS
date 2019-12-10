using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GBIS.Models;
using GBIS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBIS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StadiumsController : ControllerBase
    {
        //controller
        public StadiumsController(JsonFileStadiumService stadiumService)
        {
            this.StadiumService = stadiumService;
        }

        public JsonFileStadiumService StadiumService { get; }

        [HttpGet]
        public IEnumerable<Stadium> Get()
        {
            return StadiumService.GetStadiums();
        }

        //[HttpPatch] Get That From Body
        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery]string StadiumId,[FromQuery]int Rating)
        {
            StadiumService.AddRating(StadiumId,Rating);
            return Ok();
        }
    }
}
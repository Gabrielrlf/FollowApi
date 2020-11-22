using System;
using Api.Data.Collection;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfectedController : ControllerBase
    {
        Data.DbContext _dbContext;
        IMongoCollection<Infected> _InfectedCollection;

        public InfectedController(Data.DbContext dbContext)
        {
            _dbContext = dbContext;
            _InfectedCollection = _dbContext.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
        }

        [HttpPost, Route("InfectedSave")]
        public ActionResult InfectedSave([FromBody] InfectedDTO infectedDTO)
        {
            var infected = new Infected(infectedDTO.Cpf, infectedDTO.DateBith, infectedDTO.Genre, infectedDTO.Latitude, infectedDTO.Longitude);
            infected.Status = true;
            _InfectedCollection.InsertOne(infected);

            return StatusCode(201, "Infected sucessfull save!");
        }

        [HttpPost, Route("InfectedCuredSave")]
        public ActionResult InfectedCuredSave(long cpf)
        {
            var infected = Builders<Infected>.Filter.Eq("Cpf", cpf);
            var update = Builders<Infected>.Update.Set("Status", false);
            var result = _InfectedCollection.Find(infected).FirstOrDefault();
            if (result.Status.Equals(false))
                return Ok("This pacient  its already cured!");

            _InfectedCollection.UpdateOneAsync(infected, update);
            return StatusCode(201, "Infected was cured!");

        }

        [HttpGet, Route("CheckSituatiion")]
        public ActionResult CheckSituatiion(long cpf)
        {
            var infected = Builders<Infected>.Filter.Eq("Cpf", cpf);
            var result = _InfectedCollection.Find(infected).FirstOrDefault();

            if (result.Status.Equals(false))
                return Ok("This pacient was cured!");
            else
                return Ok("This pacient continue infected!");
        }

        [HttpGet, Route("GetInfected")]
        public ActionResult GetInfected()
        {
            var infected = _InfectedCollection.Find(Builders<Infected>.Filter.Empty).ToList();
            return Ok(infected);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeerNet.Models;
using MongoDB.Bson;
using BeerNet.MathFunctions;

namespace BeerNet.Controllers
{
    [Produces("application/json")]
    [Route("beernet/adjunct")]
    public class AdjunctController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            DataAccess accessor = new DataAccess();
            IEnumerable<adjunct> currentRecipe = accessor.GetAll<adjunct>();
            return Json(currentRecipe.ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataAccess accessor = new DataAccess();
            adjunct currentRecipe = accessor.Get<adjunct>(id);
            return Json(currentRecipe);
        }

        // POST api/values
        [HttpPost("{id}")]
        [HttpPost]
        public IActionResult Post([FromBody]adjunct value, string id)
        {
            //double ibu = MathFunctions.IBU.basicIBU(value, 1.07);

            DataAccess accessor = new DataAccess();
            value = GlobalFunctions.AddIdIfNeeded(value, id);

            return Json(accessor.Post(value));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {

            DataAccess accessor = new DataAccess();

            return Json(accessor.Delete<adjunct>(id));
        }
    }
}
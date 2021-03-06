﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeerNet.Models;
using BeerNet.MathFunctions;

namespace BeerNet.Controllers
{
    [Route("beermath/[controller]")]
    public class ibuController : Controller
    {
        /*
         * This controller was for testing and is now largely unnecessary
         * */
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            DataAccess accessor = new DataAccess();
            IEnumerable<hop> hopThing = accessor.GetAll<hop>();
            return Json(hopThing.ToList<hop>());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataAccess accessor = new DataAccess();
            recipe hopThing = accessor.Get<recipe>(id);
            return Json(hopThing);
        }

        // POST api/values
        [HttpPost]
        public double Post([FromBody]List<hopAddition> value)
        {
            double ibu = 65;//MathFunctions.IBU.basicIBU(value, 1.07);
            return ibu;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

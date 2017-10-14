﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BeerNet.Models;
using MongoDB.Bson;

namespace BeerNet.Controllers
{
    [Route("beernet/[controller]")]
    public class recipeController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            DataAccess accessor = new DataAccess();
            IEnumerable<recipe> currentRecipe = accessor.GetRecipes();
            return Json(currentRecipe.ToList<recipe>());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataAccess accessor = new DataAccess();
            recipe currentRecipe = accessor.GetRecipe(id);
            return Json(currentRecipe);
        }

        // POST api/values
        [HttpPost("{id}")]
        [HttpPost]
        public RecipeStatsResponse Post([FromBody]recipe value, string id)
        {
            RecipeStatsResponse response = new RecipeStatsResponse();
            if (value != null)
            {
                DataAccess accessor = new DataAccess();
                try
                {
                    if (id != null)
                        value.Id = ObjectId.Parse(id);
                    value.recipeStats = MathFunctions.GlobalFunctions.updateStats(value);
                    bool recipeResponse = accessor.PostRecipe(value);
                    response = new RecipeStatsResponse();
                    response.recipeStats = value.recipeStats;
                    return response;
                }
                catch (Exception e)
                {
                    response.Fail(e);
                    return response;
                }
            }
            else
            {
                response.RecipeNullFailure();
                return response;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [HttpDelete]
        public string Delete(string id)
        {
            DataAccess accessor = new DataAccess();
            try
            {
                if (id != null)
                    return accessor.deleteRecipe(id);
                else
                    return "fail";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
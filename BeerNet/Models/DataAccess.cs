﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BeerNet.Models
{
    public class DataAccess
    {
        MongoClient _client;
        //MongoServer _server;
        IMongoDatabase _db;

        public DataAccess()
        {
            //mongodb://rest.unacceptable.beer:5283 <- the server IP and stuff
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("BeerNet");
        }

        public IEnumerable<T> GetAll<T>()
        {
            return _db.GetCollection<T>(typeof(T).Name).Find(_ => true).ToList();
        }

        public T Get<T>(string id)
        {
            FilterDefinition<T> def = "{_id: \"" + id + "\"}";
            var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
            string collection = typeof(T).Name;
            List<T> result = _db.GetCollection<T>(collection).Find(filter).ToList<T>();
            if (result.Count > 0)
                return result[0];

            return default(T);
        }

        public Response Post<T>(T document)
        {
            Response r = new Response();
            try
            {
                
                var idProperty = typeof(T).GetProperty("Id");
                ObjectId value = (ObjectId)idProperty.GetValue(document);
                if (value == ObjectId.Empty)
                {
                    _db.GetCollection<T>(typeof(T).Name).InsertOne(document);
                }
                else
                {
                    var filter = Builders<T>.Filter.Eq("_id", value);

                    _db.GetCollection<T>(typeof(T).Name).ReplaceOne(filter, document);
                }

                ObjectId oid = (ObjectId)idProperty.GetValue(document);
                r.Message = oid.ToString();
            }
            catch (Exception ex)
            {
                r.Fail(ex);

            }

            return r;
        }

        public Response Delete<T>(string id)
        {
            Response r = new Response();
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
                _db.GetCollection<T>(typeof(T).Name).DeleteOne(filter);
            }
            catch (Exception ex)
            {
                r.Fail(ex);
            }

            return r;
        }

        public bool PostRecipe(recipe currentRecipe)
        {
            //Maybe one day...
            //Post(currentRecipe);

            if (currentRecipe.Id == ObjectId.Empty)
            {
                _db.GetCollection<recipe>("recipe").InsertOne(currentRecipe);
                return true;
            }
            else
            {
                ObjectId recipeObjectID = ObjectId.Parse(currentRecipe.idString);
                currentRecipe.Id = recipeObjectID;
                _db.GetCollection<recipe>("recipe").ReplaceOne<recipe>(j => j.Id == recipeObjectID, currentRecipe);
                return ytue;
            }
        }
    }
}

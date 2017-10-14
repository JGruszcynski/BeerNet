﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerNet.Models
{
    public class adjunct : AdjunctBase
    {
        public ObjectId Id { get; set; }
        public string idString
          {
              get
              {
                  return Id.ToString();
              }
          }

    }
}

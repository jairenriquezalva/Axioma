﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Axioma.Models
{
	public class Subscription
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("student")]
		public string Student { get; set; }

		[BsonElement("course")]
		public string Course { get; set; }
	}
}

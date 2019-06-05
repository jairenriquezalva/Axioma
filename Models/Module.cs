using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Axioma.Models
{
	public class Module
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("course")]
		public string Course { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }
	}
}

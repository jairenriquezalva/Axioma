using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Axioma.Models
{
	public class Course
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public String Id { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("university")]
		public string University { get; set; }

		[BsonElement("image")]
		public string Image { get; set; }
	}
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axioma.Models
{
	public class Student
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("user")]
		public string User { get; set; }

		[BsonElement("university")]
		public string University { get; set; }

	}
}

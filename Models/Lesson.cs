using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Axioma.Models
{
	public class Lesson
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("module")]
		public string Module { get; set; }
		
		[BsonElement("video")]
		public string Video { get; set; }

		[BsonElement("text")]
		public string Text { get; set; }

		[BsonElement("pdf")]
		public string Pdf { get; set; }
	}
}

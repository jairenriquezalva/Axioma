using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Axioma.Models
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		[BsonElement("username")]
		public string Username { get; set; }

		[BsonElement("password")]
		public string Password { get; set; }

		[BsonElement("email")]
		public string Email { get; set; }

		[BsonElement("firstname")]
		public string Firstname { get; set; }

		[BsonElement("surname")]
		public string Surname { get; set; }

		[BsonElement("role")]
		public string Role { get; set; }
	}
}

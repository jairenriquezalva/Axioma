using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axioma.Services
{
	public class UserService
	{
		private readonly IMongoCollection<User> users;

		public UserService(IConfiguration configuration)
		{
			MongoClient client = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
			IMongoDatabase db= client.GetDatabase("axiomadb");
			users= db.GetCollection<User>("users");
		}

		public List<User> Get()
		{
			return users.Find(user => true).ToList();
		}

		public User Get(string id)
		{
			return users.Find(user => user.Id == id).FirstOrDefault();
		}

		public User Create(User user)
		{
			users.InsertOne(user);
			return user;
		}

	}
}

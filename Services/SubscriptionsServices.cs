using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axioma.Services
{
	public class SubscriptionsService
	{
		private readonly IMongoCollection<Subscription> subscriptions;
        private readonly StudentService studentService;

		public SubscriptionsService(IConfiguration configuration, StudentService studentService)
		{
            this.studentService = studentService;
			MongoClient client = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
			IMongoDatabase db = client.GetDatabase("axiomadb");
			subscriptions = db.GetCollection<Subscription>("subscriptions");
		}

		public List<Subscription> Get(string userId)
		{
            string studentId = studentService.Get(userId).Id;
			return subscriptions.Find(sub => sub.Student == studentId).ToList();
		}
		public Subscription Create(Subscription subscription)
		{
			subscriptions.InsertOne(subscription);
			return subscription;
		}

		public void Delete(string userId, string courseId)
		{
            string studentId = studentService.Get(userId).Id;
			subscriptions.DeleteOne(sub => sub.Id == studentId && sub.Course == courseId);
		}

	}
}
using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Axioma.Services
{
	public class StudentService
	{
		private readonly IMongoCollection<Student> students;

		public StudentService(IConfiguration configuration)
		{
			MongoClient client = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
			IMongoDatabase db = client.GetDatabase("axiomadb");
			students = db.GetCollection<Student>("students");
		}

		public List<Student> Get()
		{
			IFindFluent<Student,Student> ff = students.Find(student => true);
			return ff.ToList();
		}

		public Student Get(string userId)
		{
			return students.Find(student => student.User == userId).FirstOrDefault();
		}

		public Student Create(Student student)
		{
			students.InsertOne(student);
			return student;
		}

		public void Update(string userId, Student student)
		{
			students.ReplaceOne(s => s.User == userId, student);
		}

	}
}

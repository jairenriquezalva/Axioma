using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;

namespace Axioma.Services
{
	public class CoursesService
	{
		UniversitiesService universitiesService;
		StudentService studentService;
		IMongoCollection<Course> courses;
		IMongoDatabase db;
		public CoursesService(IConfiguration configuration,
							 UniversitiesService universitiesService,
							 StudentService studentService
							 )
		{
			this.universitiesService = universitiesService;
			this.studentService = studentService;
			MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
			db = mongoClient.GetDatabase("axiomadb");
			courses = db.GetCollection<Course>("courses");
		}

		public IEnumerable<Course> GetAll(string userId)
		{
			string universityId = studentService.Get(userId).University;
			return courses.Find(x => x.University == universityId).ToList();
		}

		public Course Get(string id){
			return courses.Find(c => c.Id == id).FirstOrDefault();
		}

		public Course Create(Course course){
			courses.InsertOne(course);
			return course;
		}

		public string CreateImage(string id, byte[] bytes)
		{
			var bucket = new GridFSBucket(db, new GridFSBucketOptions
			{
				BucketName = "CourseImages"
			});

			Course course = courses.Find(u => u.Id == id).FirstOrDefault();

			ObjectId bid = bucket.UploadFromBytes(course.Name, bytes);
			return bid.ToString();
		}

		public byte[] GetImage(string idstring)
		{
			var bucket = new GridFSBucket(db, new GridFSBucketOptions
			{
				BucketName = "CourseImages"
			});
			ObjectId id = new ObjectId(idstring);
			byte[] bytes = bucket.DownloadAsBytes(id);
			return bytes;
		}

        public void Update(string id, Course course)
        {
            courses.ReplaceOne(c => c.Id == id, course);
        }
    }
}

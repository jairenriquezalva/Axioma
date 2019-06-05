using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;

namespace Axioma.Services
{
	public class UniversitiesService
	{
		IMongoDatabase db;
		IMongoCollection<University> universidades;
		public UniversitiesService(IConfiguration configuration) {
			MongoClient mongoClient = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
			db = mongoClient.GetDatabase("axiomadb");
			universidades = db.GetCollection<University>("universities");
		}

		public IEnumerable<University> Get()
		{
			return universidades.Find(a => true).ToList();
		}

		public University Get(string id)
		{
			return universidades.Find(a => id == a.Id).FirstOrDefault();
		}

		public University Create(University u)
		{
			universidades.InsertOne(u);
			return u;
		}

		

		public string CreateImage(string id, byte[] bytes)
		{
			var bucket = new GridFSBucket(db, new GridFSBucketOptions
			{
				BucketName = "UniversityImages",						
			});

			University universidad = universidades.Find(u => u.Id == id).FirstOrDefault();

			ObjectId bid = bucket.UploadFromBytes(universidad.Name, bytes);
			return bid.ToString();
		}

		public byte[] GetImage(string idstring)
		{
			var bucket = new GridFSBucket(db, new GridFSBucketOptions
			{
				BucketName = "UniversityImages",
			});
			ObjectId id = new ObjectId(idstring);
			byte[] bytes = bucket.DownloadAsBytes(id);
			return bytes;
		}

		public void Update(string id, University u)
		{
			universidades.ReplaceOne(uni => uni.Id == id, u);
		}


	}
}

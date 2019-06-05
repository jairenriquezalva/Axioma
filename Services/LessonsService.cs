using System;
using System.Collections.Generic;
using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class LessonsService
{
    IMongoCollection<Lesson> collection;

    public LessonsService(IConfiguration configuration){
        MongoClient mongoclient = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
        IMongoDatabase mongodb = mongoclient.GetDatabase("axiomadb");
        collection = mongodb.GetCollection<Lesson>("lessons");
    }
    public Lesson Create(Lesson lesson)
    {
        collection.InsertOne(lesson);
        return lesson;
    }

    public List<Lesson> Get(string moduleId){
        return collection.Find(less => less.Module == moduleId).ToList();
    }
}
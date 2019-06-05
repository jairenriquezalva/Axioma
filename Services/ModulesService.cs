using System;
using System.Collections.Generic;
using Axioma.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

public class ModulesService {

    IMongoCollection<Module> mongoCollection;

    public ModulesService(IConfiguration configuration){
        MongoClient mongoclient = new MongoClient(configuration.GetConnectionString("AxiomaDb"));
        IMongoDatabase mongodb = mongoclient.GetDatabase("axiomadb");
        mongoCollection = mongodb.GetCollection<Module>("modules");
    }

    public List<Module> Get(string courseId){
        return mongoCollection.Find(mod => mod.Course == courseId).ToList();
    }

    public Module Create(Module module)
    {
        mongoCollection.InsertOne(module);
        return module;
    }
}
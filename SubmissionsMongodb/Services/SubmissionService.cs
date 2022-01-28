using MongoDB.Driver;
using SubmissionsMongodb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubmissionsMongodb.Services
{
    public class SubmissionService
    {
        private readonly IMongoCollection<Submissions> _submissions;
        public SubmissionService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _submissions = database.GetCollection<Submissions>("Submissions");
        }

        public Submissions Create(Submissions submission)
        {
            _submissions.InsertOne(submission);
            return submission;
        }

        public IList<Submissions> Read() =>
            _submissions.Find<Submissions>(sub => true).ToList();

        public Submissions Find(String id) =>
            _submissions.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(Submissions submission) =>
            _submissions.ReplaceOne(sub => sub.Id == submission.Id, submission);

        public void Delete(String id) =>
            _submissions.DeleteOne(sub => sub.Id == id);

    }
}

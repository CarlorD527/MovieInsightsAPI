using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId MovieId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId UserId { get; set; }

        [BsonElement("reviewContent")]
        public string? ReviewContent { get; set;}

        [BsonElement("score")]
        public int Score { get; set; }

        [BsonElement("datedCreated")]
        public DateTime? DatedCreated { get; set; }

        [BsonElement("lastCreated")]
        public DateTime? LastModified { get; set; }

        [BsonElement("state")]
        public string? State { get; set; }
    }
}

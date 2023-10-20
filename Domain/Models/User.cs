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
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        [JsonPropertyName("username")]
        public string ?Username { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string ?Email { get; set; }

        [BsonElement("password")]
        [JsonPropertyName("password")]
        public string ?Password { get; set; }

        [BsonElement("state")]
        [JsonPropertyName("state")]
        public string? State { get; set; }

        [BsonElement("datedCreated")]
        [JsonPropertyName("datedCreated")]
        public DateTime? DatedCreated { get; set; }

        [BsonElement("lastModified")]
        [JsonPropertyName("lastModifiede")]
        public DateTime? LastModified { get;set; }


    }
}

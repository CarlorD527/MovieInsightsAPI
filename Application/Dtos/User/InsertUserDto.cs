using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class InsertUserDto
    {

        [BsonElement("username")]
        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [BsonElement("password")]
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

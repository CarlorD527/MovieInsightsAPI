using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Producer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("producerName")]
        public string? ProducerName { get; set; }

        [BsonElement("companyName")]
        public string? companyName { get; set; }
        
        [BsonElement("datedCreated")]
        public DateTime? DatedCreated { get; set; }
        [BsonElement("lastModified")]
        public DateTime? LastModified { get; set; }

        [BsonElement("state")]
        public string? State { get; set; }

    }
}


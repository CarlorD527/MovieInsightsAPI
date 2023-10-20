using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Producer
{
    public class InsertProducerDto
    {

        [BsonElement("producerName")]
        public string? ProducerName { get; set; }

        [BsonElement("companyName")]
        public string? companyName { get; set; }
    }
}

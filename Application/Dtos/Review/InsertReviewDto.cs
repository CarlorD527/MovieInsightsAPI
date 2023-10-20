using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Review
{
    public class InsertReviewDto
    {

        public string? MovieId { get; set; }
        public string? UserId { get; set; }

        [BsonElement("reviewContent")]
        public string? ReviewContent { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }
    }
}

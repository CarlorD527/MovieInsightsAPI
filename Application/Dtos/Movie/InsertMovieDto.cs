using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos.Movie
{
    public class InsertMovieDto
    {
        [BsonElement("movieTitle")]
        public string? MovieTitle { get; set; }

        [BsonElement("movieDescription")]
        public string? MovieDescription { get; set; }

        [BsonElement("movieImageUrl")]
        public string? MovieImageUrl { get; set; }

        [BsonElement("premiereYear")]
        public int? PremiereYear { get; set; }

        [BsonElement("premiereMonth")]
        public int? PremiereMonth { get; set; }

        [BsonElement("premiereDay")]
        public int? PremiereDay { get; set; }

        [BsonElement("premiereCountry")]
        public string? PremiereCountry { get; set; }

        [BsonElement("premiereBudget")]
        public double? Budget { get; set; }

        [BsonElement("Incomes")]
        public double? Incomes { get; set; }
    }
}

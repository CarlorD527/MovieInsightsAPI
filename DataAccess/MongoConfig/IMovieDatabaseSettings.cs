using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MongoConfig
{
    public interface IMovieDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MoviesCollectionName { get; set; }
    }
}

using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProducerCollection
    {

        Task<List<Producer>> GetAllProducer();

        Task<List<Producer>> GetAllProducerById(string id);

        Task InsertProducer(Producer producer);
        Task<bool> DeleteProducer(string id);
    }
}

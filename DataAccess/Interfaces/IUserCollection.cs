using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserCollection
    {
        Task<List<User>> GetAllUsers();

        Task<List<User>> GetUserById(string id);

        Task InsertUser(User User);
        Task<bool> DeleteUser(string id);
    }
}

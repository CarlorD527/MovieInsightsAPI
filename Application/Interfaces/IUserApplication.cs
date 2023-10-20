using Application.Commons;
using Application.Dtos.User;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<List<User>>> GetAllUsers();
        Task<BaseResponse<List<User>>> GetByIdUser(string id);
        Task<BaseResponse<bool>> addUser(InsertUserDto userDto);
        Task<BaseResponse<bool>> deleteUser(string id);
    }
}

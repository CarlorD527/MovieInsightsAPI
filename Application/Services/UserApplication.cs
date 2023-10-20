using Application.Commons;
using Application.Dtos.User;
using Application.Interfaces;
using Application.Validators;
using AutoMapper;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserApplication : IUserApplication
    {
        
        private readonly UserCollection _userCollection;

        private readonly IMapper _mapper;

        private readonly UserValidators _validatorRules;

        public UserApplication(UserCollection userCollection, UserValidators validatorRules, IMapper mapper)
        {
            _userCollection = userCollection;
            _mapper = mapper;
            _validatorRules = validatorRules;
        }

        public async Task<BaseResponse<bool>> addUser(InsertUserDto userDto)
        {
            var response = new BaseResponse<bool>();

            var validationResult = await _validatorRules.ValidateAsync(userDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.ValidationErrors = validationResult.Errors;

            }
            else
            {
                try
                {
                    byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                          password: userDto.Password!,
                          salt: salt,
                          prf: KeyDerivationPrf.HMACSHA256,
                          iterationCount: 100000,
                          numBytesRequested: 256 / 8));

                    //Mapeo del postDto al post
                    var user = _mapper.Map<User>(userDto);
                    user.DatedCreated = DateTime.UtcNow;
                    user.State = "Active";
                    user.Password = hashed;

                    // Intenta insertar la película
                    await _userCollection.InsertUser(user);

                    response.Data = false;
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;

                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { ex.Message };
                }
            }

            return response;
        }

        public async Task<BaseResponse<bool>> deleteUser(string id)
        {
            var response = new BaseResponse<bool>();

            try
            {
                bool isDeleted = await _userCollection.DeleteUser(id);

                if (isDeleted)
                {
                    response.IsSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudo eliminar el usuario." };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ApplicationErrors = new List<string> { "Error de la aplicación: " + ex.Message };
            }

            return response;
        }

        public async Task<BaseResponse<List<User>>> GetAllUsers()
        {
            var response = new BaseResponse<List<User>>();

            try
            {
                var users = await _userCollection.GetAllUsers();

                if (users is not null)
                {
                    response.IsSuccess = true;
                    response.Data = users;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Data = null;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudieron recuperar los usuarios." };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ApplicationErrors = new List<string> { "Error de la aplicación: " + ex.Message };
            }

            return response;
        }

        public async Task<BaseResponse<List<User>>> GetByIdUser(string id)
        {
            var response = new BaseResponse<List<User>>();

            try
            {
                var user = await _userCollection.GetUserById(id);

                if (user is not null)
                {
                    response.IsSuccess = true;
                    response.Data = user;
                    response.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.ApplicationErrors = new List<string> { "No se pudo encontró el usuario con el ID especificado." };
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ApplicationErrors = new List<string> { "Error de la aplicación: " + ex.Message };
            }

            return response;
        }
    }
}


using Application.Dtos.Movie;
using Application.Dtos.Review;
using Application.Dtos.User;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class MovieMappingProfile : Profile
    {
        public MovieMappingProfile()
        {
            CreateMap<Movie, InsertMovieDto>()
                .ReverseMap();
            CreateMap<User, InsertUserDto>() .ReverseMap();

            CreateMap<Review, InsertReviewDto>().ReverseMap();
        }

    }
}

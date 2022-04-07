﻿namespace BookStore_API.Helper;
using AutoMapper;
using BookStore_Models.DBModels;
using BookStore_Models.Requests;
using BookStore_Models.Responses;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<Users, AuthenticateResponse>();
        // RegisterRequest -> User
        CreateMap<RegisterRequest, Users>();
    }
}

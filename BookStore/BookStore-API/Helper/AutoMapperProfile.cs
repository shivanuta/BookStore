namespace BookStore_API.Helper;
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
        // AdminUser -> AdminAuthenticateResponse
        CreateMap<AdminUsers, AdminAuthenticateResponse>();
        // RegisterRequest -> User
        CreateMap<RegisterRequest, Users>();
        // Categories -> CategoriesResponse
        CreateMap<Categories, CategoriesResponse>();
        CreateMap<CategoryRequest, Categories>();
        CreateMap<Categories, CategoryRequest>();

        CreateMap<BookRequest, Books>()
                 .ForMember(dest => dest.BookImage, opt => opt.MapFrom(src => src.BookImage.FileName));

        CreateMap<Books, BooksResponse>();
        CreateMap<Books, BookRequest>()
            .ForMember(d => d.BookImage, opt => opt.Ignore())
            .ForMember(dest => dest.BookImageName, opt => opt.MapFrom(src => src.BookImage));
    }
}


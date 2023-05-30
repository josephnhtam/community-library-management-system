using AutoMapper;
using CLMS.API.DtoModels.Authors;
using CLMS.API.DtoModels.Books;
using CLMS.API.DtoModels.Patrons;
using CLMS.Application.Commands.Authors;
using CLMS.Application.Commands.Books;
using CLMS.Application.Commands.Patrons;

namespace CLMS.API.Profiles
{
    public class AutoMapperProfile : Profile {
        public AutoMapperProfile () {
            CreateMap<AddAuthorRequest, AddAuthorCommand>();
            CreateMap<AddBookRequest, AddBookCommand>();
            CreateMap<AddPatronRequest, AddPatronCommand>();
            CreateMap<BorrowBookRequest, BorrowBookCommand>();
        }
    }
}

using AutoMapper;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Mappers
{
    public sealed class SQLDatabaseProfile : Profile
    {
        public SQLDatabaseProfile()
        {
            CreateMap<AccountsDTO, Accounts>().ReverseMap();
            CreateMap<BookingsDTO, Bookings>().ReverseMap();
            CreateMap<ReviewsDTO, Reviews>().ReverseMap();
            CreateMap<LocationsDTO, Locations>().ReverseMap();
        }
    }
}

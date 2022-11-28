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
            CreateMap<AccountDTO, Account>().ReverseMap();
            CreateMap<BookingDTO, Booking>().ReverseMap();
            CreateMap<ReviewDTO, Review>().ReverseMap();
            CreateMap<LocationDTO, Location>().ReverseMap();
        }
    }
}

using AutoMapper;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Data.Interfaces;
using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Mappers
{
    public class CustomProfileWithEntities : Profile
    {
        public CustomProfileWithEntities(IBookingsRepository bookingsRepository, ILocationsRepository locationsRepository)
        {
            CreateMap<BookingWithEntitiesDTO, Booking>()
                .ReverseMap()
                .ForMember(x => x.Account, y => y.MapFrom(value => bookingsRepository.SearchByIdAsync(value.BookingId)))
                .ForMember(x => x.Location, y => y.MapFrom(value => locationsRepository.SearchByIdAsync(value.LocationId)));

        }
    }
}

using AutoMapper;
using FluentValidation;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Validators;

namespace SESFIR.Services.Model.Service
{
    public sealed class LocationsService : IServiceLocations
    {
        #region Fields
        private readonly ISQLDataFactory _repositories;
        private readonly IValidator<LocationDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public LocationsService(ISQLDataFactory repositories, IValidator<LocationDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(LocationDTO value)
        {
            var Locations = _mapper.Map<Location>(value);

            return await _repositories.LocationsRepository.DeleteAsync(Locations);
        }

        public async Task<List<LocationDTO>> GetAllAsync()
        {
            var Locations = await _repositories.LocationsRepository.GetAllAsync();

           // if (!Locations.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<LocationDTO>>(Locations);
        }

        public async Task<LocationDTO> InsertAsync(LocationDTO value)
        {
            if (await _repositories.LocationsRepository.FirstOrDefaultAsync(x => x.LocationX == value.LocationX && 
                                                                                 x.LocationY == value.LocationY &&
                                                                                 x.LocationName == value.LocationName) is not null)
                throw new ValidationException("Location already exists");

            await Validate.FluentValidate(_validator, value);

            var userDTO = await _repositories.LocationsRepository.InsertAsync(_mapper.Map<Location>(value));

            return _mapper.Map<LocationDTO>(userDTO);
        }

        public async Task<LocationDTO> SearchByIdAsync(int id)
        {
            var Location = await _repositories.LocationsRepository.SearchByIdAsync(id);

            return _mapper.Map<LocationDTO>(Location);
        }

        public async Task<LocationDTO> UpdateAsync(LocationDTO value)
        {
            if (await _repositories.LocationsRepository.SearchByIdAsync(value.LocationId) is null)
                throw new ValidationException("Location does not exists");

            await Validate.FluentValidate(_validator, value);

            var Location = await _repositories.LocationsRepository.UpdateAsync(_mapper.Map<Location>(value));

            return _mapper.Map<LocationDTO>(Location);
        }
        #endregion
    }
}

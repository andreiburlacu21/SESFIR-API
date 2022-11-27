using AutoMapper;
using FluentValidation;
using SESFIR.DataAccess.Data.Domains;
using SESFIR.DataAccess.Factory;
using SESFIR.DTOs;
using SESFIR.Services.Model.Service.Contracts;
using SESFIR.Validators;

namespace SESFIR.Services.Model.Service
{
    public sealed class ReviewsService : IServiceReviews
    {
        #region Fields
        private readonly ISQLDataFactory _repositories;
        private readonly IValidator<ReviewsDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReviewsService(ISQLDataFactory repositories, IValidator<ReviewsDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(ReviewsDTO value)
        {
            var Reviews = _mapper.Map<Reviews>(value);

            return await _repositories.ReviewsRepository.DeleteAsync(Reviews);
        }

        public async Task<List<ReviewsDTO>> GetAllAsync()
        {
            var Reviews = await _repositories.ReviewsRepository.GetAllAsync();

            if (!Reviews.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<ReviewsDTO>>(Reviews);
        }

        public async Task<ReviewsDTO> InsertAsync(ReviewsDTO value)
        {
            await Validate.FluentValidate(_validator, value);

            if (await _repositories.ReviewsRepository.FirstOrDefaultAsync(x => x.AccountId == value.AccountId &&
                                                                               x.Description.ToLower() == value.Description.ToLower()) is not null)
                throw new ValidationException("Review already exists");

            var userDTO = await _repositories.ReviewsRepository.InsertAsync(_mapper.Map<Reviews>(value));

            return _mapper.Map<ReviewsDTO>(userDTO);
        }

        public async Task<ReviewsDTO> SearchByIdAsync(int id)
        {
            var Review = await _repositories.ReviewsRepository.SearchByIdAsync(id);

            return _mapper.Map<ReviewsDTO>(Review);
        }

        public async Task<ReviewsDTO> UpdateAsync(ReviewsDTO value)
        {
            if (await _repositories.ReviewsRepository.SearchByIdAsync(value.ReviewId) is null)
                throw new ValidationException("Review does not exists");

            await Validate.FluentValidate(_validator, value);

            var Review = await _repositories.ReviewsRepository.UpdateAsync(_mapper.Map<Reviews>(value));

            return _mapper.Map<ReviewsDTO>(Review);
        }
        #endregion
    }
}

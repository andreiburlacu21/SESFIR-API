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
        private readonly IValidator<ReviewDTO> _validator;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public ReviewsService(ISQLDataFactory repositories, IValidator<ReviewDTO> validator, IMapper mapper)
        {
            _repositories = repositories;
            _validator = validator;
            _mapper = mapper;
        }
        #endregion

        #region Crud Methods     
        public async Task<bool> DeleteAsync(ReviewDTO value)
        {
            var Reviews = _mapper.Map<Review>(value);

            return await _repositories.ReviewsRepository.DeleteAsync(Reviews);
        }

        public async Task<List<ReviewDTO>> GetAllAsync()
        {
            var Reviews = await _repositories.ReviewsRepository.GetAllAsync();

           // if (!Reviews.Any()) throw new ValidationException("This table is empty");

            return _mapper.Map<List<ReviewDTO>>(Reviews);
        }

        public async Task<ReviewDTO> InsertAsync(ReviewDTO value)
        {
            await Validate.FluentValidate(_validator, value);

            if (await _repositories.ReviewsRepository.FirstOrDefaultAsync(x => x.AccountId == value.AccountId &&
                                                                               x.Description.ToLower() == value.Description.ToLower()) is not null)
                throw new ValidationException("Review already exists");

            var userDTO = await _repositories.ReviewsRepository.InsertAsync(_mapper.Map<Review>(value));

            return _mapper.Map<ReviewDTO>(userDTO);
        }

        public async Task<ReviewDTO> SearchByIdAsync(int id)
        {
            var Review = await _repositories.ReviewsRepository.SearchByIdAsync(id);

            return _mapper.Map<ReviewDTO>(Review);
        }

        public async Task<ReviewDTO> UpdateAsync(ReviewDTO value)
        {
            if (await _repositories.ReviewsRepository.SearchByIdAsync(value.ReviewId) is null)
                throw new ValidationException("Review does not exists");

            await Validate.FluentValidate(_validator, value);

            var Review = await _repositories.ReviewsRepository.UpdateAsync(_mapper.Map<Review>(value));

            return _mapper.Map<ReviewDTO>(Review);
        }
        #endregion

        public async Task<ReviewWithEntitiesDTO> GetReviewEnitityAsync(int id)
        {
            var review = await _repositories.BookingsRepository.SearchByIdAsync(id);

            var account = await _repositories.AccountsRepository.SearchByIdAsync(review.AccountId);

            var location = await _repositories.LocationsRepository.SearchByIdAsync(review.LocationId);

            var reviewWithEntitiesDTO = _mapper.Map<ReviewWithEntitiesDTO>(review);

            reviewWithEntitiesDTO.Account = _mapper.Map<AccountDTO>(account);

            reviewWithEntitiesDTO.Location = _mapper.Map<LocationDTO>(location);

            return reviewWithEntitiesDTO;
        }
    }
}

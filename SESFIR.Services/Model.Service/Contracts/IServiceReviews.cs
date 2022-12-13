using SESFIR.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Model.Service.Contracts
{
    public interface IServiceReviews : IService<ReviewDTO>
    {
        Task<ReviewWithEntitiesDTO> GetReviewEnitityAsync(int id);
        Task<List<ReviewWithEntitiesDTO>> GetMyReviewsEntitiesAsync(int id);

    }
}

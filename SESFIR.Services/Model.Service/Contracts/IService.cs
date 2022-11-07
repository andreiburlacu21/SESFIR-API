using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SESFIR.Services.Model.Service.Contracts
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> InsertAsync(T value);
        Task<T> UpdateAsync(T value);
        Task<T> SearchByIdAsync(int id);
        Task<bool> DeleteAsync(T value);
    }
}

﻿using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using SESFIR.DataAccess.ConnectionAccess;
using System.Linq;
using System.Linq.Expressions;


namespace SESFIR.DataAccess.Data.AbstractRepository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        #region Fields
        protected ISQLDataAccess sqlDataAccess;
        #endregion

        #region constructors
        public Repository(ISQLDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess ?? throw new NullReferenceException();
        }

        #endregion

        #region Methods
        public async Task<bool> DeleteAsync(T value)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            return await connection.DeleteAsync(value);
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

            return entities.ToList();
        }

        public async Task<T> InsertAsync(T value)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            var id = await connection.InsertAsync(value);

            return await connection.GetAsync<T>(id);
        }

        public async Task<T> SearchByIdAsync(int id)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            return await connection.GetAsync<T>(id);
        }

        public async Task<T?> UpdateAsync(T value)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            if (await connection.UpdateAsync(value))
                return value;

            return null;
        }

        public async Task<List<T>> GetEntitiesWhereAsync(Func<T, bool> expression)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

            return entities.Where(expression).ToList();
        }

        public async Task<T> FirstOrDefaultAsync(Func<T, bool> expression)
        {
            using var connection = new MySqlConnection(sqlDataAccess.Connection);

            var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

            return entities.FirstOrDefault(expression);
        }
        #endregion    
    }
}

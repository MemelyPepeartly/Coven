using Coven.Data.Entities;
using OpenAI_API.Embedding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Repository
{
    public interface IRepository
    {
        #region Create
        Task<User> CreateUser(string username, string worldAnvilUsername, string email);
        Task<bool> CreateEmbeddings(Guid userId, string characterSet, float[] vectors);
        #endregion

        #region Read
        Task<User> GetUser(Guid userId);
        Task<List<User>> GetUsers();
        Task<List<WacharacterSet>> GetUserEmbeddings(Guid userId);
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

        Task<bool> SaveAsync();
    }
}

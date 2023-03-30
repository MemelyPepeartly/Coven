using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tardigrade.Data.Entities;

namespace Tardigrade.Data.Repository
{
    public interface IRepository
    {
        #region Create
        /// <summary>
        /// Creates a new client and returns the new client object from the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Client> CreateClient(string username, string email);
        /// <summary>
        /// Creates a new character and returns the new character object from the database
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterName"></param>
        /// <returns></returns>
        Task<Character> CreateCharacter(Guid userId, string characterName);
        #endregion

        #region Read
        /// <summary>
        /// Gets a list of all client objects
        /// </summary>
        /// <returns></returns>
        Task<List<Client>> GetClients();
        /// <summary>
        /// Returns a client by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Client> GetClient(Guid userId);
        /// <summary>
        /// Returns a list of all characters in the database
        /// </summary>
        /// <returns></returns>
        Task<List<Character>> GetAllCharacters();
        /// <summary>
        /// Gets a specific character by characterId
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        Task<Character> GetCharacter(Guid characterId);
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coven.Data.Entities;

namespace Coven.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly CovenDbContext CovenContext;

        public Repository(CovenDbContext _CovenContext)
        {
            CovenContext = _CovenContext ?? throw new ArgumentNullException(nameof(_CovenContext));
        }

        #region Create
        public async Task<Client> CreateClient(string username, string email)
        {
            Guid newUserId = Guid.NewGuid();
            await CovenContext.Clients.AddAsync(new Client()
            {
                UserId = newUserId,
                Username = username,
                Email = email
            });
            await CovenContext.SaveChangesAsync();

            return await GetClient(newUserId);
        }
        public async Task<Character> CreateCharacter(Guid userId, string characterName)
        {
            Guid characterId = Guid.NewGuid();
            await CovenContext.Characters.AddAsync(new Character()
            {
                UserId = userId,
                CharacterId = characterId,
                CharacterName = characterName
            });
            await CovenContext.SaveChangesAsync();

            return await GetCharacter(characterId);
        }
        #endregion

        #region Read
        public async Task<List<Client>> GetClients()
        {
            return await CovenContext.Clients.AsNoTracking()
                .ToListAsync();
        }
        public async Task<Client> GetClient(Guid userId)
        {
            return await CovenContext.Clients.AsNoTracking()
                .FirstAsync(c => c.UserId == userId);
        }
        public async Task<List<Character>> GetAllCharacters()
        {
            return await CovenContext.Characters.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Character> GetCharacter(Guid characterId)
        {
            return await CovenContext.Characters.AsNoTracking()
                .FirstAsync(c => c.CharacterId == characterId);
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}

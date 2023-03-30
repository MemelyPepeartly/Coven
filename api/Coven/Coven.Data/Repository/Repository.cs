using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tardigrade.Data.Entities;

namespace Tardigrade.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly TardigradeDbContext TardigradeContext;

        public Repository(TardigradeDbContext _TardigradeContext)
        {
            TardigradeContext = _TardigradeContext ?? throw new ArgumentNullException(nameof(_TardigradeContext));
        }

        #region Create
        public async Task<Client> CreateClient(string username, string email)
        {
            Guid newUserId = Guid.NewGuid();
            await TardigradeContext.Clients.AddAsync(new Client()
            {
                UserId = newUserId,
                Username = username,
                Email = email
            });
            await TardigradeContext.SaveChangesAsync();

            return await GetClient(newUserId);
        }
        public async Task<Character> CreateCharacter(Guid userId, string characterName)
        {
            Guid characterId = Guid.NewGuid();
            await TardigradeContext.Characters.AddAsync(new Character()
            {
                UserId = userId,
                CharacterId = characterId,
                CharacterName = characterName
            });
            await TardigradeContext.SaveChangesAsync();

            return await GetCharacter(characterId);
        }
        #endregion

        #region Read
        public async Task<List<Client>> GetClients()
        {
            return await TardigradeContext.Clients.AsNoTracking()
                .ToListAsync();
        }
        public async Task<Client> GetClient(Guid userId)
        {
            return await TardigradeContext.Clients.AsNoTracking()
                .FirstAsync(c => c.UserId == userId);
        }
        public async Task<List<Character>> GetAllCharacters()
        {
            return await TardigradeContext.Characters.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Character> GetCharacter(Guid characterId)
        {
            return await TardigradeContext.Characters.AsNoTracking()
                .FirstAsync(c => c.CharacterId == characterId);
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}

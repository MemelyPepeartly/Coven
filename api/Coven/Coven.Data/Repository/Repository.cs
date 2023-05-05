using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Microsoft.EntityFrameworkCore;
using OpenAI_API.Embedding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coven.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly CovenContext CovenContext;

        public Repository(CovenContext _CovenContext)
        {
            CovenContext = _CovenContext ?? throw new ArgumentNullException(nameof(_CovenContext));
        }

        #region Create

        public async Task<User> CreateUser(string username, string worldAnvilUsername, string email)
        {
            try
            {
                Guid userId = Guid.NewGuid();
                await CovenContext.Users.AddAsync(new User()
                {
                    UserId = userId,
                    Username = username,
                    WorldAnvilUsername = worldAnvilUsername,
                    Email = email
                });

                await SaveAsync();

                return await CovenContext.Users.FirstAsync(x => x.UserId == userId);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateEmbeddings(Guid userId, string characterSet, float[] vectors)
        {
            try
            {
                Guid characterId = Guid.NewGuid();

                await CovenContext.WacharacterSets.AddAsync(new WacharacterSet()
                {
                    CharacterSetId = characterId,
                    CharacterSet = characterSet,
                    UserId = userId
                });
                await CovenContext.SaveChangesAsync();

                byte[] byteArray = new byte[vectors.Length * sizeof(float)];
                Buffer.BlockCopy(vectors, 0, byteArray, 0, byteArray.Length);

                await CovenContext.Waembeddings.AddAsync(new Waembedding()
                {
                    EmbeddingId = Guid.NewGuid(),
                    CharacterSetId = characterId,
                    Vector = byteArray
                });

                await CovenContext.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Read
        public async Task<User> GetUser(Guid userId)
        {
            try
            {
                return await CovenContext.Users
                    .Include(x => x.WacharacterSets)
                        .ThenInclude(x => x.Waembeddings)
                    .FirstAsync(x => x.UserId == userId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await CovenContext.Users
                    .Include(x => x.WacharacterSets)
                        .ThenInclude(x => x.Waembeddings)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserDTO>> GetDTOUsers()
        {
            try
            {
                return await CovenContext.Users
                    .Select(u => new UserDTO()
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Email = u.Email,
                        WorldAnvilUsername = u.WorldAnvilUsername
                    })
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<WACharacterSetDTO>> GetUserEmbeddings(Guid userId)
        {
            try
            {
                return await CovenContext.WacharacterSets
                    .Include(x => x.Waembeddings)
                    .Where(x => x.UserId == userId)
                        .SelectMany(x => x.Waembeddings)
                        .Select(e => new WACharacterSetDTO()
                        {
                            characterSet = e.CharacterSet.CharacterSet,
                            vectors = ByteArrayToFloatArray(e.Vector)
                        })
                        .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion

        public async Task<bool> SaveAsync()
        {
            try
            {
                return (await CovenContext.SaveChangesAsync() > 0);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public float[] ByteArrayToFloatArray(byte[] byteArray)
        {
            float[] floatArray = new float[byteArray.Length / sizeof(float)];
            Buffer.BlockCopy(byteArray, 0, floatArray, 0, byteArray.Length);
            return floatArray;
        }
    }
}

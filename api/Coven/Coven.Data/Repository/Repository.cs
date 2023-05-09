using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Data.Pinecone;
using Coven.Logic.Meta_Objects;
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

        public async Task<bool> CreatePineconeMetadataEntry(Guid userId, Guid pineconeIdentifier, ArticleMetadata metadata)
        {
            try
            {
                await CovenContext.PineconeVectorMetadata.AddAsync(new PineconeVectorMetadatum()
                {
                    EntryId = pineconeIdentifier,
                    WorldId = Guid.Parse(metadata.WorldId),
                    ArticleId = Guid.Parse(metadata.ArticleId),
                    CharacterString = metadata.CharacterString,
                });

                return await SaveAsync();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreatePineconeMetadataEntries(Guid userId, List<Embedding> embeddingsData)
        {
            try
            {
                foreach (Embedding embedding in embeddingsData)
                {
                    await CovenContext.PineconeVectorMetadata.AddAsync(new PineconeVectorMetadatum()
                    {
                        EntryId = Guid.Parse(embedding.identifier),
                        WorldId = Guid.Parse(embedding.metadata.WorldId),
                        ArticleId = Guid.Parse(embedding.metadata.ArticleId),
                        CharacterString = embedding.metadata.CharacterString,
                    });
                }
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> CreateWorld(Guid userId, WorldSegment WAWorldSegment)
        {
            await CovenContext.Worlds.AddAsync(new World()
            {
                WorldId = WAWorldSegment.id,
                UserId = userId,
                WorldName = WAWorldSegment.name
            });

            return await SaveAsync();
        }

        public async Task<bool> CreateWorlds(Guid userId, List<WorldSegment> WAWorldSegments)
        {
            foreach (WorldSegment worldSegment in WAWorldSegments)
            {
                await CovenContext.Worlds.AddAsync(new World()
                {
                    WorldId = worldSegment.id,
                    UserId = userId,
                    WorldName = worldSegment.name
                });
            }
            return await SaveAsync();
        }

        #endregion

        #region Read
        public async Task<User> GetUser(Guid userId)
        {
            try
            {
                return await CovenContext.Users
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

        public async Task<List<PineconeVectorMetadatum>> GetWorldPineconeMetadatum(Guid worldId)
        {
            try
            {
                return await CovenContext.PineconeVectorMetadata
                    .Where(m => m.WorldId == worldId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<World>> GetWorlds(Guid userId)
        {
            try
            {
                return await CovenContext.Worlds
                    .Where(w => w.UserId == userId)
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
    }
}

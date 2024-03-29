﻿using Coven.Data.DTO.AI;
using Coven.Data.Entities;
using Coven.Data.Meta_Objects;
using Coven.Data.Pinecone;
using Coven.Data.Repository.Models;
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
            // if a world with the same id doesn't exist, create it
            if(!(await CovenContext.Worlds.AnyAsync(w => w.WorldId == WAWorldSegment.id)))
            {
                await CovenContext.Worlds.AddAsync(new World()
                {
                    WorldId = WAWorldSegment.id,
                    UserId = userId,
                    WorldName = WAWorldSegment.name
                });

                return await SaveAsync();
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CreateWorlds(Guid userId, List<WorldSegment> WAWorldSegments)
        {
            List<World> worlds = await CovenContext.Worlds.Where(w => w.UserId == userId).ToListAsync();
            foreach (WorldSegment worldSegment in WAWorldSegments)
            {
                // if a world with the same id doesn't exist, create it
                if (!worlds.Any(w => w.WorldId == worldSegment.id))
                {
                    await CovenContext.Worlds.AddAsync(new World()
                    {
                        WorldId = worldSegment.id,
                        UserId = userId,
                        WorldName = worldSegment.name
                    });
                }
            }
            return await SaveAsync();
        }

        public async Task<bool> CreateWorldContentEntries(List<IndexTableModel> models)
        {
            // Begin a transaction
            using (var transaction = await CovenContext.Database.BeginTransactionAsync())
            {
                try
                {
                    List<WorldContent> newEntities = models.Select(m => new WorldContent()
                    {
                        WorldContentId = Guid.NewGuid(),
                        ArticleTitle = m.articleTitle,
                        WorldAnvilArticleType = m.worldAnvilArticleType,
                        Author = m.author,
                        Content = m.content,
                        ArticleId = m.articleId,
                        WorldId = m.worldId
                    }).ToList();

                    await CovenContext.WorldContents.AddRangeAsync(newEntities);

                    // Attempt to save changes to the database
                    var saveResult = await SaveAsync();

                    // If the save was successful, commit the transaction
                    if (saveResult)
                    {
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        // If the save failed, the transaction will be rolled back implicitly
                        // when the using block is exited and transaction is disposed
                    }

                    return saveResult;
                }
                catch (Exception)
                {
                    // In case of an exception, the transaction will be rolled back implicitly
                    // when the using block is exited and transaction is disposed
                    throw; // Re-throw the exception to handle it further up the call stack
                }
            }
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
                        WorldAnvilUsername = u.WorldAnvilUsername,
                        Worlds = u.Worlds.Select(w => new WorldDTO()
                        {
                            WorldId = w.WorldId,
                            WorldName = w.WorldName,
                            UserId = w.UserId
                        }).ToList()
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

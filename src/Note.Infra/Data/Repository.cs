﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Note.Core.Data;
using Note.Core.Entities;
using Note.Core.Services;

namespace Note.Infra.Data
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly IConfiguration _configuration;
        protected readonly ICurrentUserInfo _userService;
        protected readonly DocumentDBConnection _db;

        public Repository(IConfiguration configuration, ICurrentUserInfo userService)
        {
            _configuration = configuration;
            _userService = userService;

            var cosmosDB = _configuration["CosmosDB:"];
            _db = new DocumentDBConnection(
                _configuration[Constants.CosmosDB.Endpoint],
                _configuration[Constants.CosmosDB.Key],
                _configuration[Constants.CosmosDB.DatabaseId],
                _configuration[Constants.CosmosDB.CollectionId]);
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await _db.Client.ReadDocumentAsync(_db.CreateUri(id));
                return JsonConvert.DeserializeObject<T>(document.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync()
        {
            IDocumentQuery<T> query = _db.Client.CreateDocumentQuery<T>(
                _db.CreateUri(),
                new FeedOptions { MaxItemCount = -1 })
                .Where(o => o.Type == typeof(T).Name)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = _db.Client.CreateDocumentQuery<T>(
                _db.CreateUri(),
                new FeedOptions { MaxItemCount = -1 })
                .Where(o => o.Type == typeof(T).Name)
                .Where(predicate)
                .AsDocumentQuery();

            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T>());
            }

            return results;
        }

        public async Task<T> CreateItemAsync(T item)
        {
            if (item is Entity)
            {
                var entity = item as Entity;
                entity.Type = typeof(T).Name;
                entity.CreatedAt = DateTime.Now;
                entity.CreatedBy = _userService.GetName();
            }
            var result = await _db.Client.CreateDocumentAsync(_db.CreateUri(), item);
            if (result.StatusCode == HttpStatusCode.Created)
            {
                return JsonConvert.DeserializeObject<T>(result.Resource.ToString());
            }
            throw new Exception($"Error creating resource: StatusCode = {result.StatusCode}");
        }

        public async Task<T> UpdateItemAsync(string id, T item)
        {
            if (item is Entity)
            {
                var entity = item as Entity;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = _userService.GetName();
            }
            var response = await _db.Client.ReplaceDocumentAsync(_db.CreateUri(id), item);
            return JsonConvert.DeserializeObject<T>(response.Resource.ToString());
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var response = await _db.Client.DeleteDocumentAsync(_db.CreateUri(id));
            return true;
        }
    }
}
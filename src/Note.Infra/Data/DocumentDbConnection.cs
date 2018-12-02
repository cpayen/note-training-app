using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Note.Core.Enums;
using Note.Core.Helpers;
using Note.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Note.Infra.Data
{
    public class DocumentDBConnection
    {
        private static string Endpoint;
        private static string Key;
        private static string DatabaseId;
        private static string CollectionId;

        private static DocumentClient _client { get; set; }

        public DocumentDBConnection(string endpoint, string key, string databaseId, string collectionId)
        {
            Endpoint = endpoint;
            Key = key;
            DatabaseId = databaseId;
            CollectionId = collectionId;

            _client = new DocumentClient(new Uri(Endpoint), Key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        public DocumentClient Client
        {
            get { return _client; }
            private set { }
        }

        public Uri CreateUri(string id = null)
        {
            return
                string.IsNullOrEmpty(id) ?
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId) :
                UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id);
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await _client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await _client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });

                    // Create super admin
                    // TODO: Externalize DB seed?
                    string salt = SecurityHelper.GetNewSalt();
                    AppUser admin = new AppUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        Type = typeof(AppUser).Name,
                        Name = "Admin",
                        Email = "admin@note.com",
                        Role = AppUserRole.Admin,
                        CreatedAt = DateTime.Now,
                        Password = SecurityHelper.EncryptPassword("Pa$$w0rd", salt),
                        Salt = salt
                    };

                    await Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), admin);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

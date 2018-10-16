using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Note.Core.Enums;
using Note.Core.Helpers;
using Note.Core.Models;
using System;
using System.Threading.Tasks;

namespace Note.Core
{
    public class DocumentDBConnection
    {
        private static readonly string Endpoint = "https://localhost:8081/";
        private static readonly string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private static readonly string DatabaseId = "note-db-dev";
        private static readonly string CollectionId = "note-db-coll";

        private static DocumentClient _client { get; set; }

        public DocumentDBConnection()
        {
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

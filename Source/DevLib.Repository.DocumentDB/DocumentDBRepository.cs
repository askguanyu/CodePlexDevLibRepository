namespace DevLib.Repository.DocumentDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.Documents;
    using Microsoft.Azure.Documents.Client;

    public class DocumentDBRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private const string DefaultCollectionSuffix = "Collection";
        private readonly string _databaseId;
        private readonly string _collectionId;
        private readonly DocumentClient _documentClient;

        public DocumentDBRepository()
        {
            this._documentClient = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"], new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await this._documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await this._documentClient.CreateDatabaseAsync(new Database { Id = DatabaseId });
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
                await this._documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await this._documentClient.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        public Action<string> Log
        {
            get;
            set;
        }

        public bool ThrowOnError
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int? Timeout
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> InsertOrUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity InsertOrUpdate(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Select(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> SelectAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}

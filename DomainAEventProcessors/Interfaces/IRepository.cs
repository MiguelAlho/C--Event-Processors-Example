namespace DomainAEventProcessors.Interfaces
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Get the entity object from the repository, by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(string id);

        /// <summary>
        /// Persist the enity object to the storage
        /// </summary>
        /// <param name="entity">The object to store</param>
        void Save(TEntity entity);
    }
}

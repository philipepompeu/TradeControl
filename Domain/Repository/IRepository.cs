namespace TradeControl.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task SaveChanges();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

namespace FileStorage.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        void Update(T entity);
        void Delete(T entity);
    }
}

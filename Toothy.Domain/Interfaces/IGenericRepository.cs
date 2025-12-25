namespace Toothy.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T?> ObtenerPorIdAsync(int id);
        Task<T> AgregarAsync(T entity);
        Task ActualizarAsync(T entity);
        Task EliminarAsync(int id);
    }
}

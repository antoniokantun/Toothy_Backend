namespace Toothy.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entity);
        Task ActualizarAsync(T entity);
        Task EliminarAsync(int id);
    }
}

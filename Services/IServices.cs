using OdinApi.Models;

namespace OdinApi.Services
{
    public interface ISateliteService
    {
        Task<IEnumerable<Satelite>> GetAllAsync();
        Task<Satelite?> GetByIdAsync(int id);
        Task<Satelite> CreateAsync(Satelite satelite);
        Task<Satelite> UpdateAsync(int id, Satelite satelite);
        Task<bool> DeleteAsync(int id);
    }

    public interface IOperadorService
    {
        Task<IEnumerable<Operador>> GetAllAsync();
        Task<Operador?> GetByIdAsync(int id);
        Task<Operador> CreateAsync(Operador operador);
        Task<Operador> UpdateAsync(int id, Operador operador);
        Task<bool> DeleteAsync(int id);
    }

    public interface IDebitoService
    {
        Task<IEnumerable<Detrito>> GetAllAsync();
        Task<Detrito?> GetByIdAsync(int id);
        Task<Detrito> CreateAsync(Detrito detrito);
        Task<Detrito> UpdateAsync(int id, Detrito detrito);
        Task<bool> DeleteAsync(int id);
    }
}

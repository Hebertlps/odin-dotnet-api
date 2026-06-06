using Microsoft.EntityFrameworkCore;
using OdinApi.Data;
using OdinApi.Models;

namespace OdinApi.Services
{
    public class SateliteService : ISateliteService
    {
        private readonly OdinDbContext _context;

        public SateliteService(OdinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Satelite>> GetAllAsync()
        {
            return await _context.Satelites
                .Include(s => s.Operador)
                .Include(s => s.Manobras)
                .Include(s => s.Alertas)
                .ToListAsync();
        }

        public async Task<Satelite?> GetByIdAsync(int id)
        {
            return await _context.Satelites
                .Include(s => s.Operador)
                .Include(s => s.Manobras)
                .Include(s => s.Alertas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Satelite> CreateAsync(Satelite satelite)
        {
            _context.Satelites.Add(satelite);
            await _context.SaveChangesAsync();
            return satelite;
        }

        public async Task<Satelite> UpdateAsync(int id, Satelite satelite)
        {
            var existing = await _context.Satelites.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Satélite com ID {id} não encontrado");

            existing.Nome = satelite.Nome;
            existing.CombustivelAtual = satelite.CombustivelAtual;
            existing.StatusOperacional = satelite.StatusOperacional;
            existing.DataLancamento = satelite.DataLancamento;
            existing.OperadorId = satelite.OperadorId;

            _context.Satelites.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var satelite = await _context.Satelites.FindAsync(id);
            if (satelite == null)
                return false;

            _context.Satelites.Remove(satelite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

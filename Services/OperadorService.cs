using Microsoft.EntityFrameworkCore;
using OdinApi.Data;
using OdinApi.Models;

namespace OdinApi.Services
{
    public class OperadorService : IOperadorService
    {
        private readonly OdinDbContext _context;

        public OperadorService(OdinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Operador>> GetAllAsync()
        {
            return await _context.Operadores
                .Include(o => o.Satelites)
                .Include(o => o.Detritos)
                .Include(o => o.Manobras)
                .ToListAsync();
        }

        public async Task<Operador?> GetByIdAsync(int id)
        {
            return await _context.Operadores
                .Include(o => o.Satelites)
                .Include(o => o.Detritos)
                .Include(o => o.Manobras)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Operador> CreateAsync(Operador operador)
        {
            _context.Operadores.Add(operador);
            await _context.SaveChangesAsync();
            return operador;
        }

        public async Task<Operador> UpdateAsync(int id, Operador operador)
        {
            var existing = await _context.Operadores.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Operador com ID {id} não encontrado");

            existing.Nome = operador.Nome;
            existing.Email = operador.Email;
            existing.NivelAcesso = operador.NivelAcesso;

            _context.Operadores.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
                return false;

            _context.Operadores.Remove(operador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

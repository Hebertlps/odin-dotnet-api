using Microsoft.EntityFrameworkCore;
using OdinApi.Data;
using OdinApi.Models;

namespace OdinApi.Services
{
    public class DebitoService : IDebitoService
    {
        private readonly OdinDbContext _context;

        public DebitoService(OdinDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Detrito>> GetAllAsync()
        {
            return await _context.Detritos
                .Include(d => d.Operador)
                .Include(d => d.Alertas)
                .ToListAsync();
        }

        public async Task<Detrito?> GetByIdAsync(int id)
        {
            return await _context.Detritos
                .Include(d => d.Operador)
                .Include(d => d.Alertas)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Detrito> CreateAsync(Detrito detrito)
        {
            _context.Detritos.Add(detrito);
            await _context.SaveChangesAsync();
            return detrito;
        }

        public async Task<Detrito> UpdateAsync(int id, Detrito detrito)
        {
            var existing = await _context.Detritos.FindAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Detrito com ID {id} não encontrado");

            existing.Identificacao = detrito.Identificacao;
            existing.Latitude = detrito.Latitude;
            existing.Longitude = detrito.Longitude;
            existing.Altitude = detrito.Altitude;
            existing.Velocidade = detrito.Velocidade;
            existing.NivelRisco = detrito.NivelRisco;
            existing.OperadorId = detrito.OperadorId;

            _context.Detritos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var detrito = await _context.Detritos.FindAsync(id);
            if (detrito == null)
                return false;

            _context.Detritos.Remove(detrito);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class PlayerService
    {
        private readonly AppDbContext _context;

        public PlayerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAllAsync() => 
            await _context.Players.ToListAsync();

        public async Task<Player?> GetByIdAsync(int id) =>
            await _context.Players.FindAsync(id);

        public async Task CreateAsync(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Player updatedPlayer)
        {
            if (id != updatedPlayer.Id) return;

            _context.Entry(updatedPlayer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player is null) return;

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }
    }
}

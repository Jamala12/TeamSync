using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Player>>> GetAll()
        {
            return await _context.Players.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetById(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return player;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> Create(Player newPlayer)
        {
            _context.Players.Add(newPlayer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = newPlayer.Id }, newPlayer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Player updatedPlayer)
        {
            if (id != updatedPlayer.Id) return BadRequest();
            _context.Entry(updatedPlayer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

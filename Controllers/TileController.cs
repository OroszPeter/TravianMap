using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravianMapWebAPI.DBContext;
using TravianMapWebAPI.Entities;

namespace TravianMapWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TileController(ApplicationDbContext context) : ControllerBase
    {
        // Térkép generálása
        [HttpPost("generate-empty-map")]
        public async Task<IActionResult> GenerateMap()
        {
            const int mapSize = 101;
            const int minCoord = -50;
            const int maxCoord = 50;

            
            bool mapExists = await context.Tiles.AnyAsync();

            if (mapExists)
            {
                return Conflict("A térkép már létezik.");
            }

            var tiles = new List<Tile>();

            
            for (int x = minCoord; x <= maxCoord; x++)
            {
                for (int y = minCoord; y <= maxCoord; y++)
                {
                    var tile = new Tile
                    {
                        xCoord = x,
                        yCoord = y,
                        TileType = TileType.Empty 
                    };

                    tiles.Add(tile);
                }
            }

            
            context.Tiles.AddRange(tiles); 
            await context.SaveChangesAsync();

            return Ok("Üres térkép sikeresen létrehozva.");
        }

        // Adott csempe módosítása 
        [HttpPut("update-tile")]
        public async Task<IActionResult> UpdateTileType(int xCoord, int yCoord, TileType newTileType)
        {

            var tile = await context.Tiles
                .FirstOrDefaultAsync(t => t.xCoord == xCoord && t.yCoord == yCoord);


            if (tile == null)
            {
                return NotFound("A csempe nem létezik.");
            }


            tile.TileType = newTileType;


            context.Tiles.Update(tile);
            await context.SaveChangesAsync();

            return Ok("Sikeres módosítás.");
        }
        // Adott csempe lekérdezése
        [HttpGet]
        public async Task<IActionResult> GetTile(int xCoord, int yCoord)
        {
            var tile = await context.Tiles
                .FirstOrDefaultAsync(t => t.xCoord == xCoord && t.yCoord == yCoord);


            if (tile == null)
            {
                return NotFound("A csempe nem létezik.");
            }

            return Ok(tile);
        }

        // 7x7 megjelenítéshez szükséges metódus
        [HttpGet("get-tiles")]
        public async Task<IActionResult> GetTiles(int startX, int startY, int endX, int endY)
        {

                var tiles = await context.Tiles
                    .Where(t => t.xCoord >= startX && t.xCoord <= endX && t.yCoord >= startY && t.yCoord <= endY)
                    .ToListAsync();

                return Ok(tiles);
            
        }






    }
}

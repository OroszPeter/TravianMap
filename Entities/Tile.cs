namespace TravianMapWebAPI.Entities
{
    public class Tile
    {
        public int Id { get; set; }
        public int xCoord { get; set; }
        public int yCoord { get; set; }
        public TileType TileType { get; set; }
    }
}

using Godot;
[GlobalClass]
public partial class DungeonTileInfo : Resource
{
	[Export] private string tileName;
	[Export] private Texture2D tileTexture;
	[Export] private DungeonCellType dungeonCellType;
	
	public string TileName => tileName;	
	public Texture2D TileTexture => tileTexture;
	public DungeonCellType DungeonCellType=> dungeonCellType;
}

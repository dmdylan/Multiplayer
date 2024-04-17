using Godot;
[GlobalClass]
public partial class DungeonTile : Resource
{
	[Export] private string tileName;
	[Export] private Texture2D tileTexture;
	
	public string TileName => tileName;	
	public Texture2D TileTexture => tileTexture;
}

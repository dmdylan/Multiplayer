using Godot;
using System;

[GlobalClass]
public partial class DungeonTile : Resource
{
	[Export] private string tileName;
	[Export] private Color tileColor;
	
	public string TileName => tileName;	
	public Color TileColor => tileColor;
}

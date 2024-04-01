using Godot;
using System;

[GlobalClass]
public partial class DungeonTile : Resource
{
	[Export] private Color tileColor;
	
	public Color TileColor => tileColor;
}

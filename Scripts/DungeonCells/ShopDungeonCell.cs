using System.Collections.Generic;
using Godot;

public class ShopDungeonCell : DungeonCell
{
	public List<Item> Items { get; private set; }
	
	public ShopDungeonCell(DungeonCellType dungeonCellType, Vector2I gridLocation) : base(dungeonCellType, gridLocation)
	{
	}
}

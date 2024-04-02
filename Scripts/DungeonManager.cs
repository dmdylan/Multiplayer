using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using Godot;

public partial class DungeonManager : Node
{
	private static DungeonManager instance;
	public static DungeonManager Instance => instance;
	
	[Export] private Vector2I gridSize;
	[Export] private DungeonTile[] dungeonTiles;
	
	private DungeonTile[,] dungeonGrid;
	public DungeonTile[,] DungeonGrid => dungeonGrid;


	public override void _EnterTree()
	{		
		if(instance != null)
			QueueFree();
		else
			instance = this;
	}

	public override void _Ready()
	{
		base._Ready();
		
		dungeonGrid = new DungeonTile[gridSize.X, gridSize.Y];
	}

	public void PopulateDungeonGrid()
	{
		//Loop through grid Y
		for (int i = 0; i < gridSize.Y; i++)
		{
			//Loop through grid X
			for (int j = 0; j < gridSize.X; j++)
			{
				dungeonGrid[j, i] = dungeonTiles[GD.Randi() % dungeonTiles.Length];
				// GD.Print($"Dungeon position {j},{i}: {dungeonGrid[j,i].TileName}");
			}
		}
	}
}
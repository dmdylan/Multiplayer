using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class DungeonManager : Node
{
	private static DungeonManager instance;
	public static DungeonManager Instance => instance;
	
	[Export] private Vector2I gridSize;
	[Export] private DungeonTileInfo[] dungeonTiles;
	[Export] private DungeonTileInfo bossTile;
	[Export] private EntityDatabase enemyEntityDatabase;

	public DungeonCell[][] DungeonGrid { get; private set; }

	private int currentTier = 0;

	public override void _EnterTree()
	{		
		if(instance != null)
			QueueFree();
		else
			instance = this;
	}

	public override void _Ready()
	{
		DungeonGrid = new DungeonCell[gridSize.X][];
		
		for (int i = 0; i < gridSize.X; i++)
		{
			if(i == gridSize.X - 1)
				DungeonGrid[i] = new DungeonCell[1];
			else
				DungeonGrid[i] = new DungeonCell[gridSize.Y];
		}
	}

	public void PopulateDungeonGrid()
	{
		Array values = Enum.GetValues(typeof(DungeonCellType));
		
		foreach (var item in values)
		{
			GD.Print(item);
		}
		
		//Loop through grid Y
		for (int i = 0; i < DungeonGrid.Length; i++)
		{
			//Loop through grid X
			for (int j = 0; j < DungeonGrid[i].Length; j++)
			{
				if(i == DungeonGrid.Length - 1)
				{
					DungeonGrid[i][j] = new DungeonCell(DungeonCellType.Boss, new Vector2I(i,j));
					GD.Print($"Dungeon position {i},{j}: {DungeonGrid[i][j].DungeonCellType}");
					continue;
				}
				
				DungeonCell dungeonCell = new((DungeonCellType)values.GetValue(GD.Randi() % values.Length), new Vector2I(i,j));
				
				DungeonGrid[i][j] = dungeonCell;
				GD.Print($"Dungeon position {i},{j}: {DungeonGrid[i][j].DungeonCellType}");	
			}
		}
		
		SetupDungeonTiles();		
	}
	
	//Select enemies at certain tier threshold
	//Assign number of enemies in encounter (could be based on set of parameters)
	//Assign selected enemies to specific encounter
	public void SetupDungeonTiles()
	{
		List<EntityInfo> entities = enemyEntityDatabase.Entities
									.Where(x => x.Tier == currentTier - 1
									|| x.Tier ==  currentTier
									|| x.Tier == currentTier + 1).ToList();
		
		
	}
}
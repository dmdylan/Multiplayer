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
		if (instance != null)
			QueueFree();
		else
			instance = this;
	}

	public override void _Ready()
	{
		DungeonGrid = new DungeonCell[gridSize.X][];

		for (int i = 0; i < gridSize.X; i++)
		{
			if (i == gridSize.X - 1)
				DungeonGrid[i] = new DungeonCell[1];
			else
				DungeonGrid[i] = new DungeonCell[gridSize.Y];
		}
	}

	public void PopulateDungeonGrid()
	{	
		//Loop through grid Y
		for (int i = 0; i < DungeonGrid.Length; i++)
		{
			//Loop through grid X
			for (int j = 0; j < DungeonGrid[i].Length; j++)
			{
				DungeonCellType dungeonCellType = GetRandomDungeonCell();
				
				if (i == DungeonGrid.Length - 1)
				{
					dungeonCellType = DungeonCellType.Boss;
				}
				
				DungeonGrid[i][j] = SetupDungeonCell(dungeonCellType, new Vector2I(i, j));
				// GD.Print($"Dungeon position {i},{j}: {DungeonGrid[i][j].DungeonCellType}");
			}
		}
	}
	
	//TODO: Make weighted randoms that can be changed throughout the playthrough based on current tier, or just change % values
	private DungeonCellType GetRandomDungeonCell()
	{
		float randomValue = GD.Randf();
		
		if(randomValue < .70f)
		{
			return DungeonCellType.Encounter;
		}
		else if(randomValue < .85f)
		{
			return DungeonCellType.Loot; 
		}
		else if(randomValue < .95f)
		{
			return DungeonCellType.Shop;
		}
		else if(randomValue < .97f)
		{
			return DungeonCellType.RareEncounter;
		}
		else if(randomValue <.99f)
		{
			return DungeonCellType.RareLoot;
		}
		else
		{
			return DungeonCellType.ExoticShop;
		}
	}
	
	private DungeonCell SetupDungeonCell(DungeonCellType dungeonCellType, Vector2I position)
	{
		switch (dungeonCellType)
		{
			case DungeonCellType.Encounter:
				EncounterDungeonCell encounterDungeonCell = new (dungeonCellType, position);
				encounterDungeonCell.SetupEncounterEntities(SetupEncounterList());
				return encounterDungeonCell;
			case DungeonCellType.RareEncounter:
				return new DungeonCell(dungeonCellType, position);
			case DungeonCellType.Shop:
				return new DungeonCell(dungeonCellType, position);
			case DungeonCellType.ExoticShop:
				return new DungeonCell(dungeonCellType, position);
			case DungeonCellType.Loot:
				return new DungeonCell(dungeonCellType, position);
			case DungeonCellType.RareLoot:
				return new DungeonCell(dungeonCellType, position);
			case DungeonCellType.Boss:
				return new DungeonCell(dungeonCellType, position);
			default:
				return default;
		}
	}
	
	//Select enemies at certain tier threshold
	//Assign number of enemies in encounter (could be based on set of parameters)
	//Assign selected enemies to specific encounter
	private List<EntityInfo> SetupEncounterList()
	{
		List<EntityInfo> encounterList = new List<EntityInfo>();
		
		encounterList = enemyEntityDatabase.Entities
									.Where(x => x.Tier == currentTier - 1
									|| x.Tier == currentTier
									|| x.Tier == currentTier + 1).ToList();
		
		return encounterList;
	}
}
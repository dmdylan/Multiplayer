using Godot;

public class DungeonCell
{
	public DungeonCellType DungeonCellType { get; private set; }
	public Vector2I GridLocation { get; private set; }

	public DungeonCell(DungeonCellType dungeonCellType, Vector2I gridLocation)
	{
		DungeonCellType = dungeonCellType;
		GridLocation = gridLocation;
	}
}

public enum DungeonCellType
{
	Encounter,
	RareEncounter,
	Shop,
	ExoticShop,
	Loot,
	RareLoot,
	Boss
}

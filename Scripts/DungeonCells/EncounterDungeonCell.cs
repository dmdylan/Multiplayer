using System.Collections.Generic;
using Godot;

public class EncounterDungeonCell : DungeonCell
{
	public List<EntityInfo> EnemyEntities { get; private set; } = new();
	
	public EncounterDungeonCell(DungeonCellType dungeonCellType, Vector2I gridLocation) : base(dungeonCellType, gridLocation)
	{
	}
	
	public virtual void SetupEncounterEntities(List<EntityInfo> entities)
	{
		int count = GD.RandRange(1,5);
		
		for (int i = 0; i < count; i++)
		{
			EnemyEntities.Add(entities[(int)GD.Randi() % entities.Count]);
		}
		
		GD.Print($"Dungeon Cell {GridLocation} contains {EnemyEntities.Count} enemies");
	}
}

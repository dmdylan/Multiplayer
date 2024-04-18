using System.Collections.Generic;
using Godot;

public class EncounterDungeonCell : DungeonCell
{
	public List<EntityInfo> EnemyEntities { get; private set; } = new();
	
	public EncounterDungeonCell(DungeonCellType dungeonCellType, Vector2I gridLocation) : base(dungeonCellType, gridLocation)
	{
	}
	
	public void SetupEncountEntities(List<EntityInfo> entities)
	{
		int count = (int)GD.Randi() % 5;
		
		for (int i = 0; i < count; i++)
		{
			EnemyEntities.Add(entities[(int)GD.Randi() % entities.Count]);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GameManager : Node
{
	private static GameManager instance;
	public static GameManager Instance => instance;

	[Export] public EntityDatabase EntityDatabase { get; private set; }

	public List<PlayerInfo> Players { get; } = new();

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
		
		GD.Seed(12345);
	}

	public void StartGame()
	{
 		DungeonManager.Instance.PopulateDungeonGrid();
				
		UIManager.Instance.LoadGameUI();
	}
	
	public void SpawnPlayerEntities(string entityName)
	{
		foreach (var player in Players)
		{
			EntityInfo entityInfo = EntityDatabase.Entities.Where(x => x.Name == entityName).FirstOrDefault();
			
			player.SetEntity(EntityManager.Instance.CreateNewEntity(entityInfo, player.ID));
		}
	}
}

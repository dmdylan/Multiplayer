using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GameManager : Node
{
	private static GameManager instance;
	public static GameManager Instance => instance;

	[Export] private PackedScene gameDebugScene;

	[Export] public EntityDatabase EntityDatabase { get; private set; }
	
	public List<PlayerInfo> Players { get; } = new();
	
	private Dictionary<int, Vector2I> playerDungeonTileSelections = new(); 

	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;
			
		GameEventsManager.DungeonTileNodePressed += DungeonTileNodePressedEventHandler;
	}

	public override void _ExitTree()
	{
		GameEventsManager.DungeonTileNodePressed -= DungeonTileNodePressedEventHandler;
	}

	public override void _Ready()
	{
		base._Ready();
		
		GD.Seed(12345);
	}

	public void StartGame()
	{
 		DungeonManager.Instance.PopulateDungeonGrid();
		
		var scene = gameDebugScene.Instantiate();
		
		AddChild(scene);
				
		// UIManager.Instance.LoadGameUI();
	}
	
	public void SpawnPlayerEntities(string entityName)
	{
		foreach (var player in Players)
		{
			EntityInfo entityInfo = EntityDatabase.Entities.Where(x => x.Name == entityName).FirstOrDefault();
			
			player.SetEntity(EntityManager.Instance.CreateNewEntity(entityInfo, player.ID));
		}
	}
	
	private void DungeonTileNodePressedEventHandler(int playerID, Vector2I tileGridPosition)
	{
		playerDungeonTileSelections.Add(playerID, tileGridPosition);
		
		if(playerDungeonTileSelections.Count == Players.Count)
		{
			//Find tile with most votes
			//Load the appropriate scene of the selected tile
		}
	}
}

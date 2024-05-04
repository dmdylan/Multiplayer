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

	public void StartGame()
	{
 		DungeonManager.Instance.PopulateDungeonGrid();
				
		UIManager.Instance.LoadGameUI();
	}
	
	public void SpawnPlayerEntities(string entityName)
	{
		foreach (PlayerInfo player in Players)
		{
			EntityInfo entityInfo = EntityDatabase.Entities.Where(x => x.Name == entityName).FirstOrDefault();
			
			player.SetEntity(EntityManager.Instance.CreateNewEntity(entityInfo));
		}
	}
	
	private void DungeonTileNodePressedEventHandler(int playerID, Vector2I tileGridPosition)
	{
		if(Multiplayer.IsServer())
			DungeonTileNodeRpc(playerID, tileGridPosition);
		else
			RpcId(1, nameof(DungeonTileNodeRpc), playerID, tileGridPosition);
	}
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void DungeonTileNodeRpc(int playerID, Vector2I tileGridPosition)
	{
		GD.Print("Dungeon tile node pressed called");
		if (playerDungeonTileSelections.TryGetValue(playerID, out Vector2I selected))
		{
			playerDungeonTileSelections[playerID] = selected;
		}
		else
		{
			playerDungeonTileSelections.Add(playerID, tileGridPosition);
		}

		if (playerDungeonTileSelections.Count == Players.Count)
		{
			CheckTileVoteCount();
		}
	}
	
	private void CheckTileVoteCount()
	{
		Dictionary<Vector2I, int> keyValuePairs = new();
		
		foreach (Vector2I value in playerDungeonTileSelections.Values)
		{
			if(keyValuePairs.ContainsKey(value))
			{
				keyValuePairs[value]++;
			}
			else
			{
				keyValuePairs.Add(value, 1);
			}
		}
		
		Vector2I gridPosition;
		
		if(keyValuePairs.Count > 1 && keyValuePairs.ElementAt(0).Value == keyValuePairs.ElementAt(1).Value)
		{
			gridPosition = keyValuePairs.ElementAt(GD.RandRange(0,1)).Key;		
		}
		else
		{
			gridPosition = keyValuePairs.ElementAt(0).Key;			
		}
		
		// DungeonCell dungeonCell = DungeonManager.Instance.DungeonGrid[gridPosition.X][gridPosition.Y];
			
		Rpc(nameof(ChangeSceneRpc), gridPosition);
	}
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void ChangeSceneRpc(Vector2I gridPosition)
	{
		GameEventsManager.InvokeChangedScene(gridPosition);
	}
}

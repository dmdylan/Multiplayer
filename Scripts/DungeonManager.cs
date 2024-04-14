using Godot;

public partial class DungeonManager : Node
{
	private static DungeonManager instance;
	public static DungeonManager Instance => instance;
	
	[Export] private Vector2I gridSize;
	[Export] private DungeonTile[] dungeonTiles;
	[Export] private DungeonTile bossTile;
	
	private DungeonTile[][] dungeonGrid;
	public DungeonTile[][] DungeonGrid => dungeonGrid;


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
		
		dungeonGrid = new DungeonTile[gridSize.X][];
		
		//TODO: Put in for loop
		dungeonGrid[0] = new DungeonTile[gridSize.Y];
		dungeonGrid[1] = new DungeonTile[gridSize.Y];
		dungeonGrid[2] = new DungeonTile[gridSize.Y];
		dungeonGrid[3] = new DungeonTile[gridSize.Y];
		dungeonGrid[4] = new DungeonTile[1];
	}

	//TODO: Isn't synched between clients
	[Rpc(MultiplayerApi.RpcMode.Authority)]
	public void PopulateDungeonGrid()
	{
		//Loop through grid Y
		for (int i = 0; i < dungeonGrid.Length; i++)
		{
			//Loop through grid X
			for (int j = 0; j < dungeonGrid[i].Length; j++)
			{
				if(i == dungeonGrid.Length - 1  )
				{
					dungeonGrid[i][j] = bossTile;
					GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");
					continue;
				}
				
				dungeonGrid[i][j] = dungeonTiles[GD.Randi() % dungeonTiles.Length];
				GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");	
			}
		}
		
		// if(Multiplayer.IsServer())
		// {
		// 	foreach (var player in GameManager.Instance.Players)
		// 	{
				
		// 	}
		// }
	}
	
	[Rpc]
	private void SendDungeonGrid()
	{
		
	}
	
	public void DungeonTileSelected()
	{
		
	}
}
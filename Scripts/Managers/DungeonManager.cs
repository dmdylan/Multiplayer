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
		
		for (int i = 0; i < gridSize.X; i++)
		{
			if(i == gridSize.X - 1)
				dungeonGrid[i] = new DungeonTile[1];
			else
				dungeonGrid[i] = new DungeonTile[gridSize.Y];
		}
	}

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
					// GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");
					continue;
				}
				
				dungeonGrid[i][j] = dungeonTiles[GD.Randi() % dungeonTiles.Length];
				// GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");	
			}
		}		
	}
}
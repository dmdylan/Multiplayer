using Godot;

public partial class DungeonManager : Node
{
	private static DungeonManager instance;
	public static DungeonManager Instance => instance;
	
	[Export] private Vector2I gridSize;
	[Export] private DungeonTile[] dungeonTiles;
	[Export] private DungeonTile bossTile;

    public DungeonTile[][] DungeonGrid { get; private set; }

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
		
		DungeonGrid = new DungeonTile[gridSize.X][];
		
		for (int i = 0; i < gridSize.X; i++)
		{
			if(i == gridSize.X - 1)
				DungeonGrid[i] = new DungeonTile[1];
			else
				DungeonGrid[i] = new DungeonTile[gridSize.Y];
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
				if(i == DungeonGrid.Length - 1  )
				{
					DungeonGrid[i][j] = bossTile;
					// GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");
					continue;
				}
				
				DungeonGrid[i][j] = dungeonTiles[GD.Randi() % dungeonTiles.Length];
				// GD.Print($"Dungeon position {i},{j}: {dungeonGrid[i][j].TileName}");	
			}
		}		
	}
	
	public void SetupDungeonTiles()
	{
		
	}
}
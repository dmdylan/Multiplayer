using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameUI : Control
{
	[Export] private Button diceRollDebugButton;
	
	[ExportCategory("Dungeon Tile Info")]
	[Export] private DungeonTileInfo[] dungeonTiles;
	
	[ExportCategory("Scenes")]
	[Export] private PackedScene playerCharacterNameplateScene;
	[Export] private PackedScene enemyEntityNameplateScene;
	[Export] private PackedScene dungeonTileUIScene;
	
	[ExportCategory("Parent Nodes")]
	[Export] private Control characterNameplateParent;
	[Export] private HBoxContainer[] dungeonGridContainers;

	private Dictionary<Vector2I, DungeonTileNode> dungeonTileNodes = new();
	private Dictionary<int, PlayerDungeonMarker> dungeonMarkers = new();
	
	private List<DungeonTileInfo> dungeonTileInfoList;

	public override void _EnterTree()
	{
		GameEventsManager.DungeonTileNodePressed += PlayerSelectedDungeonTile;
		diceRollDebugButton.Pressed += RollDice;
	}

	public override void _ExitTree()
	{
		GameEventsManager.DungeonTileNodePressed -= PlayerSelectedDungeonTile;
		diceRollDebugButton.Pressed -= RollDice;
	}
	
	private void RollDice()
	{	
		int entityListID = EntityManager.Instance.ActiveEntities.Where(x => x.OwnerID == Multiplayer.GetUniqueId()).First().GetIndex();
		
		RpcId(1, nameof(RollDiceRpc), entityListID);
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void RollDiceRpc(int entityID)
	{	
		DiceManager.Instance.RollDice(entityID);
	}

	public override void _Ready()
	{
		dungeonTileInfoList = dungeonTiles.ToList();
		
		SpawnPlayerCharacterNameplates();
		// SetDungeonTiles();	
	}
	
	private void SpawnPlayerCharacterNameplates()
	{
		foreach (var player in GameManager.Instance.Players)
		{
			PlayerCharacterNameplate characterNameplate = playerCharacterNameplateScene.Instantiate<PlayerCharacterNameplate>();
			
			characterNameplate.NameLabel.Text = player.Name;

			characterNameplate.Icon.Texture = ImageTexture.CreateFromImage(ResourceLoader.Load<Image>(player.Entity.EntityInfo.EntityIcon.ResourcePath));

			characterNameplateParent.AddChild(characterNameplate);
		}
	}
	
	// [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void SetDungeonTiles()
	{
		for (int i = 0; i < DungeonManager.Instance.DungeonGrid.Length; i++)
		{
			for (int j = 0; j < DungeonManager.Instance.DungeonGrid[i].Length; j++)
			{				
				var dungeonTile = dungeonTileUIScene.Instantiate() as DungeonTileNode;
				
				dungeonTile.InitDungeonTile(DungeonManager.Instance.DungeonGrid[i][j]);		
					
				dungeonTile.TextureButton.TextureNormal = GetTileType(DungeonManager.Instance.DungeonGrid[i][j].DungeonCellType).TileTexture;
				
				//Disable all but the first row for initalization
				if(i != 0)
					dungeonTile.TextureButton.Disabled = true;
				
				dungeonTile.Label.Text = GetTileType(DungeonManager.Instance.DungeonGrid[i][j].DungeonCellType).TileName;
				
				dungeonTile.Name = $"{j},{i}";
				
				
				dungeonTileNodes.Add(dungeonTile.TilePosition, dungeonTile);
				dungeonGridContainers[i].AddChild(dungeonTile);
			}
		}
	}
	
	private DungeonTileInfo GetTileType(DungeonCellType dungeonCellType)
	{			
		return dungeonTileInfoList.Where(x => x.DungeonCellType == dungeonCellType).FirstOrDefault();
	}
	
	private void PlayerSelectedDungeonTile(int playerID, Vector2I tileGridPosition)
	{
		Rpc(nameof(SetIconOnTile), playerID, tileGridPosition);	
	}
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void SetIconOnTile(int id, Vector2I tileGridPosition)
	{
		//TODO: Setup checks for this when not being lazy
		DungeonTileNode tile = dungeonTileNodes[tileGridPosition];
		
		if(!dungeonMarkers.ContainsKey(id))
		{
			dungeonMarkers.Add(id, new PlayerDungeonMarker
			{
				imageTexture = ImageTexture.CreateFromImage(GameManager.Instance.Players.Where(x => x.ID == id).First().Entity.EntityInfo.EntityIcon),
				currentSelectedTile = tile
			});
			
			dungeonMarkers[id].currentSelectedTile.ImageTextures.Where(x => x.Texture == null).FirstOrDefault().Texture = dungeonMarkers[id].imageTexture;
		}
		else
		{
			dungeonMarkers[id].currentSelectedTile.ImageTextures.Where(x => x.Texture == dungeonMarkers[id].imageTexture).First().Texture = null;	
			
			tile.ImageTextures.Where(x => x.Texture == null).FirstOrDefault().Texture = dungeonMarkers[id].imageTexture;
			
			dungeonMarkers[id].currentSelectedTile = tile;
		}
	}
	
	private class PlayerDungeonMarker
	{
		public ImageTexture imageTexture;
		public DungeonTileNode currentSelectedTile;
	}
}

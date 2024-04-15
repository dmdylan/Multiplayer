using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIManager : Node
{
	private static UIManager instance;
	public static UIManager Instance => instance;
	
	private Dictionary<int, PlayerDungeonMarker> dungeonMarkers = new();
	
	public List<DungeonTileNode> DungeonTileNodes { get; private set; } = new();
	
	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;	
	}
	
	public void PlayerSelectedDungeonTile(int playerID, int tileID)
	{
		Rpc(nameof(SetIconOnTile), playerID, tileID);	
	}
	
	//Does not remove current node for new one
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	public void SetIconOnTile(int id, int tileID)
	{
		DungeonTileNode tile = DungeonTileNodes[tileID];
		
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



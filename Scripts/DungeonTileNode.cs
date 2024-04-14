using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DungeonTileNode : Panel
{
	[Export] private TextureButton textureButton;
	[Export] private GridContainer iconGrid;

	private List<TextureRect> imageTextures = new();

	public override void _Ready()
	{
		foreach (var item in iconGrid.GetChildren())
		{
			if(item is TextureRect rect)
				imageTextures.Add(rect);
		}
	}

	public override void _EnterTree()
	{
		textureButton.Pressed += OnDungeonTileButtonPressed;
	}

	public override void _ExitTree()
	{
		textureButton.Pressed -= OnDungeonTileButtonPressed;
	}

	private void OnDungeonTileButtonPressed()
	{
		Rpc(nameof(SetIconOnTile), Multiplayer.GetUniqueId());
	}
	
	//Does not remove current node for new one
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void SetIconOnTile(int id)
	{
		TextureRect textureRect = imageTextures.Where(x => x.Texture == null).FirstOrDefault();
		
		if(textureRect == null)
			return;
		
		Image image = GameManager.Instance.Players.Where(x => x.ID == id).First().Entity.EntityInfo.EntityIcon;
		
		textureRect.Texture = ImageTexture.CreateFromImage(image);
		
		textureRect.Visible = true;
	}
}

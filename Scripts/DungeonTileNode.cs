using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DungeonTileNode : Panel
{
	[Export] private TextureButton textureButton;
	[Export] private GridContainer iconGrid;

	private List<TextureRect> imageTextures;

	public override void _Ready()
	{
		foreach (var item in iconGrid.GetChildren(false))
		{
			GD.Print(item.Name);
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
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void SetIconOnTile(int id)
	{
		TextureRect textureRect = imageTextures.Where(x => x.Texture == null).First();
		
		textureRect.Texture = ImageTexture.CreateFromImage(GameManager.Instance.Players[id].Entity.EntityInfo.EntityIcon);
		
		textureRect.Visible = true;
	}
}

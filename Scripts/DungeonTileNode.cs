using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DungeonTileNode : Panel
{
	[Export] private TextureButton textureButton;
	[Export] private GridContainer iconGrid;

	public int ID {get; set;}
	
	//TODO: Remove the list, make one ui scene for the player choice and add to the node, not keep track in node
	public List<TextureRect> ImageTextures { get; private set; } = new();

	public override void _Ready()
	{
		foreach (var item in iconGrid.GetChildren())
		{
			GD.Print(item);
			if(item is TextureRect rect)
				ImageTextures.Add(rect);
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
		UIManager.Instance.PlayerSelectedDungeonTile(Multiplayer.GetUniqueId(), ID);
	}
}

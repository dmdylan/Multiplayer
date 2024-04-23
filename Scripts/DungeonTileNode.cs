using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DungeonTileNode : Panel
{
	[Export] public TextureButton TextureButton { get; private set; }
	[Export] public GridContainer IconGrid {get; private set;}
	[Export] public Label Label {get; private set;}

	private DungeonCell dungeonCell;
	public Vector2I TilePosition { get; private set; }
	
	//TODO: Remove the list, make one ui scene for the player choice and add to the node, not keep track in node
	public List<TextureRect> ImageTextures { get; private set; } = new();

	public override void _Ready()
	{
		foreach (var item in IconGrid.GetChildren())
		{
			if(item is TextureRect rect)
				ImageTextures.Add(rect);
		}
	}

	public override void _EnterTree()
	{
		TextureButton.Pressed += OnDungeonTileButtonPressed;
	}

	public override void _ExitTree()
	{
		TextureButton.Pressed -= OnDungeonTileButtonPressed;
	}

	public void InitDungeonTile(DungeonCell dungeonCell)
	{
		this.dungeonCell = dungeonCell;
		TilePosition = dungeonCell.GridLocation;
	}
	
	private void OnDungeonTileButtonPressed()
	{
		GameEventsManager.InvokeDungeonTileNodePressed(Multiplayer.GetUniqueId(), TilePosition);
		
		// if(dungeonCell is EncounterDungeonCell encounterDungeonCell)
		// {
		// 	foreach (var item in encounterDungeonCell.EnemyEntities)
		// 	{
		// 		GD.Print(item.Name);
		// 	}
		// }
	}
}

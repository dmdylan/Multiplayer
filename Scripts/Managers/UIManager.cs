using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIManager : Node
{
	private static UIManager instance;
	public static UIManager Instance => instance;
	
	[ExportCategory("UI Scenes")]
	[Export] private PackedScene GameUIScene;
	
	private GameUI gameUI;
	
	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;
			
		GameEventsManager.ChangedScene += ChangedSceneEventHandler;	
	}

	public override void _ExitTree()
	{
		GameEventsManager.ChangedScene -= ChangedSceneEventHandler;	
	}

	private void ChangedSceneEventHandler(Vector2I i)
	{
		switch (Helpers.GetDungeonCell(i).DungeonCellType)
		{
			case DungeonCellType.Encounter:
				gameUI.ChangeDungeonGridVisibility();
				break;
			case DungeonCellType.RareEncounter:
				break;
			case DungeonCellType.Shop:
				break;
			case DungeonCellType.ExoticShop:
				break;
			case DungeonCellType.Loot:
				break;
			case DungeonCellType.RareLoot:
				break;
			case DungeonCellType.Boss:
				break;
		}
	}

	public void LoadGameUI()
	{
		var scene = ResourceLoader.Load<PackedScene>(GameUIScene.ResourcePath).Instantiate<Control>() as GameUI;
		
		gameUI = scene;
		
		GetTree().Root.AddChild(scene);
		
		scene.SetDungeonTiles();
	}
	
	public void LoadDungeonTileUIScene()
	{
		
	}
}



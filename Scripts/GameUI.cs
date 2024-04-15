using Godot;
using System;
using System.Collections.Generic;

public partial class GameUI : Control
{
	[ExportCategory("Scenes")]
	[Export] private PackedScene playerCharacterNameplateScene;
	[Export] private PackedScene dungeonTileUIScene;
	
	[ExportCategory("Parent Nodes")]
	[Export] private Control characterNameplateParent;
	[Export] private HBoxContainer[] dungeonGridContainers;

	public List<DungeonTileNode> DungeonTileNodes { get; private set; }

	public override void _Ready()
	{
		SpawnPlayerCharacterNameplates();
		SetDungeonTiles();
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
	
	private void SetDungeonTiles()
	{
		int counter = 0;
		
		for (int i = 0; i < DungeonManager.Instance.DungeonGrid.Length; i++)
		{
			for (int j = 0; j < DungeonManager.Instance.DungeonGrid[i].Length; j++)
			{				
				var dungeonTile = dungeonTileUIScene.Instantiate() as DungeonTileNode;
				
				dungeonTile.ID = counter;
				
				UIManager.Instance.DungeonTileNodes.Add(dungeonTile);
				
				TextureButton textureButton = dungeonTile.GetNode<TextureButton>("MarginContainer/TextureButton");
					
				textureButton.TextureNormal = DungeonManager.Instance.DungeonGrid[i][j].TileTexture;
				
				if(i != 0)
					textureButton.Disabled = true;
				
				dungeonTile.GetNode<Label>("Label").Text = DungeonManager.Instance.DungeonGrid[i][j].TileName;
				
				dungeonTile.Name = $"{j},{i}";
				
				dungeonGridContainers[i].AddChild(dungeonTile);
				
				counter++;
			}
		}
	}
}

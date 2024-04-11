using Godot;
using System;

public partial class GameUI : Control
{
	[ExportCategory("Scenes")]
	[Export] private PackedScene playerCharacterNameplateScene;
	[Export] private PackedScene dungeonTileUIScene;
	
	[ExportCategory("Parent Nodes")]
	[Export] private Control characterNameplateParent;
	[Export] private GridContainer dungeonGridContainer;

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

			characterNameplate.Icon.Texture = ResourceLoader.Load<ImageTexture>(player.PlayerEntity.EntityInfo.EntityIcon.ResourcePath);

			characterNameplateParent.AddChild(characterNameplate);
		}
	}
	
	private void SetDungeonTiles()
	{
		for (int i = 0; i < DungeonManager.Instance.DungeonGrid.GetLength(1); i++)
		{
			for (int j = 0; j < DungeonManager.Instance.DungeonGrid.GetLength(0); j++)
			{
				var dungeonTile = dungeonTileUIScene.Instantiate();
				
				dungeonTile.GetNode<TextureButton>("MarginContainer/TextureButton").TextureNormal = DungeonManager.Instance.DungeonGrid[j,i].TileTexture;
				
				dungeonTile.GetNode<Label>("Label").Text = DungeonManager.Instance.DungeonGrid[j,i].TileName;
				
				dungeonTile.Name = $"{j},{i}";
				
				dungeonGridContainer.AddChild(dungeonTile);
			}
		}
	}
}

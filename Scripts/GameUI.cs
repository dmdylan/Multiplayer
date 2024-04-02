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

			switch (player.PlayerCharacterClass)
			{
				case PlayerCharacterClass.Warrior:
					characterNameplate.Icon.Color = new Color(0.412f, 0.412f, 0.412f,1);
					break;
				case PlayerCharacterClass.Mage:
					characterNameplate.Icon.Color = new Color(0.118f, 0.537f, 1,1);
					break;
				case PlayerCharacterClass.Cleric:
					characterNameplate.Icon.Color = new Color(0.789f, 0.749f, 0.312f,1);
					break;
				case PlayerCharacterClass.Rogue:
					characterNameplate.Icon.Color = new Color(0.149f, 0.353f, 0.184f,1);
					break;
				default:
					break;
			}

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
				
				dungeonTile.GetNode<ColorRect>("MarginContainer/ColorRect").Color = DungeonManager.Instance.DungeonGrid[j,i].TileColor;
				
				dungeonTile.GetNode<Label>("Label").Text = DungeonManager.Instance.DungeonGrid[j,i].TileName;
				
				dungeonTile.Name = $"{j},{i}";
				
				dungeonGridContainer.AddChild(dungeonTile);
			}
		}
	}
}

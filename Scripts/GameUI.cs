using Godot;
using System;

public partial class GameUI : Control
{
	[Export] private PackedScene playerCharacterNameplateScene;
	[Export] private Control characterNameplateParent;
	
	public override void _Ready()
	{
		SpawnPlayerCharacterNameplates();
	}
	
	private void SpawnPlayerCharacterNameplates()
	{
		foreach (var player in GameManager.Players)
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
}

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

			// switch (player.PlayerCharacterClass)
			// {
			// 	case PlayerCharacterClass.Warrior:
			// 		break;
			// 	case PlayerCharacterClass.Mage:
			// 		break;
			// 	case PlayerCharacterClass.Cleric:
			// 		break;
			// 	case PlayerCharacterClass.Rogue:
			// 		break;
			// 	default:
			// 		break;
			// }

			characterNameplateParent.AddChild(characterNameplate);
		}
	}
}

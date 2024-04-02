using Godot;
using System;
using System.Linq;

public partial class PlayerCharacterSelection : VBoxContainer
{
	[Export] private Button warriorButton;
	[Export] private Button mageButton;
	[Export] private Button clericButton;
	[Export] private Button rogueButton;
	
	public override void _EnterTree()
	{
		warriorButton.Pressed += OnWarriorButtonPressed;
		mageButton.Pressed += OnMageButtonPressed;
		clericButton.Pressed += OnClericButtonPressed;
		rogueButton.Pressed += OnRogueButtonPressed;
	}

	public override void _ExitTree()
	{
		warriorButton.Pressed -= OnWarriorButtonPressed;
		mageButton.Pressed -= OnMageButtonPressed;
		clericButton.Pressed -= OnClericButtonPressed;
		rogueButton.Pressed -= OnRogueButtonPressed;
	}

	private void OnWarriorButtonPressed()
	{
		GameManager.Instance.Players.Where(x => x.ID == Multiplayer.GetUniqueId()).First().ChangePlayerCharacterClass(PlayerCharacterClass.Warrior);
	}

	private void OnMageButtonPressed()
	{
		GameManager.Instance.Players.Where(x => x.ID == Multiplayer.GetUniqueId()).First().ChangePlayerCharacterClass(PlayerCharacterClass.Mage);
	}

	private void OnClericButtonPressed()
	{
		GameManager.Instance.Players.Where(x => x.ID == Multiplayer.GetUniqueId()).First().ChangePlayerCharacterClass(PlayerCharacterClass.Cleric);
	}

	private void OnRogueButtonPressed()
	{
		GameManager.Instance.Players.Where(x => x.ID == Multiplayer.GetUniqueId()).First().ChangePlayerCharacterClass(PlayerCharacterClass.Rogue);
	}
}

using Godot;

public partial class PlayerLobbyNameplate : HBoxContainer
{
	[Export] private CheckBox readyCheckbox;
	[Export] private Label nameLabel;
	[Export] private Label currentCharacterClassLabel;

	public CheckBox ReadyCheckBox => readyCheckbox;
	public Label NameLabel => nameLabel;
	public Label CurrentCharacterClassLabel => currentCharacterClassLabel;
}

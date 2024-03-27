using Godot;

public partial class PlayerLobbyController : HBoxContainer
{
	[Export] private CheckBox readyCheckbox;
	[Export] private Label nameLabel;

	public CheckBox ReadyCheckBox => readyCheckbox;
	public Label NameLabel => nameLabel;
}

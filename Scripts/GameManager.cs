using System.Collections.Generic;
using Godot;

public partial class GameManager : Node
{
	private static GameManager instance;
	public static GameManager Instance => instance;

    public List<PlayerInfo> Players { get; } = new();

    public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;
	}
	
	public void StartGame()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://Scenes/UI/GameUI.tscn").Instantiate<Control>();
		DungeonManager.Instance.PopulateDungeonGrid();
		GetTree().Root.AddChild(scene);
	}
}

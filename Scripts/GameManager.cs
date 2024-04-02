using System;
using System.Collections.Generic;
using Godot;

public partial class GameManager : Node
{
	private static GameManager instance;
	public static GameManager Instance => instance;
	
	private readonly List<PlayerInfo> players = new();
	public List<PlayerInfo> Players => players;

	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;
	}
	
	public void StartGame()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://Scenes/GameUI.tscn").Instantiate<Control>();
		DungeonManager.Instance.PopulateDungeonGrid();
		GetTree().Root.AddChild(scene);
	}
}

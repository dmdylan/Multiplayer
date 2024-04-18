using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIManager : Node
{
	private static UIManager instance;
	public static UIManager Instance => instance;
	
	[ExportCategory("UI Scenes")]
	[Export] private PackedScene GameUIScene;
	
	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;	
	}
	
	public void LoadGameUI()
	{
		var scene = ResourceLoader.Load<PackedScene>(GameUIScene.ResourcePath).Instantiate<Control>() as GameUI;
		
		GetTree().Root.AddChild(scene);
		
		scene.SetDungeonTiles();
	}
}



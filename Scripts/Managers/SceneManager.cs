using Godot;
using System;

public partial class SceneManager : Node
{
	private static SceneManager instance;
	public static SceneManager Instance => instance;

	[ExportCategory("Scenes")]
	[Export] private PackedScene combatScene;
	[Export] private PackedScene shopScene;

	private Node currentLevel;

	public override void _EnterTree()
	{
		if (instance != null)
			QueueFree();
		else
			instance = this;

		GameEventsManager.ChangedScene += ChangedSceneEventHandler;
	}

	public override void _ExitTree()
	{
		GameEventsManager.ChangedScene -= ChangedSceneEventHandler;
	}

	private void ChangedSceneEventHandler(DungeonCellType dungeonCellType)
	{
		switch (dungeonCellType)
		{
			case DungeonCellType.Encounter:
				LoadNewScene(combatScene);
				break;
			case DungeonCellType.RareEncounter:
				break;
			case DungeonCellType.Shop:
				break;
			case DungeonCellType.ExoticShop:
				break;
			case DungeonCellType.Loot:
				break;
			case DungeonCellType.RareLoot:
				break;
			case DungeonCellType.Boss:
				break;
		}
	}
	
	private void LoadNewScene(PackedScene packedScene)
	{
		Node sceneInstance = ResourceLoader.Load<PackedScene>(packedScene.ResourcePath).Instantiate();

		currentLevel = sceneInstance;

		AddChild(sceneInstance);
	}
}
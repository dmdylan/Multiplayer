using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class GameManager : Node
{
	private static GameManager instance;
	public static GameManager Instance => instance;

	[Export] public EntityDatabase EntityDatabase { get; private set; }

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
	
	public void SpawnPlayerEntities(string entityName)
	{
		foreach (var player in Players)
		{
			GD.Print(entityName);
			EntityInfo entityInfo = EntityDatabase.Entities.Where(x => x.Name == entityName).FirstOrDefault();
			
			player.SetEntity(EntityManager.Instance.CreateNewEntity(entityInfo, player.ID));
		}
	}
}

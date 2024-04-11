using Godot;
using System;

public partial class EntityManager : Node
{
	[Export] private PackedScene entityBase;
	
	private static EntityManager instance;
	public static EntityManager Instance => instance;

	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;		
	}
	
	public Entity CreateNewEntity(EntityInfo entityInfo, int? ownerID = null)
	{
		Entity entity = entityBase.Instantiate() as Entity;
		
		entity.InitializeEntity(entityInfo, ownerID);
		
		AddChild(entity);
		
		return entity;
	}
}
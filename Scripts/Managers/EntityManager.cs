using Godot;
using System;
using System.Collections.Generic;

public partial class EntityManager : Node
{
	[Export] private PackedScene entityBase;
	
	private static EntityManager instance;
	public static EntityManager Instance => instance;
	
	public List<Entity> ActiveEntities { get; private set; } = new();
	
	int entityIDCounter = 0;

	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;		
	}
	
	public Entity CreateNewEntity(EntityInfo entityInfo)
	{
		Entity entity = entityBase.Instantiate() as Entity;
		
		entity.InitializeEntity(entityInfo, entityIDCounter);
		
		ActiveEntities.Add(entity);
		
		AddChild(entity);
		
		entityIDCounter++;
		
		return entity;
	}
}

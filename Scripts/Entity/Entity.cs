using Godot;
using System;

public partial class Entity : Node
{
	protected EntityInfo entityInfo;
	private EntityStatController entityStatController;
	
	public void InitializeEntity(EntityInfo entityInfo)
	{
		this.entityInfo = entityInfo;
		
		entityStatController = GetNodeOrNull<EntityStatController>("EntityStatController");
	}
	
	private void EntityStatSetup()
	{
		
	}
}

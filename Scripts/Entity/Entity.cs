using Godot;
using System;

public partial class Entity : Node
{
	protected EntityInfo entityInfo;
	
	public void InitializeEntity(EntityInfo entityInfo)
	{
		this.entityInfo = entityInfo;
	}
}

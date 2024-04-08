using Godot;
using System;
using System.Collections.Generic;

public partial class StatusEffectComponent : Node
{
	private Entity entity;
	private Dictionary<string, StatusEffect> statusEffects;
	
	public void IntializeStatusEffectComponent(Entity entity)
	{
		this.entity = entity;
	}
}

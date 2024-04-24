using Godot;
using System;
using System.Collections.Generic;

public partial class DiceComponent : Node
{
	public List<Die> DiceList { get; private set; } = new();
	private Entity entity;
	
	public void InitalizeDiceComponent(Entity entity)
	{
		this.entity = entity;
	}
	
	public void AddDie(Die die)
	{
		DiceList.Add(die);
	}
	
	public void RemoveDie(Die die)
	{
		if(DiceList.Remove(die))
			GD.Print($"Removed {die}");
		else
			GD.PushError($"Could not remove {die}");
	}
	
	public void EditDie(Die die)
	{
		
	}
}

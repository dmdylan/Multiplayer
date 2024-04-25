using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DiceComponent : Node
{
	//TODO: Currently is just the info and the dice are made by the dice manager. Could have the dice made here and sent to dice manager.
	public List<DieInfo> DiceList { get; private set; } = new();
	private Entity entity;
	
	public void InitalizeDiceComponent(Entity entity)
	{
		this.entity = entity;
		
		foreach (var item in entity.EntityInfo.BaseDiceLoadout.DiceInfo)
		{
			DiceList.Add(item);
		}
	}
	
	public void AddDie(Die die)
	{
		// DiceList.Add(die);
	}
	
	public void RemoveDie(Die die)
	{
		// if(DiceList.Remove(die))
		// 	GD.Print($"Removed {die}");
		// else
		// 	GD.PushError($"Could not remove {die}");
	}
	
	public void EditDie(Die die)
	{
		
	}
}

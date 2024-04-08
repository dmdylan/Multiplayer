using Godot;
using System.Collections.Generic;

public partial class StatComponent : Node
{
	private Entity entity;
	public Dictionary<StatType, Stat> Stats {get; private set;} = new();
	
	public void InitalizeStatComponent(Entity entity)
	{
		this.entity = entity;
		
		foreach (var statPair in entity.EntityInfo.BaseStatSheet.BaseStats)
		{
			Stats.Add(statPair.StatType, new Stat(statPair.Value));
		}
	}
	
	public void AddStat(StatType type, float value)
	{
		if(Stats.ContainsKey(type))
		{
			GD.Print("Stat is already added");
			return;
		}

		Stat stat = new(value);
		Stats.Add(type, stat);
	}

	public void RemoveStat(StatType type)
	{
		if(!Stats.ContainsKey(type))
		{
			GD.Print("Cannot remove stat, it is not in the stat list");
			return;
		}
		
		Stats.Remove(type);
	}
	
	public void AddStatModifier(StatType type, StatModifier modifier)
	{
		if(!Stats.ContainsKey(type))
			return;
		
		Stats[type].AddModifier(modifier);
	}
	
	public void RemoveStatModifier(StatType type, StatModifier modifier)
	{
		if(!Stats.ContainsKey(type))
			return;
		
		Stats[type].RemoveModifier(modifier);
	}
	
	public void RemoveAllModifiersFromSource(StatType type, object source)
	{
		if(!Stats.ContainsKey(type))
			return;
			
		Stats[type].RemoveAllModifiersFromSource(source);
	}
}

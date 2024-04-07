using Godot;
using System;
using System.Collections.Generic;

public partial class EntityStatController : Node
{
	private Dictionary<StatType, Stat> stats;
	
	public void InitalizeStatController(BaseStatSheet baseStatSheet)
	{
		foreach (var statPair in baseStatSheet.BaseStats)
		{
			stats.Add(statPair.StatType, new Stat(statPair.Value));
		}
	}
}

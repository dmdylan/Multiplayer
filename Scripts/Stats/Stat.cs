using System;
using System.Collections.Generic;

public class Stat
{
	private readonly float baseValue;
	private readonly List<StatModifier> statModifiers = new();
	private float value = float.MinValue;
	private bool hasChanged = true;

	public float FinalValue
	{
		get
		{
			if (hasChanged)
			{
				value = CalculateValue();
				hasChanged = false;
			}
			return value;
		}
	}

	public Stat(float initalValue)
	{
		baseValue = initalValue;
	}	
	
	public void AddModifier(StatModifier statModifier)
	{
		hasChanged = true;
		statModifiers.Add(statModifier);
	}
	
	public void RemoveModifier(StatModifier statModifier)
	{
		hasChanged = true;
		statModifiers.Remove(statModifier);
	}
	
	private float CalculateValue()
	{
		float finalValue = baseValue;
		
		foreach (StatModifier statModifier in statModifiers)
		{
			
		}
		
		return finalValue;
	}
}

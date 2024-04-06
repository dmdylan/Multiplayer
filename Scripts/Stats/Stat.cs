using System;
using System.Collections.Generic;

[Serializable]
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
		statModifiers.Sort(CompareModifierOrder);
	}
	
	private int CompareModifierOrder(StatModifier a, StatModifier b)
	{
		if(a.Order < b.Order)
			return -1;
		else if(a.Order > b.Order)
			return 1;
		else
			return 0;
	}
	
	public bool RemoveModifier(StatModifier statModifier)
	{
		if(statModifiers.Remove(statModifier))
		{
			hasChanged = true;
			return true;
		}
		
		return false;
	}
	
	public bool RemoveAllModifiersFromSource(object source)
	{
		bool didRemove = false;
		
		for (int i = statModifiers.Count; i >= 0; i--)
		{
			if(statModifiers[i].Source == source)
			{
				statModifiers.RemoveAt(i);
				hasChanged = true;
				didRemove = true;
			}
		}
		
		return didRemove;
	}
	
	private float CalculateValue()
	{
		float finalValue = baseValue;
		float sumPercentAdd = 0;
		
		for (int i = 0; i < statModifiers.Count; i++)
		{
			StatModifier statModifier = statModifiers[i];
			
			//Add all modifiers to the final value
			if(statModifier.StatModifierType == StatModifierType.Flat)
			{
				finalValue += statModifier.Value;
			}
			//The statmodifier list should be sorted by type everytime a new modifier is added
			//so all additive modifiers should be next to each other and can be added up then multiplied
			else if(statModifier.StatModifierType == StatModifierType.PercentAdditive)
			{
				sumPercentAdd += statModifier.Value;
				
				if(i + 1 >= statModifiers.Count || statModifiers[i +1].StatModifierType != StatModifierType.PercentAdditive)
				{
					finalValue *= 1 + sumPercentAdd;
					sumPercentAdd = 0;
				}
			}
			//Multiply each value to final value
			else if(statModifier.StatModifierType == StatModifierType.PercentMultiplicitive)
			{
				finalValue *= 1 + statModifier.Value;	
			}
		}
		
		return finalValue;
	}
}

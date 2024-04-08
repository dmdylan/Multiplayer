using Godot;
using System;

[GlobalClass]
public partial class StatusEffectInfo : Resource
{
	[Export] public Image EffectIcon { get; private set; }
	[Export] public string EffectName { get; private set; }
	[Export] public string EffectDescription { get; private set; }
	[Export] public StatusEffectType EffectType { get; private set; }
	[Export] public StatusEffectApplicationTime EffectApplicationTime { get; private set; }
	[Export] public StatusEffectStackingType EffectStackingType { get; private set; }
}

public enum StatusEffectType
{
	Buff,
	Debuff,	
}

public enum StatusEffectApplicationTime
{
	TurnStart,
	TurnEnd,
	RoundStart,
	RoundEnd,
	OnAttack,
	OnDamaged
}

public enum StatusEffectStackingType
{
	Individual,
	Total
}
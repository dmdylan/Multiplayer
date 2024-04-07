using Godot;
using Godot.Collections;

[GlobalClass]
public partial class BaseStatSheet : Resource
{
	[Export] private BaseStatPair[] baseStats;
	
	public BaseStatPair[] BaseStats => baseStats;
}

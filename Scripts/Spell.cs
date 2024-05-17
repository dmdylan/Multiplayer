using System.Collections.Generic;
using Godot;

public partial class Spell : Node
{
	public SpellInfo SpellInfo { get; private set; }

	public Spell(SpellInfo spellInfo)
	{
		SpellInfo = spellInfo;
	}
	
	public virtual void Activate()
	{	
	}
}
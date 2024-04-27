using Godot;

[GlobalClass]
public partial class EntityInfo : Resource
{
	[ExportCategory("Base Info")]
	[Export] public string Name { get; private set; }
	[Export] public Texture2D EntityIcon { get; private set; }
	[Export] public BaseStatSheet BaseStatSheet { get; private set; }
	[Export] public BaseDiceLoadout BaseDiceLoadout { get; private set; }
	[Export] public DieFace[] AvailableDieFaces { get; private set; } 
	
	[ExportCategory("Enemy Info")]
	[Export] public int Tier {get; private set;}
	[Export] public int BaseExpValue {get; private set;}
}

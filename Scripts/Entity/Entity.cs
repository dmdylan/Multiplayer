using Godot;

public partial class Entity : Node
{
	[Export] public StatComponent StatComponent { get; private set; }
	[Export] public HealthComponent HealthComponent { get; private set; }
	[Export] public StatusEffectComponent StatusEffectComponent { get; private set; }
	[Export] public DiceComponent DiceComponent { get; private set; }
	[Export] public SpellBookComponent SpellBookComponent { get; private set; }
	public EntityInfo EntityInfo { get; private set; }
	public int EntityListID{ get; private set; }

	public void InitializeEntity(EntityInfo entityInfo, int entityListID)
	{
		EntityInfo = entityInfo;
		EntityListID = entityListID;
		
		//TODO: Change init so I don't need a reference to the entity?
		StatComponent.InitalizeStatComponent(this);
		HealthComponent.InitalizeHealthComponent(this);
		StatusEffectComponent.IntializeStatusEffectComponent(this);
		DiceComponent.InitalizeDiceComponent(this);
	}
}

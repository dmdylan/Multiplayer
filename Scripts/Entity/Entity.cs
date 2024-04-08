using Godot;

public partial class Entity : Node
{
	public EntityInfo EntityInfo { get; private set; }
	public StatComponent StatComponent { get; private set; }
	public HealthComponent HealthComponent { get; private set; }
	public StatusEffectComponent StatusEffectComponent { get; private set; }

	public void InitializeEntity(EntityInfo entityInfo)
	{
		EntityInfo = entityInfo;
		
		StatComponent = GetNodeOrNull<StatComponent>("StatComponent");
		HealthComponent = GetNodeOrNull<HealthComponent>("HealthComponent");
		StatusEffectComponent = GetNodeOrNull<StatusEffectComponent>("StatusEffectComponent");
		
		//TODO: Change init so I don't need a reference to the entity?
		StatComponent.InitalizeStatComponent(this);
		HealthComponent.InitalizeHealthComponent(this);
		StatusEffectComponent.IntializeStatusEffectComponent(this);
	}
}

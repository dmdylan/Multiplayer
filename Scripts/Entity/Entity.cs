using Godot;

public partial class Entity : Node
{
	[Export] public StatComponent StatComponent { get; private set; }
	[Export] public HealthComponent HealthComponent { get; private set; }
	[Export] public StatusEffectComponent StatusEffectComponent { get; private set; }
	public EntityInfo EntityInfo { get; private set; }
	public int? OwnerID { get; private set; }

	public void InitializeEntity(EntityInfo entityInfo, int? ownerID = null)
	{
		EntityInfo = entityInfo;
		OwnerID = ownerID;
		
		//TODO: Change init so I don't need a reference to the entity?
		StatComponent.InitalizeStatComponent(this);
		HealthComponent.InitalizeHealthComponent(this);
		StatusEffectComponent.IntializeStatusEffectComponent(this);
	}
}

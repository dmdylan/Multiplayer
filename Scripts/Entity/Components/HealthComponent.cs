using Godot;

public partial class HealthComponent : Node
{
	private Entity entity;
	private int currentHealth;
	
	public void InitalizeHealthComponent(Entity entity)
	{
		this.entity = entity;
		
		currentHealth = (int)entity.StatComponent.Stats[StatType.Health].FinalValue;
	}
	
	public void ChangeHealthValue(int amount)
	{
		if (currentHealth + amount > (int)entity.StatComponent.Stats[StatType.Health].FinalValue)
			currentHealth = (int)entity.StatComponent.Stats[StatType.Health].FinalValue;
		else if(currentHealth + amount < 0)
		{
			currentHealth  = 0;
			GD.Print($"{entity.Name} dead");
		}
		else
			currentHealth += amount;
	}
}
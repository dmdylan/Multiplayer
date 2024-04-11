public class PlayerInfo
{
	public int ID { get; private set; }
	public string Name { get; private set; }
	public Entity Entity { get; private set; }

	public PlayerInfo(int id, string name)
	{
		ID = id;
		Name = name;
	}
	
	public void SetEntity(Entity entity)
	{
		Entity = entity;
	}
}


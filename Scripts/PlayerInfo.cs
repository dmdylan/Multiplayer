using Godot;
using System;

public enum PlayerCharacterClass { Warrior, Mage, Cleric, Rogue}

public class PlayerInfo
{
	public int ID { get; private set; }
	public string Name { get; private set; }
	public Entity PlayerEntity { get; private set; }
	public PlayerCharacterClass PlayerCharacterClass { get; private set; }

	public PlayerInfo(int id, string name)
	{
		ID = id;
		Name = name;
	}
	
	public void ChangePlayerCharacterClass(PlayerCharacterClass playerCharacterClass)
	{
		PlayerCharacterClass = playerCharacterClass;
	}
}


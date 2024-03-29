using Godot;
using System;

public enum PlayerCharacterClass { Warrior, Mage, Cleric, Rogue}

public class PlayerInfo
{
	private readonly int id;
	private readonly string name;
	private PlayerCharacterClass playerCharacterClass;

	public int ID => id;
	public string Name => name;
	public PlayerCharacterClass PlayerCharacterClass => playerCharacterClass;

	public PlayerInfo(int id, string name)
	{
		this.id = id;
		this.name = name;
	}
	
	public void ChangePlayerCharacterClass(PlayerCharacterClass playerCharacterClass)
	{
		this.playerCharacterClass = playerCharacterClass;
	}
}


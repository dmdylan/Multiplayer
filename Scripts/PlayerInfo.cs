using Godot;
using System;

public class PlayerInfo
{
	private readonly int id;
	private readonly string name;

	public int ID => id;
	public string Name => name;

	public PlayerInfo(int id, string name)
	{
		this.id = id;
		this.name = name;

	}
}

using Godot;
using System;

public class PlayerInfo
{
	private readonly int id;
	private readonly string name;
	private bool isReady;

	public int ID => id;
	public string Name => name;
	public bool IsReady => isReady;

	public PlayerInfo(int id, string name, bool isReady)
	{
		this.id = ID;
		this.name = name;
		this.isReady = isReady;
	}
	
	public void ChangeReadyStatus()
	{
		isReady = !isReady;
	}
}

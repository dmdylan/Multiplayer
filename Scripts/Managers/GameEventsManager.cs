using System;
using Godot;

public static class GameEventsManager
{
	/// <summary>
	/// 
	/// </summary> <summary>
	/// <param name="PlayerID"></param>
	/// <param name="TileNodeID"></param>
	/// </summary>
	public static event Action<int, Vector2I> DungeonTileNodePressed;
	public static void InvokeDungeonTileNodePressed(int playerID, Vector2I tileGridPosition) => DungeonTileNodePressed?.Invoke(playerID, tileGridPosition);
	
	public static event Action<Vector2I> ChangedScene;
	public static void InvokeChangedScene(Vector2I dungeonCell) => ChangedScene?.Invoke(dungeonCell); 
}

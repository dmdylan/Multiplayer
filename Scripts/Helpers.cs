
using Godot;

public static class Helpers
{
	public static DungeonCell GetDungeonCell(Vector2I cellPosition)
	{
		return DungeonManager.Instance.DungeonGrid[cellPosition.X][cellPosition.Y];
	}
}

using System;
using System.Collections.Generic;

public static class GameManager
{
	private static readonly List<PlayerInfo> players = new();
	public static List<PlayerInfo> Players => players;
}

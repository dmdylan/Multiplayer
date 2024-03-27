using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MultiplayerController : Control
{
	[ExportCategory("Connection Info")]
	[Export] private string address = "127.0.0.1";
	[Export] private int port = 7777;
	
	[ExportCategory("Inputs")]
	[Export] private TextEdit nameInput;
	[Export] private Button hostButton;
	[Export] private Button joinButton;
	[Export] private Button leaveButton;
	[Export] private Button readyButton;
	[Export] private Button startButton;
	
	[ExportCategory("Lobby Info")]
	[Export] private PackedScene playerLobbyScene;
	[Export] private Panel playerLobbyPanel;
	[Export] private Control playerLobbyParentNode;

	ENetMultiplayerPeer peer;
	
	private Dictionary<int, PlayerLobbyController> playerLobbyNameplates = new();

	public override void _Ready()
	{	
		Multiplayer.PeerConnected += OnPeerConnected;
		Multiplayer.PeerDisconnected += OnPeerDisconnceted;
		Multiplayer.ConnectedToServer += OnConnectedToServer;
		Multiplayer.ConnectionFailed += OnConnectionFailed;
		Multiplayer.ServerDisconnected += OnServerDisconnected;
		
		hostButton.Pressed += OnHostButtonPressed;
		joinButton.Pressed += OnJoinButtonPressed;
		leaveButton.Pressed += OnLeaveButtonPressed;
		readyButton.Pressed += OnReadyButtonPressed;
		startButton.Pressed += OnStartButtonPressed;
	}

	public override void _ExitTree()
	{
		Multiplayer.PeerConnected -= OnPeerConnected;
		Multiplayer.PeerDisconnected -= OnPeerDisconnceted;
		Multiplayer.ConnectedToServer -= OnConnectedToServer;
		Multiplayer.ConnectionFailed -= OnConnectionFailed;
		Multiplayer.ServerDisconnected -= OnServerDisconnected;
		
		hostButton.Pressed -= OnHostButtonPressed;
		joinButton.Pressed -= OnJoinButtonPressed;
		leaveButton.Pressed -= OnLeaveButtonPressed;
		readyButton.Pressed -= OnReadyButtonPressed;
		startButton.Pressed -= OnStartButtonPressed;
	}
	
	private void HostGame()
	{
		peer = new();
		
		Error error = peer.CreateServer(port, 4);
		
		if(error != Error.Ok)
		{
			GD.Print($"Error cannot host: {error}");
			return;
		}
		
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		
		Multiplayer.MultiplayerPeer = peer;
		
		startButton.Visible = true;	
		
		GD.Print("Waiting for players...");
	}

	private void JoinGame()
	{
		peer = new();
		
		peer.CreateClient(address, port);
		
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print("Joining game...");
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SendPlayerInfo(string name, int id)
	{
		PlayerInfo playerInfo = new(id, name);
		
		if(!GameManager.Players.Contains(playerInfo))
			GameManager.Players.Add(playerInfo);
			
		if(Multiplayer.IsServer())
		{
			foreach (var player in GameManager.Players)
			{
				Rpc(nameof(SendPlayerInfo), player.Name, player.ID);
			}
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SpawnPlayerLobbyNameplate(int id)
	{		
		PlayerLobbyController playerLobbyController = playerLobbyScene.Instantiate<PlayerLobbyController>();
		
		PlayerInfo playerInfo = GameManager.Players.Where(x => x.ID == id).First();
		
		playerLobbyController.NameLabel.Text = playerInfo.Name;
		playerLobbyController.ReadyCheckBox.ButtonPressed = false;
		
		playerLobbyNameplates.Add(playerInfo.ID, playerLobbyController);
		
		playerLobbyParentNode.AddChild(playerLobbyController);
		
		if(Multiplayer.IsServer())
		{
			foreach (var player in playerLobbyNameplates)
			{
				Rpc(nameof(SpawnPlayerLobbyNameplate), player.Key);
			}
		}
	}
	
	private void RemovePlayerLobbyNameplate(int id)
	{
		playerLobbyNameplates[id].QueueFree();
		playerLobbyNameplates.Remove(id);
		
		// if(Multiplayer.IsServer())
		// {
		// 	foreach (var player in playerLobbyNameplates)
		// 	{
		// 		Rpc(nameof(RemovePlayerLobbyNameplate), player.Key);
		// 	}
		// }
	}

	private void ReadyUp()
	{
		
	}

	private void Disconnect()
	{		
		Multiplayer.MultiplayerPeer = null;
		
		foreach (var playerLobbyNameplate in playerLobbyNameplates)
		{
			playerLobbyNameplate.Value.QueueFree();
		}
		
		playerLobbyNameplates.Clear();
	}

	#region Mutliplayer Callbacks
	
	private void OnConnectionFailed()
	{
	}

	private void OnConnectedToServer()
	{
		RpcId(1, nameof(SendPlayerInfo), nameInput.Text, Multiplayer.GetUniqueId());
		RpcId(1, nameof(SpawnPlayerLobbyNameplate), Multiplayer.GetUniqueId());	
	}

	private void OnPeerDisconnceted(long id)
	{
		GD.Print("Peer disconnected");
		GameManager.Players.Remove(GameManager.Players.Where(i => i.ID == id).First());
		RemovePlayerLobbyNameplate((int)id);
	}

	private void OnPeerConnected(long id)
	{
		
	}
	
	private void OnServerDisconnected()
	{
		Multiplayer.MultiplayerPeer = null;
		GameManager.Players.Clear();
		playerLobbyNameplates.Clear();
	}
	
	#endregion Multiplayer Callbacks

	#region Button Callbacks
	
	private void OnHostButtonPressed()
	{
		HostGame();
		SendPlayerInfo(nameInput.Text, 1);
		SpawnPlayerLobbyNameplate(1);		
				
		hostButton.Visible = false;
		joinButton.Visible = false;
		nameInput.Visible = false;
		playerLobbyPanel.Visible = true;
		readyButton.Visible = true;
		leaveButton.Visible = true;
	}
	
	private void OnJoinButtonPressed()
	{
		JoinGame();	
		
		hostButton.Visible = false;
		joinButton.Visible = false;
		nameInput.Visible = false;
		playerLobbyPanel.Visible = true;
		readyButton.Visible = true;
		leaveButton.Visible = true;
	}
	
	private void OnStartButtonPressed()
	{
		
	}

	private void OnReadyButtonPressed()
	{		
		foreach (var player in GameManager.Players)
		{
			GD.Print($"Player: \n ID: {player.ID} \n Name: {player.Name}");
		}
	}

	private void OnLeaveButtonPressed()
	{
		Disconnect();
			
		hostButton.Visible = true;
		joinButton.Visible = true;
		nameInput.Visible = true;
		playerLobbyPanel.Visible = false;
		readyButton.Visible = false;
		leaveButton.Visible = false;
	}
	
	#endregion Button Callbacks
}
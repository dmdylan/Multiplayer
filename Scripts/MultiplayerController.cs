using Godot;
using System;

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

	public override void _Ready()
	{	
		Multiplayer.PeerConnected += OnPeerConnected;
		Multiplayer.PeerDisconnected += OnPeerDisconnceted;
		Multiplayer.ConnectedToServer += OnConnectedToServer;
		Multiplayer.ConnectionFailed += OnConnectionFailed;
		
		hostButton.Pressed += OnHostButtonPressed;
		joinButton.Pressed += OnJoinButtonPressed;
		leaveButton.Pressed += OnLeaveButtonPressed;
		readyButton.Pressed += OnReadyButtonPressed;
		startButton.Pressed += OnStartButtonPressed;
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

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = false)]
	private void SendPlayerInfo(string name, int id)
	{
		PlayerInfo playerInfo = new(id, name, false);
		
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

	#region Mutliplayer Callbacks
	
	private void OnConnectionFailed()
	{
	}

	private void OnConnectedToServer()
	{
		RpcId(1, nameof(SendPlayerInfo), nameInput.Text, Multiplayer.GetUniqueId());	
	}

	private void OnPeerDisconnceted(long id)
	{
	}

	private void OnPeerConnected(long id)
	{
	}
	
	#endregion Multiplayer Callbacks

	#region Button Callbacks
	
	private void OnHostButtonPressed()
	{
		HostGame();
		SendPlayerInfo(nameInput.Text, 1);
				
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
			GD.Print($"Player: ID: {player.ID} /n Name: {player.Name} /n Ready: {player.IsReady}");
		}
	}

	private void OnLeaveButtonPressed()
	{
		throw new NotImplementedException();
	}
	
	#endregion Button Callbacks
}
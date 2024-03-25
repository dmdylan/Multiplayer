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

	#region Mutliplayer Callbacks
	
	private void OnConnectionFailed()
	{
	}

	private void OnConnectedToServer()
	{
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
		throw new NotImplementedException();
	}

	private void OnReadyButtonPressed()
	{
		throw new NotImplementedException();
	}

	private void OnLeaveButtonPressed()
	{
		throw new NotImplementedException();
	}
	
	#endregion Button Callbacks
}
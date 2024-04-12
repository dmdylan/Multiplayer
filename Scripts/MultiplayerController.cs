using Godot;
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
	
	[ExportCategory("Character Select")]
	[Export] private VBoxContainer playerCharacterSelection;
	[Export] private Button warriorButton;
	[Export] private Button mageButton;
	[Export] private Button clericButton;
	[Export] private Button rogueButton;
	
	[ExportCategory("Lobby Info")]
	[Export] private PackedScene playerLobbyScene;
	[Export] private Panel playerLobbyPanel;
	[Export] private Control playerLobbyParentNode;

	ENetMultiplayerPeer peer;
	
	private Dictionary<int, PlayerLobbyNameplate> playerLobbyNameplates = new();

	public override void _EnterTree()
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
		
		warriorButton.Pressed += OnWarriorButtonPressed;
		mageButton.Pressed += OnMageButtonPressed;
		clericButton.Pressed += OnClericButtonPressed;
		rogueButton.Pressed += OnRogueButtonPressed;
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
		
		warriorButton.Pressed -= OnWarriorButtonPressed;
		mageButton.Pressed -= OnMageButtonPressed;
		clericButton.Pressed -= OnClericButtonPressed;
		rogueButton.Pressed -= OnRogueButtonPressed;
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
		
		if(!GameManager.Instance.Players.Contains(playerInfo))
			GameManager.Instance.Players.Add(playerInfo);
			
		if(Multiplayer.IsServer())
		{
			foreach (var player in GameManager.Instance.Players)
			{
				Rpc(nameof(SendPlayerInfo), player.Name, player.ID);
			}
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SpawnPlayerLobbyNameplate(int id, bool isReady)
	{	
		if(playerLobbyNameplates.ContainsKey(id))
			return;
			
		PlayerLobbyNameplate playerLobbyNameplate = playerLobbyScene.Instantiate<PlayerLobbyNameplate>();
		
		PlayerInfo playerInfo = GameManager.Instance.Players.Where(x => x.ID == id).First();
		
		playerLobbyNameplate.NameLabel.Text = playerInfo.Name;
		playerLobbyNameplate.ReadyCheckBox.ButtonPressed = isReady;
		
		playerLobbyNameplates.Add(playerInfo.ID, playerLobbyNameplate);
		
		playerLobbyParentNode.AddChild(playerLobbyNameplate);
		
		if(Multiplayer.IsServer())
		{
			foreach (var player in playerLobbyNameplates)
			{
				Rpc(nameof(SpawnPlayerLobbyNameplate), player.Key, player.Value.ReadyCheckBox.ButtonPressed);
			}
		}
	}
	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void RemovePlayerLobbyNameplate(int id)
	{
		playerLobbyNameplates[id].QueueFree();
		playerLobbyNameplates.Remove(id);
		
		if(Multiplayer.IsServer())
		{
			foreach (var player in playerLobbyNameplates)
			{
				Rpc(nameof(RemovePlayerLobbyNameplate), player.Key);
			}
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void ReadyUp(int id)
	{
		playerLobbyNameplates[id].ReadyCheckBox.ButtonPressed = !playerLobbyNameplates[id].ReadyCheckBox.ButtonPressed;
		
		if(Multiplayer.IsServer())
			CheckIfEveryoneIsReady();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void CheckIfEveryoneIsReady()
	{
		int readyCount = 0;
		
		foreach (var player in playerLobbyNameplates)
		{
			if(player.Value.ReadyCheckBox.ButtonPressed)
				readyCount++;
		}
		
		if(readyCount == playerLobbyNameplates.Count)
			startButton.Disabled = false;
		else
			startButton.Disabled = true;
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

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void SetPlayerCharacterClass(int id, string className)
	{		
		playerLobbyNameplates[id].CurrentCharacterClassLabel.Text = className;
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void StartGame()
	{
		GameManager.Instance.SpawnPlayerEntities(playerLobbyNameplates[Multiplayer.GetUniqueId()].CurrentCharacterClassLabel.Text);
		GameManager.Instance.StartGame();
		Hide();
	}

	#region Mutliplayer Callbacks

	private void OnConnectionFailed()
	{
		GD.Print("Connection failed");
	}

	private void OnConnectedToServer()
	{
		RpcId(1, nameof(SendPlayerInfo), nameInput.Text, Multiplayer.GetUniqueId());
		RpcId(1, nameof(SpawnPlayerLobbyNameplate), Multiplayer.GetUniqueId(), false);	
		RpcId(1, nameof(CheckIfEveryoneIsReady));
	}

	private void OnPeerDisconnceted(long id)
	{
		GD.Print("Peer disconnected");
		GameManager.Instance.Players.Remove(GameManager.Instance.Players.Where(i => i.ID == id).First());
	}

	private void OnPeerConnected(long id)
	{
	}
	
	private void OnServerDisconnected()
	{
		Multiplayer.MultiplayerPeer = null;
		GameManager.Instance.Players.Clear();
		playerLobbyNameplates.Clear();
	}
	
	#endregion Multiplayer Callbacks

	#region Button Callbacks
	
	private void OnHostButtonPressed()
	{
		HostGame();
		SendPlayerInfo(nameInput.Text, 1);
		SpawnPlayerLobbyNameplate(1, false);		
				
		hostButton.Visible = false;
		joinButton.Visible = false;
		nameInput.Visible = false;
		playerLobbyPanel.Visible = true;
		playerCharacterSelection.Visible = true;
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
		playerCharacterSelection.Visible = true;
		readyButton.Visible = true;
		leaveButton.Visible = true;
	}
	
	private void OnStartButtonPressed()
	{
		Rpc(nameof(StartGame));
	}

	private void OnReadyButtonPressed()
	{
		ReadyUp(Multiplayer.GetUniqueId());
		Rpc(nameof(ReadyUp), Multiplayer.GetUniqueId());
	}

	private void OnLeaveButtonPressed()
	{
		RpcId(1, nameof(RemovePlayerLobbyNameplate), Multiplayer.GetUniqueId());	
		Disconnect();
			
		hostButton.Visible = true;
		joinButton.Visible = true;
		nameInput.Visible = true;
		playerLobbyPanel.Visible = false;
		playerCharacterSelection.Visible = false;
		readyButton.Visible = false;
		leaveButton.Visible = false;
	}
	
	private void OnWarriorButtonPressed()
	{
		Rpc(nameof(SetPlayerCharacterClass), Multiplayer.GetUniqueId(), "Warrior");
	}

	private void OnMageButtonPressed()
	{
		Rpc(nameof(SetPlayerCharacterClass), Multiplayer.GetUniqueId(), "Mage");
	}

	private void OnClericButtonPressed()
	{
		Rpc(nameof(SetPlayerCharacterClass), Multiplayer.GetUniqueId(), "Cleric");
	}

	private void OnRogueButtonPressed()
	{
		Rpc(nameof(SetPlayerCharacterClass), Multiplayer.GetUniqueId(), "Rogue");
	}

	#endregion Button Callbacks
}
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks, IInRoomCallbacks
{
    private static PhotonManager instance = null;

    public AppSettings appSettings = new AppSettings();

    public LoadBalancingClient client;


    public LobbyManager lobbyManager;
    public List<RoomInfo> PMroomList = new List<RoomInfo>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        if (client != null)
        {
            //#######ÇÊ¼ö########
            client.Service();

            //Debug.Log(client.State.ToString());
            //Debug.Log(client.CurrentRoom.Players.Count);
        }
    }

    public static PhotonManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    public void ConnectWithOption(string nickName)
    {
        client = new LoadBalancingClient();

        client.NickName = nickName;
        client.ConnectUsingSettings(appSettings);

        client.AddCallbackTarget(this);
    }

    private void OnApplicationQuit()
    {
        if (client != null && client.IsConnected)
            client.Disconnect();
    }

    //IConnectionCallbacks
    public void OnConnected()
    {
        
    }

    public void OnConnectedToMaster()
    {
        client.OpJoinLobby(TypedLobby.Default);
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        
    }

    //IMatchmakingCallbacks
    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        
    }

    public void OnCreatedRoom()
    {
        
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinedRoom()
    {
        SceneManager.LoadSceneAsync("Room 1");
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        
    }

    public void OnLeftRoom()
    {
        //client.OpJoinLobby(TypedLobby.Default);
    }

    //ILobbyCallbacks
    public void OnJoinedLobby()
    {
        SceneManager.LoadSceneAsync("Lobby");
    }

    public void OnLeftLobby()
    {
        client.Disconnect();

        SceneManager.LoadSceneAsync("Launcher");
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        PMroomList = roomList;

        if (lobbyManager == null)
            return;

        lobbyManager.LMroomList = PMroomList;
        lobbyManager.DeleteAllPanel();
        lobbyManager.CreatePanel();
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        
    }

    //IInRoomCallbacks
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
        
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }

    public void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
        
    }

    public void OnGUI()
    {
        if (client != null)
            GUI.TextArea(new Rect(0, 0, 300, 30), client.State.ToString());
    }
}

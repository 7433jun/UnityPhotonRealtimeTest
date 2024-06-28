using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class TestClient : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks, IInRoomCallbacks, IOnEventCallback
{
    private LoadBalancingClient client;

    public Text clientStateText;
    public Text clientUserID;
    public Text currentLobby;

    void Start()
    {
        client = new LoadBalancingClient();
        client.AppId = "33712f15-76be-4e8c-83f4-d85827acc2f7";
        bool connectInProcess = client.ConnectToRegionMaster("kr");

        client.AddCallbackTarget(this);
    }

    
    void Update()
    {
        client.Service();

        clientStateText.text = $"Client State : {client.State}";
        Debug.Log(client.State.ToString());

        if (client.InLobby)
        {
            clientUserID.text = $"Client UserID : {client.UserId}";
            currentLobby.text = $"Current Lobby : {client.CurrentLobby}";
        }
    }

    private void OnApplicationQuit()
    {
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
        
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        
    }

    public void OnLeftRoom()
    {
       
    }

    //ILobbyCallbacks
    public void OnJoinedLobby()
    {
        
    }

    public void OnLeftLobby()
    {
        
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        
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

    //IOnEventCallback
    public void OnEvent(EventData photonEvent)
    {
        
    }
}

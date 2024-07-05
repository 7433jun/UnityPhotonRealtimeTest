using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private InputField searchRoomName;
    [SerializeField] private InputField roomName;
    [SerializeField] private InputField maxPlayerCount;
    [SerializeField] private Transform listContent;
    [SerializeField] private GameObject roomPanel;

    [SerializeField] private Text ID;
    [SerializeField] private Text inLobbyCount;
    [SerializeField] private Text inRoomCount;

    public List<RoomInfo> LMroomList;

    void Start()
    {
        PhotonManager.Instance.lobbyManager = this;

        LMroomList = PhotonManager.Instance.PMroomList;
        CreatePanel();
    }

    void Update()
    {
        ID.text = $"Client UserID : {PhotonManager.Instance.client.UserId}";
        inLobbyCount.text = $"Client InLobby Count : {PhotonManager.Instance.client.PlayersOnMasterCount}";
        inRoomCount.text = $"Client InRoom Count : {PhotonManager.Instance.client.PlayersInRoomsCount}";
    }

    public void SearchRoomButton()
    {
        DeleteAllPanel();

        List<RoomInfo> searchedRoomList = new List<RoomInfo>();

        // 사용가능한 룸만 추출 & 룸 검색
        foreach (var info in LMroomList)
        {
            if (!info.RemovedFromList && info.Name.Contains(searchRoomName.text))
            {
                searchedRoomList.Add(info);
            }
        }

        // 룸 패널 생성 및 초기화
        foreach (RoomInfo info in searchedRoomList)
        {
            Instantiate(roomPanel, listContent).GetComponent<RoomPanel>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }

    public void CreateRoomButton()
    {
        EnterRoomParams enterRoomParams = new EnterRoomParams();
        enterRoomParams.RoomOptions = new RoomOptions();

        enterRoomParams.RoomName = roomName.text;
        enterRoomParams.RoomOptions.MaxPlayers = int.Parse(maxPlayerCount.text);
        enterRoomParams.RoomOptions.IsOpen = true;
        enterRoomParams.RoomOptions.IsVisible = true;

        PhotonManager.Instance.client.OpCreateRoom(enterRoomParams);
    }

    public void ExitLobbyButton()
    {
        PhotonManager.Instance.client.OpLeaveLobby();
    }

    public void RefreshButton()
    {
        DeleteAllPanel();
        CreatePanel();
    }

    public void DeleteAllPanel()
    {
        foreach (Transform transform in listContent)
        {
            Destroy(transform.gameObject);
        }
    }

    public void CreatePanel()
    {
        List<RoomInfo> availableRoomList = new List<RoomInfo>();

        // 사용가능한 룸만 추출
        foreach (var info in LMroomList)
        {
            if (!info.RemovedFromList)
            {
                availableRoomList.Add(info);
            }
        }

        // 룸 패널 생성 및 초기화
        foreach (RoomInfo info in availableRoomList)
        {
            Instantiate(roomPanel, listContent).GetComponent<RoomPanel>().SetInfo(info.Name, info.PlayerCount, info.MaxPlayers);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomPanel : MonoBehaviour
{
    [SerializeField] private Text panelRoomName;
    [SerializeField] private Text panelPlayerCount;
    private string roomName;

    public void JoinRoomButton()
    {
        EnterRoomParams enterRoomParams = new EnterRoomParams();
        enterRoomParams.RoomName = roomName;

        PhotonManager.Instance.client.OpJoinRoom(enterRoomParams);
    }

    public void SetInfo(string name, int current, int max)
    {
        roomName = name;
        panelRoomName.text = $"Name : {name}";
        panelPlayerCount.text = $"{current} / {max}";
    }
}

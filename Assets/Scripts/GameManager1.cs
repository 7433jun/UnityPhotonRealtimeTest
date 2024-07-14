using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameManager1 : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    private int roomNumber = 1;
    private Player localPlayer;

    private Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();

    void Start()
    {
        foreach (Player p in PhotonManager.Instance.client.CurrentRoom.Players.Values)
        {
            if (p.IsLocal)
            {
                localPlayer = p;
            }
        }
    }

    void Update()
    {
        CharacterUpdate();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Hashtable customProperties = new Hashtable();
            customProperties["PlayerScene"] = 2;
            localPlayer.SetCustomProperties(customProperties);

            SceneManager.LoadSceneAsync("Room 2");
        }
    }

    private void CharacterUpdate()
    {
        if (!PhotonManager.Instance.client.InRoom) return;

        lock (PhotonManager.Instance)
        {
            Dictionary<int, Player> currentScenePlayers = PhotonManager.Instance.client.CurrentRoom.Players.Where(kvp => (int)kvp.Value.CustomProperties["PlayerScene"] == roomNumber).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // 플레이어 캐릭터 생성
            foreach (Player p in currentScenePlayers.Values)
            {
                GameObject character = null;

                bool found = characters.TryGetValue(p.ActorNumber, out character);

                if (!found)
                {
                    character = Instantiate(playerPrefab);
                    character.GetComponent<ClientPlayer>().player = p;
                    characters.Add(p.ActorNumber, character);
                }
            }

            // 플레이어 캐릭터 삭제
            if (characters.Count != currentScenePlayers.Count)
            {
                HashSet<int> keysToRemove = new HashSet<int>();

                foreach (int characterKey in characters.Keys)
                {
                    if (!currentScenePlayers.Keys.Contains(characterKey))
                    {
                        keysToRemove.Add(characterKey);
                    }
                }

                foreach (int i in keysToRemove)
                {
                    Destroy(characters[i].gameObject);
                    characters.Remove(i);
                }
            }
        }
    }

    public void ExitRoomButton()
    {
        bool clean = PhotonManager.Instance.client.CurrentRoom.AutoCleanUp;
        bool deleteNull = PhotonManager.Instance.client.CurrentRoom.DeleteNullProperties;

        PhotonManager.Instance.client.OpLeaveRoom(false);
    }
}

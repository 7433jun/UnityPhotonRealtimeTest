using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    private Dictionary<int, GameObject> characters = new Dictionary<int, GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        CharacterSetting();
    }

    private void CharacterSetting()
    {
        if (!PhotonManager.Instance.client.InRoom) return;

        lock (PhotonManager.Instance)
        {
            // 플레이어 캐릭터 생성
            foreach (Player p in PhotonManager.Instance.client.CurrentRoom.Players.Values)
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
            if (characters.Count != PhotonManager.Instance.client.CurrentRoom.Players.Count)
            {
                HashSet<int> keysToRemove = new HashSet<int>();
                foreach (int characterKey in characters.Keys)
                {
                    if (!PhotonManager.Instance.client.CurrentRoom.Players.Keys.Contains(characterKey))
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

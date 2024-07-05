using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    [SerializeField] InputField nickName;

    void Start()
    {
        nickName.text = PlayerPrefs.GetString("nickName");
    }

    public void Connect()
    {
        if (nickName.text == string.Empty)
        {
            Debug.Log("닉네임을 입력하세요");
        }
        else
        {
            PlayerPrefs.SetString("nickName", nickName.text);

            PhotonManager.Instance.ConnectWithOption(nickName.text);
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

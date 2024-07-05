using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Hashtable = ExitGames.Client.Photon.Hashtable;


public class ClientPlayer : MonoBehaviour, IOnEventCallback
{
    public Player player;

    private Animator animator;

    public float speed = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();

        PhotonManager.Instance.client.AddCallbackTarget(this);
    }

    void Update()
    {
        if (!player.IsLocal) return;
        if (!PhotonManager.Instance.client.InRoom) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += new Vector3(h, 0, v).normalized * speed * Time.deltaTime;

        PhotonManager.Instance.client.OpRaiseEvent(1, WriteEvMove(), new RaiseEventOptions(), new SendOptions() { Reliability = true });
    }

    public Hashtable WriteEvMove()
    {
        Hashtable evContent = new Hashtable();
        evContent[(byte)0] = new float[] { transform.position.x, transform.position.z };
        return evContent;
    }

    public void ReadEvMove(Hashtable evContent)
    {
        if (evContent.ContainsKey((byte)0))
        {
            float[] posArray = (float[])evContent[(byte)0];
            transform.position = new Vector3(posArray[0], 0, posArray[1]);
        }
        else if (evContent.ContainsKey("0"))
        {
            var posArray = (object[])evContent["0"];
            transform.position = new Vector3(Convert.ToSingle(posArray[0]), 0, Convert.ToSingle(posArray[1]));
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Sender <= 0)
            return;

        if (player.ActorNumber == photonEvent.Sender)
        {
            switch (photonEvent.Code)
            {
                case 1:
                    ReadEvMove((Hashtable)photonEvent.CustomData);
                    break;
            }
        }
    }

    public void OnDestroy()
    {
        if (PhotonManager.Instance.client != null)
            PhotonManager.Instance.client.RemoveCallbackTarget(this);
    }
}

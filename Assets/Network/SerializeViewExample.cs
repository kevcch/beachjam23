using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeViewExample : MonoBehaviourPun, IPunObservable
{
    [Header("Configuration")]
    [SerializeField] int SendRate = 20;
    [SerializeField] int SerializationRate = 10; 

    public void Start()
    {
        PhotonNetwork.SendRate = SendRate;
        PhotonNetwork.SerializationRate = SerializationRate; 
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            Debug.Log("I am the owner! Sending Message: " + transform.position);
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            Vector3 updatePos = (Vector3)stream.ReceiveNext();
            transform.position = updatePos;
            Debug.Log("I am the recvr! Got Message: " + updatePos);
        }
    }
}

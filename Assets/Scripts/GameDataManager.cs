using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameDataManager : MonoBehaviour, IPunObservable
{
    public static GameDataManager instance;
    public int currency = 50;
    public LightingManager lightingManager;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }
    private void Start()
    {
        lightingManager = GameObject.Find("DayNightLight").GetComponent<LightingManager>();
    }
    public float GetTimeOfDay() {
        return lightingManager.TimeOfDay;
    }

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(currency);
        }
        else
        {
            // Network player, receive data
            this.currency = (int)stream.ReceiveNext();
        }
    }

    #endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton instance;
    public AudioSource audioSource;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
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

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Clock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI clockText;

    private void Update()
    {
        float totalTime = GameDataManager.instance.GetTimeOfDay();
        TimeSpan time = TimeSpan.FromHours(totalTime);
        clockText.text = time.ToString("hh':'mm");


    }

}

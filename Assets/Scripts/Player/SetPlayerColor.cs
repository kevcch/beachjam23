using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerColor : MonoBehaviour
{
    public List<GameObject> models;
    void Awake()
    {
        int i = Random.Range(0, models.Count);
        models[i].SetActive(true);
    }
}

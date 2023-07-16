using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    Health HealthManager;

    private void Start()
    {
        HealthManager = GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthPickup>() != null)
        {
            HealthManager.NetworkChangeHealth(3);
            collision.gameObject.GetComponent<HealthPickup>().NetworkDestroy();
        }
    }
}

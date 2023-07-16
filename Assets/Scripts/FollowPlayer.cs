using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Vector3 Offset;
    [SerializeField] Transform DefaultPos;
    [SerializeField] float DefaultZoom = 11f;
    [SerializeField] float FollowZoom = 5f;

    [HideInInspector]
    public GameObject PlayerObject;

    Camera thisCam;
    GameObject SprintUI;
    private void Start()
    {
        thisCam = GetComponent<Camera>();
        SprintUI = GameObject.FindWithTag("Sprint_UI");
    }

    private void Update()
    {
        if(PlayerObject != null)
        {
            transform.position = PlayerObject.transform.position + Offset;
            thisCam.orthographicSize = FollowZoom;
        }
        else
        {
            transform.position = DefaultPos.position + Offset;
            thisCam.orthographicSize = DefaultZoom;
            SprintUI.SetActive(false);
        }
    }
}

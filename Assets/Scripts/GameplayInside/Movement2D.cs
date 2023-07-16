using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement2D : MonoBehaviourPun
{
    [SerializeField] float movespeed = 1f;

    private bool isMoving = false;

    void Update()
    {
        if (base.photonView.IsMine)
        {
            if(!isMoving) {
                Vector3 targetPos = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") , transform.position.y + Input.GetAxisRaw("Vertical"), 0);
                StartCoroutine(Move(targetPos));
            }
        }
        else
        {
            transform.position += Vector3.zero;
        }
    }

    IEnumerator Move(Vector3 targetPos) {
        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
}

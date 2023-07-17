using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement2D : MonoBehaviourPun
{
    [SerializeField] float movespeed = 1f;

    private bool isMoving = false;
    private LayerMask layerMask;

    void Start() {
        layerMask = LayerMask.GetMask("InsideFront");
    }

    void Update()
    {
        if (base.photonView.IsMine)
        {
            if(!isMoving) {
                Vector3 targetPos = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") , transform.position.y + Input.GetAxisRaw("Vertical"), 0);
                if(isWalkable(targetPos)) {
                    StartCoroutine(Move(targetPos));
                }
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

    private bool isWalkable(Vector3 targetPos) {
        if(Physics2D.OverlapCircle(targetPos, 0.3f, layerMask) != null) {
            return false;
        }
        return true;
    }
}

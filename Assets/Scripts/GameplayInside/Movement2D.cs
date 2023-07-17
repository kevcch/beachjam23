using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement2D : MonoBehaviourPun
{
    [SerializeField] float movespeed = 1f;

    private bool isMoving = false;
    private ContactFilter2D filter = new ContactFilter2D();

    void Start() {
        filter.SetLayerMask(LayerMask.GetMask("InsideFront"));
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
        List<Collider2D> results = new List<Collider2D>();
        if(Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), filter, results) > 0) {
            return false;
        }
        return true;
    }
}

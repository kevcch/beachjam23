using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Vector3 previousPos;
    private void Start()
    {
        animator = transform.Find("PlayerModel").gameObject.GetComponent<Animator>();
        previousPos = transform.position;
    }
    private void FixedUpdate()
    {
        float movementMagnitude = (transform.position - previousPos).magnitude;
        animator.SetFloat("speed", movementMagnitude);
        previousPos = transform.position;
    }

}

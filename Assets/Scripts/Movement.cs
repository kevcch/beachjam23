using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class Movement : MonoBehaviourPun
{
    [SerializeField] float movespeed = 200f;

    new Rigidbody rigidbody;

    private Vector2 inputVal = Vector2.zero;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (base.photonView.IsMine)
        {
            rigidbody.velocity = new Vector3(inputVal.x * movespeed, 0f, inputVal.y * movespeed);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
    void OnMove(InputValue value)
    {
        inputVal = value.Get<Vector2>();
    }
}

using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviourPun
{
    [SerializeField] float movespeed = 200f;

    new Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (base.photonView.IsMine)
        {
            rigidbody.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * movespeed, 0f, Input.GetAxisRaw("Vertical") * movespeed);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}

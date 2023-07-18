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
        TruckManager.instance.playerChangeEvent.AddListener(FixInput);
    }
    void FixInput() {
        if (!base.photonView.IsMine && gameObject.GetComponent<PlayerInput>().enabled)
        {
            gameObject.GetComponent<PlayerInput>().enabled = false;
        }
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

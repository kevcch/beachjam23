using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] float movementForce = 200f;
    [SerializeField] float counterMovementForce = 20f;
    [SerializeField] float rotationSpeed = 10f;
    Vector3 movementDirection;
    new Rigidbody rigidbody;
    private Vector2 inputVal = Vector2.zero;
    private GameObject playerModel;

    private bool drivingVehicle = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerModel = transform.Find("PlayerModel").gameObject;
    }

    void FixedUpdate()
    {
        if (base.photonView.IsMine)
        {
            if (!drivingVehicle) {
                movementDirection = new Vector3(inputVal.x, 0, inputVal.y);
                movementDirection.Normalize();
                Vector3 counterMovementDirection = new Vector3(-rigidbody.velocity.x, 0, -rigidbody.velocity.z);
                rigidbody.AddForce(movementDirection * movementForce +
                    counterMovementDirection * counterMovementForce);
                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    playerModel.transform.rotation = Quaternion.RotateTowards(playerModel.transform.rotation, toRotation, rotationSpeed);
                }
            }
        }
    }



    void OnMove(InputValue value)
    {
        inputVal = value.Get<Vector2>();
    }
}

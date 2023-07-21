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

    public bool drivingVehicle = false;
    [SerializeField] public Transform vehicleTransform;

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
            if (drivingVehicle) {
                transform.position = vehicleTransform.position;
                EventBus.Publish<MoveVehicleEvent>(new MoveVehicleEvent(inputVal));
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    VehicleMode(false, null);
                    EventBus.Publish<VehicleActivationEvent>(new VehicleActivationEvent(false));

                }
            }
        }
    }

    public void VehicleMode(bool vehicleMode, Transform _vehicleTransform) {
        if (vehicleMode)
        {
            gameObject.GetComponent<Collider>().isTrigger = vehicleMode;
            rigidbody.useGravity = false;
            drivingVehicle = vehicleMode;
            vehicleTransform = _vehicleTransform;
        }
        else {
            gameObject.GetComponent<Collider>().isTrigger = vehicleMode;
            rigidbody.useGravity = true;
            drivingVehicle = vehicleMode;
        }
        
    }

    void OnMove(InputValue value)
    {
        inputVal = value.Get<Vector2>();
    }
}

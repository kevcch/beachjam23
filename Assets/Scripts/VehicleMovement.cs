using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] float movementForce = 200f;
    [SerializeField] float counterMovementForce = 20f;
    [SerializeField] float rotationSpeed = 10f;
    Vector3 movementDirection;
    new Rigidbody rigidbody;
    private Vector2 inputVal = Vector2.zero;
    public GameObject truckModel;

    public bool vehicleActive = false;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        EventBus.Subscribe<MoveVehicleEvent>(OnMoveVehicleEvent);
        EventBus.Subscribe<VehicleActivationEvent>(OnVehicleActivationEvent);
    }

    void FixedUpdate()
    {
        if (vehicleActive) {
            movementDirection = new Vector3(inputVal.x, 0, inputVal.y);
            movementDirection.Normalize();
            Vector3 counterMovementDirection = new Vector3(-rigidbody.velocity.x, 0, -rigidbody.velocity.z);
            rigidbody.AddForce(movementDirection * movementForce +
                counterMovementDirection * counterMovementForce);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                truckModel.transform.rotation = Quaternion.RotateTowards(truckModel.transform.rotation, toRotation, rotationSpeed);
            }
        }
    }

    void OnMoveVehicleEvent(MoveVehicleEvent e) {
        inputVal = e.inputVal;
    }
    void OnVehicleActivationEvent(VehicleActivationEvent e) {
        vehicleActive = e.vehicleActive;
    }
}
public class MoveVehicleEvent {
    public Vector2 inputVal;
    public MoveVehicleEvent(Vector2 _inputVal)
    {
        inputVal = _inputVal;
    }
}
public class VehicleActivationEvent
{
    public bool vehicleActive;
    public VehicleActivationEvent(bool _vehicleActive)
    {
        vehicleActive = _vehicleActive;
    }
}

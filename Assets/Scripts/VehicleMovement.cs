using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class VehicleMovement : MonoBehaviour
{
    [SerializeField] float movementForce = 200f;
    [SerializeField] float counterMovementForce = 20f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float maxBackwardsSpeed = 10f;
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
            // Vector3 truckDirection = transform.rotation * Vector3.forward;
            // movementDirection = truckDirection * inputVal.y;
            // movementDirection.Normalize();
            // Vector3 counterMovementDirection = new Vector3(-rigidbody.velocity.x, 0, -rigidbody.velocity.z);
            // // rigidbody.AddForce(movementDirection * movementForce +
            // //     counterMovementDirection * counterMovementForce);
            // if (movementDirection != Vector3.zero)
            // {
            //     Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            //     truckModel.transform.rotation = Quaternion.RotateTowards(truckModel.transform.rotation, toRotation, rotationSpeed);
            // }

            Quaternion rotate = Quaternion.Euler(0, rotationSpeed * inputVal.x * Time.fixedDeltaTime, 0);
            transform.rotation *= rotate;
            
            Vector3 a = transform.rotation * Vector3.forward;
            a.y = 0;
            Vector3 b = rigidbody.velocity;
            b.y = 0;
            float sign = Mathf.Sign(Vector3.Dot(a, b));
            float mag = Mathf.Clamp(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).magnitude * sign, -maxBackwardsSpeed, maxSpeed);
            float cmf = counterMovementForce * sign;
            mag += (inputVal.y * movementForce - cmf) * Time.fixedDeltaTime;
            Vector3 vel = transform.rotation * Vector3.forward * mag;
            vel.y = rigidbody.velocity.y;
            rigidbody.velocity = vel;
            // rigidbody.angularVelocity = Vector3.zero;

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    void OnMoveVehicleEvent(MoveVehicleEvent e) {
        inputVal = e.inputVal;
    }
    void OnVehicleActivationEvent(VehicleActivationEvent e) {
        vehicleActive = e.vehicleActive;
        if(vehicleActive)
            AudioSingleton.instance.audioSource.PlayOneShot(
                            Resources.Load("Audio/Truck/truckStartingUpCut") as AudioClip);
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

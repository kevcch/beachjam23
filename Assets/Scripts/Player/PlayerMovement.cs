using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] float movementForce = 200f;
    [SerializeField] float counterMovementForce = 20f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float maxSpeed = 20f;
    [SerializeField] float maxBackwardsSpeed = 10f;
    Vector3 movementDirection;
    new Rigidbody rigidbody;
    private Vector2 inputVal = Vector2.zero;
    private GameObject playerModel;

    public bool drivingVehicle = false;
    public bool insideVehicle = false;
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
                if(insideVehicle) {
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
                else {
                    Quaternion rotate = Quaternion.Euler(0, rotationSpeed * inputVal.x * Time.fixedDeltaTime, 0);
                    playerModel.transform.rotation *= rotate;
                    
                    Vector3 a = playerModel.transform.rotation * Vector3.forward;
                    a.y = 0;
                    Vector3 b = rigidbody.velocity;
                    b.y = 0;
                    float sign = Mathf.Sign(Vector3.Dot(a, b));
                    float mag = Mathf.Clamp(new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).magnitude * sign, -maxBackwardsSpeed, maxSpeed);
                    float cmf = counterMovementForce * sign;
                    mag += (inputVal.y * movementForce - cmf) * Time.fixedDeltaTime;
                    Vector3 vel = playerModel.transform.rotation * Vector3.forward * mag;
                    vel.y = rigidbody.velocity.y;
                    rigidbody.velocity = vel;
                    playerModel.transform.rotation = Quaternion.Euler(0, playerModel.transform.rotation.eulerAngles.y, 0);
                }
            }
            if (drivingVehicle) {
                transform.position = vehicleTransform.position;
                transform.GetChild(0).rotation = vehicleTransform.rotation;
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
            //gameObject.GetComponent<Photon.Pun.Demo.PunBasics.CameraWork>().UpdateTargetTransform(_vehicleTransform);
            rigidbody.useGravity = false;
            drivingVehicle = vehicleMode;
            vehicleTransform = _vehicleTransform;
        }
        else {
            gameObject.GetComponent<Collider>().isTrigger = vehicleMode;
            //gameObject.GetComponent<Photon.Pun.Demo.PunBasics.CameraWork>().UpdateTargetTransform(transform);
            rigidbody.useGravity = true;
            drivingVehicle = vehicleMode;
        }
        
    }

    void OnMove(InputValue value)
    {
        inputVal = value.Get<Vector2>();
    }
}

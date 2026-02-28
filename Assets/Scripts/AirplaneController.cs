using Unity.VisualScripting;
using UnityEngine;
public class AirplaneController : MonoBehaviour
{   
    [Header("Centre of Mass")]
    [SerializeField] float centreOfMassX;
    [SerializeField] float centreOfMassY;
    [SerializeField] float centreOfMassZ;

    [Header("Throttle")]
    [SerializeField] float maxThrottle = 20000f;
    [SerializeField] float throttleAcceleration = 1000f;

    [Header("Rotation")]
    [SerializeField] float pitchStrength = 6000f;
    [SerializeField] float rollStrength = 8000f;
    [SerializeField] float yawStrength = 3000f;

    [Header("Lift")]
    [SerializeField] float liftFactor = 1;

    private Rigidbody rb;
    private float throttle;
    private AirplaneInputActions inputActions;
    public GameObject balloonPrefab;
    public Transform dropPoint;

    void Awake()
    {
        inputActions = new AirplaneInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Airplane.DropBalloon.performed += ctx => DropBalloon();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AdjustCentreOfMass();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyThrust();
        ApplyRotation();
        ApplyLift();
    }

    void ApplyThrust()
    {   
        float throttleInput = inputActions.Airplane.Throttle.ReadValue<float>();

        throttle += throttleInput * throttleAcceleration * Time.fixedDeltaTime;
        throttle = Mathf.Clamp(throttle, 0f, maxThrottle);

        rb.AddForce(transform.right * throttle);
    }

    void ApplyRotation()
    {
        float pitchInput = inputActions.Airplane.Pitch.ReadValue<float>();
        float rollInput = inputActions.Airplane.Roll.ReadValue<float>();
        float yawInput = inputActions.Airplane.Yaw.ReadValue<float>();

        Vector3 torque = new Vector3(-pitchInput * pitchStrength, yawInput * yawStrength, -rollInput * rollStrength);
        rb.AddRelativeTorque(torque);
    }

    void ApplyLift()
    {
        float speed = rb.linearVelocity.magnitude;
        float liftForce = speed * speed * 0.0001f * liftFactor;
        rb.AddForce(transform.up * liftForce);
    }
    void DropBalloon()
    {
        GameObject balloon = Instantiate(balloonPrefab, dropPoint.position, Quaternion.identity);
        Destroy(balloon, 10f);
    }
    private void AdjustCentreOfMass()
    {
        rb.centerOfMass = new Vector3(centreOfMassX, centreOfMassY, centreOfMassZ);
    }

}

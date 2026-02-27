using Unity.VisualScripting;
using UnityEngine;
public class AirplaneController : MonoBehaviour
{   
    Rigidbody rb;
    [SerializeField] float centreOfMassX;
    [SerializeField] float centreOfMassY;
    [SerializeField] float centreOfMassZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        AdjustCentreOfMass();
    }

    void Update()
    {
        
    }
    private void AdjustCentreOfMass()
    {
        rb.centerOfMass = new Vector3(centreOfMassX, centreOfMassY, centreOfMassZ);
    }
}

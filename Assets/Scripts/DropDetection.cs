using UnityEngine;

public class DropZone : MonoBehaviour
{   
    private Color zoneColour;
    private Color originalColour;
    void Start()
    {
        zoneColour = GetComponent<Renderer>().material.color;
        originalColour = zoneColour;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balloon"))
        {
            Debug.Log("Successful Drop!");
            Destroy(other.gameObject);
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            Invoke("ResetColour", 3f);
        }
    }
    void ResetColour()
    {
        gameObject.GetComponent<Renderer>().material.color = originalColour;
    }
}

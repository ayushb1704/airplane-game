using UnityEngine;
using System.IO;

public class CameraSwitcher : MonoBehaviour
{
    public Camera thirdPersonCamera;
    public Camera noseCamera;

    private AirplaneInputActions inputActions;
    private bool usingNoseCam = false;

    void Awake()
    {
        inputActions = new AirplaneInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Airplane.SwitchCamera.performed += ctx => ToggleCamera();
        inputActions.Airplane.CapturePhoto.performed += ctx => CapturePhoto();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        thirdPersonCamera.enabled = true;
        noseCamera.enabled = false;
    }

    void ToggleCamera()
    {
        usingNoseCam = !usingNoseCam;

        thirdPersonCamera.enabled = !usingNoseCam;
        noseCamera.enabled = usingNoseCam;
    }
    public void CapturePhoto()
    {
        string folderPath = Application.dataPath + "/CapturedPhotos/";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string fileName = "Photo_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
        string fullPath = Path.Combine(folderPath, fileName);

        ScreenCapture.CaptureScreenshot(fullPath);

        Debug.Log("Photo Saved: " + fullPath);
    }
}
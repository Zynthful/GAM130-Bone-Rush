using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera mainCamera;
    public float headHeight;
    public float minimumXAngle = -60f;
    public float maximumXAngle = 60f;
    public float MinimumYAngle = -360f;
    public float maximumYAngle = 360f;
    public float sensitivityX;
    public float sensitivityY;

    private float rotationX;
    private float rotationY;

    void Awake()
    {
        mainCamera.transform.position = new Vector3(gameObject.transform.position.x, headHeight, gameObject.transform.position.z);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        GetMouseMovement();
        PlayerCameraRotation();
    }

    private void GetMouseMovement()
    {
        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;
        rotationY += Input.GetAxis("Mouse X") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minimumXAngle, maximumXAngle);
    }

    private void PlayerCameraRotation()
    {
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        mainCamera.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera bottomCamera;
    public Camera topCamera;
    public Camera rightCamera;
    public Camera leftCamera;

    [SerializeField] private float cameraSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraCloser()
    {
        bottomCamera.transform.position = new Vector3(bottomCamera.transform.position.x, bottomCamera.transform.position.y + cameraSpeed ,bottomCamera.transform.position.z);
        topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y + cameraSpeed,topCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(rightCamera.transform.position.x, rightCamera.transform.position.y + cameraSpeed, rightCamera.transform.position.z);
        leftCamera.transform.position = new Vector3(leftCamera.transform.position.x, leftCamera.transform.position.y + cameraSpeed, leftCamera.transform.position.z);
    }

    public void CameraAway()
    {
        bottomCamera.transform.position = new Vector3(bottomCamera.transform.position.x, bottomCamera.transform.position.y - cameraSpeed, bottomCamera.transform.position.z);
        topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y - cameraSpeed, topCamera.transform.position.z);
        rightCamera.transform.position = new Vector3(rightCamera.transform.position.x, rightCamera.transform.position.y - cameraSpeed, rightCamera.transform.position.z);
        leftCamera.transform.position = new Vector3(leftCamera.transform.position.x, leftCamera.transform.position.y - cameraSpeed, leftCamera.transform.position.z);
    }
}

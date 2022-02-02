using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public float mouseSensitivity = 100.0f;

    private float rotY = 0.0f;

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}

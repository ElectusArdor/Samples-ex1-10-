using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform currentSportsman, cameraContainer;

    [SerializeField] private float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        cameraContainer.localRotation *= Quaternion.Euler(0, -Time.deltaTime * rotationSpeed, 0);
    }
}

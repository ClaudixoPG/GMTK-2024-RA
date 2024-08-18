using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class CinemachineCameraControl : MonoBehaviour
{

    private CameraInputActions cameraInputActions;

    private Vector2 lookInput;
    private float zoomInput;
    private float rotationStep = 90f;
    private float zoomSpeed = 0.01f;
    private float zoomMin = 10f;
    private float zoomMax = 20f;
    private float sensitivity = 1f;

    public CinemachineVirtualCamera virtualCamera;

    //private CinemachineTransposer transposer;

    private void Awake()
    {
        cameraInputActions = new CameraInputActions();

        cameraInputActions.Camera.Enable();

        cameraInputActions.Camera.Look.performed += Look_performed;
        cameraInputActions.Camera.Zoom.performed += Zoom_performed;
        cameraInputActions.Camera.RotateLeft.performed += RotateLeft_performed;
        cameraInputActions.Camera.RotateRight.performed += RotateRight_performed;

        // Obtener el componente Transposer para manipular la cámara
        //transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        //Debug camera fov
        Debug.Log("Camera FOV: " + virtualCamera.m_Lens.FieldOfView);

    }

    private void OnDestroy()
    {
        cameraInputActions.Camera.Look.performed -= Look_performed;
        cameraInputActions.Camera.Zoom.performed -= Zoom_performed;
        cameraInputActions.Camera.RotateLeft.performed -= RotateLeft_performed;
        cameraInputActions.Camera.RotateRight.performed -= RotateRight_performed;

        cameraInputActions.Dispose();
    }

    private void Look_performed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Zoom_performed(InputAction.CallbackContext context)
    {
        zoomInput = context.ReadValue<float>();
    }

    private void RotateLeft_performed(InputAction.CallbackContext context)
    {
        RotateCamera(-rotationStep);
    }

    private void RotateRight_performed(InputAction.CallbackContext context)
    {
        RotateCamera(rotationStep);
    }

    private void Update()
    {
        //HandleLook();
        HandleZoom();
    }

    private void HandleZoom()
    {
        //check if zoom input changed from last frame
        //var zoomInput = cameraInputActions.Camera.Zoom.ReadValue<float>();

        if (Mathf.Abs(zoomInput) > 0.1f)
        {
            //float currentZoom = transposer.m_FollowOffset.z;
            //transposer.m_FollowOffset.z = Mathf.Clamp(currentZoom - zoomInput * zoomSpeed, zoomMin, zoomMax);
            //Ajustar el FOV de la cámara en función de la entrada del mouse
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView - zoomInput * zoomSpeed, zoomMin, zoomMax);
            zoomInput = 0;
        }
    }

    private void RotateCamera(float angle)
    {
        //Call coroutine to rotate the camera around the player using lerp to smooth the rotation
        StartCoroutine(RotateCameraCoroutine(angle));

        //rotate the camera around the player using lerp to smooth the rotation
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y + angle, 0), 0.1f);
    }

    private IEnumerator RotateCameraCoroutine(float angle)
    {
        float elapsedTime = 0;
        float duration = 1f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, transform.eulerAngles.y + angle, 0);

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }

}

using UnityEngine;

public class CanvasFaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Obtener la cámara principal al iniciar el script
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera != null)
        {
            // Hacer que el canvas mire hacia la cámara
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }
}

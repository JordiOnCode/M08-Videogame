using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float offset;

    void Update()
    {
        // Obtén la posición en el mundo de la parte superior de la cámara
        float topCameraWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.transform.position.z)).y;
        // Obtén la posición en el mundo de la parte inferior de la cámara
        float bottomCameraWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).y;

        // Si el personaje está por encima de la parte superior de la cámara...
        if (playerTransform.position.y > topCameraWorldPos)
        {
            // Actualiza la posición de la cámara para que esté a una distancia 'offset' por encima del personaje
            transform.position = new Vector3(transform.position.x, playerTransform.position.y - offset, transform.position.z);
        }
        // Si el personaje está por debajo de la parte inferior de la cámara...
        else if (playerTransform.position.y < bottomCameraWorldPos)
        {
            // Actualiza la posición de la cámara para que esté a una distancia 'offset' por debajo del personaje
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + offset, transform.position.z);
        }
    }
}

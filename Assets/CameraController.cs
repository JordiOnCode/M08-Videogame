using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float offset;

    void Update()
    {
        // Obt�n la posici�n en el mundo de la parte superior de la c�mara
        float topCameraWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.transform.position.z)).y;
        // Obt�n la posici�n en el mundo de la parte inferior de la c�mara
        float bottomCameraWorldPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).y;

        // Si el personaje est� por encima de la parte superior de la c�mara...
        if (playerTransform.position.y > topCameraWorldPos)
        {
            // Actualiza la posici�n de la c�mara para que est� a una distancia 'offset' por encima del personaje
            transform.position = new Vector3(transform.position.x, playerTransform.position.y - offset, transform.position.z);
        }
        // Si el personaje est� por debajo de la parte inferior de la c�mara...
        else if (playerTransform.position.y < bottomCameraWorldPos)
        {
            // Actualiza la posici�n de la c�mara para que est� a una distancia 'offset' por debajo del personaje
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + offset, transform.position.z);
        }
    }
}

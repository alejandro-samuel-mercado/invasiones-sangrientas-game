using UnityEngine;
using System.Collections;

public class MoveCameraRight : MonoBehaviour
{
    public float moveDistance = 2.0f;  // Distancia que la cámara se moverá hacia la derecha
    public float duration = 2.0f;      // Duración del movimiento en segundos
    public GameObject dialogBox;       // Cuadro de diálogo (Panel)
    public Camera mainCamera;          // Referencia a la cámara principal
    public Transform player;           // Referencia al jugador

    public bool cameraStopped = false; // Variable para indicar si la cámara se ha detenido
    public delegate void CameraStoppedHandler();
    public event CameraStoppedHandler OnCameraStopped;

    void Start()
    {
        // Ocultar el cuadro de diálogo al inicio
        if (dialogBox != null)
        {
            dialogBox.SetActive(false);
        }

        // Iniciar la corutina para mover la cámara
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, -moveDistance, 0);
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Interpolar la posición de la cámara
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la cámara esté en la posición final
        transform.position = endPosition;

        // Mostrar el cuadro de diálogo cuando la cámara se detenga
        if (dialogBox != null)
        {
            dialogBox.SetActive(true);
        }

        // Aumentar el tamaño de la cámara después de 5 segundos
        yield return new WaitForSeconds(5f);

        float originalSize = mainCamera.orthographicSize;
        float targetSize = originalSize + 2.0f;  // Aumenta el tamaño de la cámara
        elapsedTime = 0;

        while (elapsedTime < 2.0f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(originalSize, targetSize, elapsedTime / 2.0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;

        // Indicar que la cámara se ha detenido
        cameraStopped = true;

        // Notificar a los suscriptores que la cámara se ha detenido
        if (OnCameraStopped != null)
        {
            OnCameraStopped();
        }
    }

    void LateUpdate()
    {
        // Seguir al jugador después de que la cámara se haya detenido
        if (cameraStopped && player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }
    }
}

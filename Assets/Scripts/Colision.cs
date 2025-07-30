using UnityEngine;

[ExecuteInEditMode]
public class AddBoxCollidersInEditor : MonoBehaviour
{
    void Start()
    {
        // Solo ejecuta en el editor
        if (!Application.isPlaying)
        {
            // Encuentra todos los objetos con el tag "casas"
            GameObject[] casasObjects = GameObject.FindGameObjectsWithTag("casas");

            // Recorre cada objeto encontrado
            foreach (GameObject casa in casasObjects)
            {
                // Verifica si el objeto tiene hijos
                if (casa.transform.childCount > 0)
                {
                    // Recorre cada hijo del objeto
                    foreach (Transform child in casa.transform)
                    {
                        // Agrega un BoxCollider2D al hijo si no tiene ya uno
                        if (child.gameObject.GetComponent<BoxCollider2D>() == null)
                        {
                            child.gameObject.AddComponent<BoxCollider2D>();
                        }
                    }
                }
            }
        }
    }
}
 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class SelectCharacter : MonoBehaviour
{

    [SerializeField] private string sceneName = "Game";
    
    void Start()
    {
        // Obtiene el componente Button y asigna el evento
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("Este script requiere un componente Button", gameObject);
        }
    }

    public void LoadScene()
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"Escena '{sceneName}' no encontrada. Verifica Build Settings");
        }
    }
}
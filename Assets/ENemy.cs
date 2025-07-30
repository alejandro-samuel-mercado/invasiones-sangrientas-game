using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class ENemy : MonoBehaviour
{
  public GameObject player;
    public Tilemap enemyTilemap;
    public float activationDistance = 10.0f;

    private bool enemiesActivated = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Busca el GameObject del jugador por etiqueta
    }

    void Update()
    {
        // Verificar si los enemigos aún no han sido activados y el jugador está lo suficientemente cerca
        if (!enemiesActivated && player != null && Vector3.Distance(transform.position, player.transform.position) <= activationDistance)
        {
            ActivateEnemies();
        }
    }

    void ActivateEnemies()
    {
        // Iterar sobre los tiles del Tilemap de enemigos y activar los enemigos
        foreach (Vector3Int pos in enemyTilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if (enemyTilemap.HasTile(localPlace))
            {
                GameObject enemy = enemyTilemap.GetInstantiatedObject(localPlace);
                if (enemy != null)
                {
                    enemy.SetActive(true);
                    // Aquí podrías iniciar el movimiento del enemigo si es necesario
                    // Por ejemplo, obtener el componente EnemyController y llamar a un método para iniciar el movimiento
                    EnemyController enemyController = enemy.GetComponent<EnemyController>();
                    if (enemyController != null)
                    {
                        enemyController.StartMoving();
                    }
                }
            }
        }

        enemiesActivated = true;
    }
}

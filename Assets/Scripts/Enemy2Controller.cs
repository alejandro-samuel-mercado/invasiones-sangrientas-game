using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public Animator animacion;

    public Vector3 enemigoPos;
    public GameObject playerMov;
    public bool perseguirPlayer;
    public int vel;
    // Start is called before the first frame update
    void Start()
    {
        animacion = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ComportamientoEnemigo();
        if(perseguirPlayer){
            transform.position = Vector3.MoveTowards(transform.position, enemigoPos, vel * Time.deltaTime);
            animacion.SetBool("walk", true);
             if (Input.GetKeyDown("space"))
            {
               animacion.SetTrigger("death");

                // Eliminar el GameObject del enemigo después de un pequeño retardo
                Destroy(gameObject, 0.5f);
            }
        }else{
            animacion.SetBool("walk",false);
        }
    
      
    
    }

 void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador colisiona con el enemigo
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el script del jugador (suponiendo que tiene un script llamado PlayerController)
            Bandit playerController = collision.gameObject.GetComponent<Bandit>();

            // Verificar si el jugador está atacando
            if (playerController.IsAttacking())
            {
                // Ejecutar animación de muerte o cualquier efecto de eliminación del enemigo
                animacion.SetTrigger("death");

                // Eliminar el GameObject del enemigo después de un pequeño retardo
                Destroy(gameObject, 0.5f);
            }
        }
    }

    
    public void ComportamientoEnemigo()
    {
        cronometro += 1 * Time.deltaTime;
        if(cronometro >= 2)
        {
            rutina = Random.Range(0,2);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
            animacion.SetBool("attack", false);
            break;
            case 1: 
            rutina++;
            break;
            case 2: 
            animacion.SetBool("attack", true);
            break;
            default:
            animacion.SetBool("attack", false);
            break;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.tag.Equals("Player")){
            enemigoPos = playerMov.transform.position;
            perseguirPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        perseguirPlayer = false;
    }
}

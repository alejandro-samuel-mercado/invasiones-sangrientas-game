using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalWalk : MonoBehaviour
{
    private float speed;
    public int direccion;
    private Vector3[] posiciones;
    void Start()
    {
        speed = 1f;
        getInitialPosition();
        StartCoroutine("CorrutinaAnimal");
    }

    IEnumerator CorrutinaAnimal()
    {
        int i = 1;
        Vector3 nuevaPosicion = posiciones[i];
        while(true)
        {
            while(transform.position != nuevaPosicion)
            {
                transform.position = Vector3.MoveTowards(transform.position, nuevaPosicion, speed * Time.deltaTime);
                yield return null;
            }
            if(i < 1){
                i++;
                if(direccion == 1)
                {
                    Flip();
                }
            }
            else{
                i = 0;
                if(direccion == 1)
                {
                    Flip();
                }
            }
            nuevaPosicion = posiciones[i];
        }
    }

    private void getInitialPosition()
    {
        posiciones = new Vector3[2];
        posiciones[0] = transform.position;
        
        if(direccion ==  1)
        {
            posiciones[1] = new Vector3(transform.position.x - 2, transform.position.y, 0); 
        }
        else {
posiciones[1] = new Vector3(transform.position.x, transform.position.y - 2, 0); 
        }
        
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

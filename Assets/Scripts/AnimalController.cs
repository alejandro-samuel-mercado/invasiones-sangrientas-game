using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float moveRange = 5.0f;
    public Animator animator;

    private bool isMovingRight = true;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            if(transform.position.x >= initialPosition.x + moveRange)
            {
                isMovingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            if(transform.position.x <= initialPosition.x - moveRange)
            {
                isMovingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{   public float moveSpeed = 3.0f;
    public float moveRange = 5.0f;
    public Animator animator;

    private bool isMovingRight = true;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= initialPosition.x + moveRange)
            {
                isMovingRight = false;
                Flip();
                animator.SetTrigger("Idle");
            }
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

            if (transform.position.x <= initialPosition.x - moveRange)
            {
                isMovingRight = true;
                Flip();
                animator.SetTrigger("Idle");
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Método para iniciar el movimiento del enemigo
    public void StartMoving()
    {
        animator.SetTrigger("Walk");
    }
}

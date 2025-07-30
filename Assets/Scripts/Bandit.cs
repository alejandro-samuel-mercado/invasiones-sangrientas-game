using UnityEngine;

public class Bandit : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float autoMoveDistance = 15.0f;  // Distancia a moverse automáticamente al inicio
    [SerializeField] float autoMoveTime = 3.0f;      // Tiempo para moverse automáticamente

    private Animator m_animator;
       private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    private bool isAutoMoving = true;  // Bandera para movimiento automático
    private bool isPlayerControlEnabled = false; // Bandera para habilitar control del jugador
    private float startPositionY;      // Posición inicial en Y para calcular la distancia recorrida
    private MoveCameraRight cameraScript;  // Referencia al script de la cámara
public bool isAttacking = false; // Variable para contr
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        startPositionY = transform.position.y;
 m_body2d = GetComponent<Rigidbody2D>();
        // Deshabilitar gravedad
        m_body2d.gravityScale = 0;
   m_body2d.bodyType = RigidbodyType2D.Kinematic;
        // Obtener referencia al script de la cámara
        cameraScript = Camera.main.GetComponent<MoveCameraRight>();

        // Asignar la referencia del jugador al script de la cámara
        cameraScript.player = this.transform;

        // Movimiento automático inicial
        float autoMoveSpeed = autoMoveDistance / autoMoveTime;  // Calcular la velocidad necesaria
        m_body2d.velocity = new Vector2(0, autoMoveSpeed);  // Establecer la velocidad del movimiento automático
        m_animator.SetInteger("AnimState", 2); // Usar la animación de correr

        // Detener el movimiento automático después del tiempo especificado
        Invoke("StopAutoMove", autoMoveTime);
            Invoke("EnableDynamicRigidbody", 10.0f);
    }
   void EnableDynamicRigidbody()
    {
        m_body2d.bodyType = RigidbodyType2D.Dynamic; // Cambiar a Rigidbody2D dinámico después de 10 segundos
    }
    void StopAutoMove()
    {
        isAutoMoving = false;
        m_body2d.velocity = Vector2.zero;
        m_animator.SetInteger("AnimState", 0); // Cambiar a estado idle
        Invoke("EnablePlayerControl", 3.0f); // Esperar 3 segundos antes de habilitar el control del jugador
    }

    void EnablePlayerControl()
    {
        isPlayerControlEnabled = true; // Permitir el control del jugador
    }

    void Update()
    {
        // Movimiento automático hacia arriba
        if (isAutoMoving)
        {
            if (transform.position.y < startPositionY + autoMoveDistance)
            {
                // El jugador se sigue moviendo automáticamente hacia arriba
                return; // Salir de Update mientras se mueve automáticamente
            }
            else
            {
                StopAutoMove();
            }
        }

        // No permitir el movimiento del jugador hasta que el control esté habilitado
        if (!isPlayerControlEnabled) return;

        // Verificar si el personaje acaba de aterrizar
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // Verificar si el personaje acaba de empezar a caer
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        // No permitir el movimiento del jugador hasta que la cámara se haya detenido
        if (!cameraScript.cameraStopped) return;

        // Manejar entrada y movimiento después del movimiento automático
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Cambiar dirección del sprite según la dirección del movimiento
        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Mover horizontal y verticalmente
        m_body2d.velocity = new Vector2(inputX * m_speed, inputY * m_speed);

        // Establecer AirSpeed en el animador
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // Manejar animaciones
        if (m_isDead)
        {
            if (Input.GetKeyDown("e"))
            {
                m_animator.SetTrigger("Recover");
                m_isDead = false;
            }
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                m_animator.SetTrigger("Death");
                m_isDead = true;
            }
            else if (Input.GetKeyDown("q"))
            {
                m_animator.SetTrigger("Hurt");
            }
            else if (Input.GetKeyDown("space"))
            {
                m_animator.SetTrigger("Attack");
                isAttacking = true;
            }
            else if (Input.GetKeyDown("f"))
            {
                m_combatIdle = !m_combatIdle;
            }
            else if (Input.GetKeyDown("space") && m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }
            else if (Mathf.Abs(inputX) > Mathf.Epsilon || Mathf.Abs(inputY) > Mathf.Epsilon)
            {
                m_animator.SetInteger("AnimState", 2); // Correr

                // Asegurar que se enfrenta en la dirección correcta
                if (inputX != 0)
                {
                    transform.localScale = new Vector3(inputX > 0 ? -1.0f : 1.0f, 1.0f, 1.0f);
                }
            }
            else if (m_combatIdle)
            {
                m_animator.SetInteger("AnimState", 1); // Combate Idle
            }
            else
            {
                m_animator.SetInteger("AnimState", 0); // Idle
            }
        }
    }


    public bool IsAttacking()
    {
        return isAttacking;
    }
}

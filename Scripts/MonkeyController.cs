using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class MonkeyController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpStrength = 10f;
    private Rigidbody2D rigidbody2D;
    private Vector2 m_Move;
    private HashSet<Collider2D> touchedGround;
    public float maxSpeed = 10;
    public GameObject deathEffectPrefab;

    public Animator anim;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        touchedGround = new HashSet<Collider2D>();
    }

    public void OnMove(InputValue value)
    {
        m_Move = value.Get<Vector2>();
        //Debug.Log(m_Move.x);
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("vertical", vertical);
        // Move
        rigidbody2D.AddForceX(m_Move.x * speed * Time.deltaTime);
        rigidbody2D.linearVelocityX = Mathf.Clamp(rigidbody2D.linearVelocityX, -maxSpeed, maxSpeed);
        // Jump
        if (m_Move.y > 0.5f && touchedGround.Count > 0)
        {
            bool canJump = false;
            foreach(Collider2D go in touchedGround)
            {
                if (rigidbody2D.IsTouching(go))
                {
                    canJump = true;
                }
            }
            if (canJump)
            {
                rigidbody2D.AddForceY(jumpStrength);
            }
            touchedGround = new HashSet<Collider2D>();
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        foreach(var contact in col.contacts)
        {
            //Debug.Log(contact.normal);
            //Debug.Log(contact.collider.gameObject);
            if(contact.normal.y > 0.9f)
            {
                touchedGround.Add(contact.collider);
            }
        }
    }
    public void Die()
    {
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        rigidbody2D.Sleep();
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        GameOverText.instance.GameOver();
        Destroy(this);
    }
}

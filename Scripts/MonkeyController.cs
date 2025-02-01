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
        // Move
        rigidbody2D.AddForceX(m_Move.x * speed * Time.deltaTime);

        // Jump
        if(m_Move.y > 0.5f)
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
                touchedGround = new HashSet<Collider2D>();
            }
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
}

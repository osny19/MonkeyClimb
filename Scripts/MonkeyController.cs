using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.Mathematics;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class MonkeyController : MonoBehaviour
{
    public float speed = 3f;
    public float jumpStrength = 10f;
    public float climbSpeed = 5000f;
    private Rigidbody2D rigidbody2D;
    private Vector2 m_Move;
    private HashSet<Collider2D> touchedGround;
    public float maxSpeed = 10;
    public GameObject deathEffectPrefab;
    public GameObject deathScreem;
    public GameObject eatBananaSound;
    public int bananaCount = 0; // Tracks collected bananas
    public TMP_Text Bananas; // Assign UI Text in Inspector
    public bool inVine = false;
    private float score = 0f;
    private float maxHeight = 0f;
    public TMP_Text scoreText; // Assign in Inspector
    private bool throwingBanana = false;
    public GameObject ThrowBanana;

    public GameObject scoreHandler;
    



    public Animator anim;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        touchedGround = new HashSet<Collider2D>();
        UpdateBananaDisplay();

    }

    public void OnMove(InputValue value)
    {
        m_Move = value.Get<Vector2>();
        //Debug.Log(m_Move.x);
    }

    void Update()
    {
        UpdateScore();
        if (inVine && rigidbody2D.linearVelocityY < 0) {
            rigidbody2D.gravityScale = 0;
        } else {
            rigidbody2D.gravityScale = 1;
        }

        if (inVine) {
            rigidbody2D.linearVelocityY = m_Move.y * climbSpeed * Time.deltaTime;
        }

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
            if (canJump && !(inVine))
            {
                rigidbody2D.AddForceY(jumpStrength);
            }
            touchedGround = new HashSet<Collider2D>();
        }
        handleThrowing();
    }
    void handleThrowing()
    {
        if (bananaCount > 0)
        {


            if (Input.GetKeyDown(KeyCode.Space))
            {

                // position of spanwed object could be 'GetPoint(0).. 1.. 2' half random choice ;)
                GameObject banana = Instantiate(ThrowBanana);
                banana.transform.position = gameObject.transform.position;
                float speed = 10f;
                banana.transform.Translate(Vector2.down * speed * Time.deltaTime);
                rigidbody2D.AddForceY(jumpStrength);
                Destroy(banana, 3);
                bananaCount--;
                UpdateBananaDisplay();

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

    public void CollectBanana()
    {
   
        bananaCount++;
        UpdateBananaDisplay();
        Instantiate(eatBananaSound);
       
        
    }

    public void UpdateScore()
    {
        if (transform.position.y > maxHeight) // Update max height
        {
            maxHeight = transform.position.y;
            UpdateScoreDisplay();
        }
    }
    public void Die()
    {
        Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Instantiate(deathScreem);
        rigidbody2D.Sleep();
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        GameOverText.instance.GameOver();
        scoreHandler.GetComponent<ScoreHandler>().addScore();
        Destroy(this);
    }

    void UpdateBananaDisplay()
    {
        Bananas.text = bananaCount.ToString();
    }
    void UpdateScoreDisplay()
    {
        scoreText.text = Mathf.FloorToInt(maxHeight) + " m";
    }


}

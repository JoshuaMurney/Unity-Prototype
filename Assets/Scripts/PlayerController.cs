using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text scoreText;
    public SpriteRenderer spriteRenderer;
    public Sprite Lazarus;
    public Sprite Bat;

    [SerializeField] private LayerMask ground;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private int score;
    private bool batForm;
    private float speed;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        score = 0;
        scoreText.text = "Score: " + score.ToString();
        batForm = false;
        speed = 10f;
    }

    void Update()
    {
        float jumpVel = 20f;
        float walkVel = speed;
        float flyVel = speed;
        if (onGround() && Input.GetKeyDown(KeyCode.W) && !(batForm))
                rigidBody2D.velocity = Vector2.up * jumpVel;

        if (Input.GetKey(KeyCode.A))
            rigidBody2D.velocity = new Vector2(-walkVel, rigidBody2D.velocity.y);
        else
        {
            if (Input.GetKey(KeyCode.D))
                rigidBody2D.velocity = new Vector2(walkVel, rigidBody2D.velocity.y);
            else
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
        }

        if (batForm)
        {
            if (Input.GetKey(KeyCode.W))
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, flyVel);
            else
            {
                if (Input.GetKey(KeyCode.S))
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, -flyVel);
                else
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (batForm)
            {
                batForm = false;
                rigidBody2D.gravityScale = 5;
                speed = 10f;
                spriteRenderer.sprite = Lazarus;
                boxCollider2D.size = new Vector2(0.4f, 1.25f);
            }
            else
            {
                batForm = true;
                rigidBody2D.gravityScale = 0;
                speed = 7f;
                spriteRenderer.sprite = Bat;
                boxCollider2D.size = new Vector2(0.4f, 0.3f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Pickups"))
        {
            c.gameObject.SetActive(false);
            score += 100;
            scoreText.text = "Score: " + score.ToString();
        }
    }

    private bool onGround()
    {
        RaycastHit2D raycastHit2DBox = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, ground);
        return raycastHit2DBox.collider != null;
    }
}

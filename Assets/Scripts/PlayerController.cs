using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text scoreText;

    [SerializeField] private LayerMask ground;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCollider2D;
    private int score;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        score = 0;
        scoreText.text = "Score: " + score.ToString();
    }

    void Update()
    {
        float jumpVel = 20f;
        float walkVel = 10f;
        if (onGround() && Input.GetKeyDown(KeyCode.W))
        {
            rigidBody2D.velocity = Vector2.up * jumpVel;
        }

        if (Input.GetKey(KeyCode.A))
            rigidBody2D.velocity = new Vector2(-walkVel, rigidBody2D.velocity.y);
        else
        {
            if (Input.GetKey(KeyCode.D))
                rigidBody2D.velocity = new Vector2(walkVel, rigidBody2D.velocity.y);
            else
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
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

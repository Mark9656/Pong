using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Ball : MonoBehaviour
{
    private int score;
    private int maxScore;
    public Rigidbody2D rigidbody2D;
    public Vector2 lastVelocity;
    public Score Score;
    public movement LeftPlayer;
    public movement RightPlayer;

    public UIController uIController;
    private int LeftPlayerScore;
    private int RightPlayerScore;
    private SpriteRenderer spriteRenderer; 

 
    void Start()
    {
        maxScore = PlayerPrefs.GetInt("MaximumLimit");
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on the Ball object!");
        }
        SendBallInRandomDirection();
    }

    public void SendBallInRandomDirection()
    {
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.isKinematic = true;
        transform.position = Vector3.zero;
        rigidbody2D.isKinematic = false;
        rigidbody2D.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 5f;
        lastVelocity = rigidbody2D.velocity;

        LeftPlayer.speed = LeftPlayer.defaultSpeed;
        RightPlayer.speed = RightPlayer.defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SendBallInRandomDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2D.velocity = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
        lastVelocity = rigidbody2D.velocity * 1.1f;
        LeftPlayer.speed *= 1.1f;
        RightPlayer.speed *= 1.1f;

        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.GetComponent<movement>() != null)
        {
            Debug.Log("Collision with Player detected, changing color.");
            ChangeColor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            LeftPlayerScore++;
            uIController.SetLeftPlayerScore(LeftPlayerScore.ToString());
            Debug.Log("Left Player Scored");
        }
        if (transform.position.x < 0)
        {
            RightPlayerScore++;
            uIController.SetRightPlayerScore(RightPlayerScore.ToString());
            Debug.Log("Right Player Scored");
        }
        SendBallInRandomDirection();
    }

    // change color
    void ChangeColor()
    {
        if (spriteRenderer != null)
        {
            Color newColor = new Color(Random.value, Random.value, Random.value);
            spriteRenderer.color = newColor;
        }
    }
}

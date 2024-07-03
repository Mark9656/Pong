using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class boll : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float speed = 6f;
    public uimeneger uimeneger;

    public int LeftPlayerScore;
    public int RightPlayerScore;


    public static event Action BallReset;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        SendBallInRandomDirection();

    }

    private void SendBallInRandomDirection()
    {
        BallReset?.Invoke();

        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.isKinematic = true;
        transform.position = Vector3.zero;
        rigidbody2D.isKinematic = false;

        Vector2 newBallVector = new Vector2();
        newBallVector.x = Random.Range(-1f, 1f);
        newBallVector.y = Random.Range(-1f, 1f);
        rigidbody2D.velocity = newBallVector.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<movement>() == null)
            return;
        collision.gameObject.GetComponent<movement>().speed *= 1.1f;
        rigidbody2D.velocity *= 1.1f;
         rigidbody2D.velocity *= 1.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SendBallInRandomDirection();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (transform.position.x > 0)
        {
            Debug.Log("Player Left +1");
            LeftPlayerScore++;
            uimeneger.SetLeftPlayerScoreText(LeftPlayerScore.ToString());
        }
        else
        {
            Debug.Log("Player Right +1");
            RightPlayerScore++;
            uimeneger.SetRightPlayerScoreText(RightPlayerScore.ToString());
        }
        SendBallInRandomDirection();
    }
 
}

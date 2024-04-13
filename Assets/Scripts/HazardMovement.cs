using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HazardMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;

    private bool lookingRight = true;
    private float dirX = 0f;
    private float movementSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Flip();
    }

    // constant movement
    private void Movement()
    {
        if (lookingRight)
        {
            dirX = 1f;
        }
        else
        {
            dirX = -1f;
        }
        rb.velocity = new Vector3(dirX * movementSpeed, rb.velocity.y);
    }

    // flip sprite
    private void Flip()
    {
        if (lookingRight && dirX < 0f || !lookingRight && dirX > 0f)
        {
            lookingRight = !lookingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // change direction when hitting wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            lookingRight = !lookingRight;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CController : MonoBehaviour
{
    Animator an;
    SpriteRenderer sr;
    Rigidbody2D rb;

    public float SPEED = 6.0f;
    public float JUMP_FORCE = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 oldVelocity = rb.velocity;
        rb.velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            an.SetBool("isRunning", true);
            rb.velocity += new Vector2(SPEED, 0.0f);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            an.SetBool("isRunning", true);
            rb.velocity += new Vector2(-SPEED, 0.0f);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            // Check if coliding with the ground
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.down, 1.0f);
            foreach (RaycastHit2D h in hit)
            {
                if (h.collider.tag == "Ground")
                {
                    rb.AddForce(Vector2.up * JUMP_FORCE, ForceMode2D.Impulse);
                }
            }

        }

        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            an.SetBool("isRunning", false);
        }

    }
}

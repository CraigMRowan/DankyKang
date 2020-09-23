using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(xInput * moveSpeed, rigidBody.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        if (Input.GetButtonDown("Jump") && IsGrounded())
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        if (rigidBody.velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (rigidBody.velocity.x < 0)
            spriteRenderer.flipX = true;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.1f, 0), Vector2.down, 0.2f);
        return hit.collider != null;
    }
}

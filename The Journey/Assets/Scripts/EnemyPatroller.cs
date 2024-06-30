using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatroller : MonoBehaviour
{
    [SerializeField]
    LayerMask bouncingWalls = 6;

    [SerializeField]
    float speed;

    [SerializeField]
    Transform leftCollisionCheck, rightCollisionCheck;


    [Range(0, .3f)][SerializeField] private float MovementSmoothing = .05f;

    Rigidbody2D rb;
    private Vector3 Velocity = Vector3.zero;

    int direction = 1;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (CheckForWallCollision())
            direction *= -1;
        Move();
    }

    private bool CheckForWallCollision()
    {
        var colliders = Physics2D.OverlapCircleAll(leftCollisionCheck.position, 0.05f, bouncingWalls);
        if (colliders.Length > 0 && direction < 0) return true;

        colliders = Physics2D.OverlapCircleAll(rightCollisionCheck.position, 0.05f, bouncingWalls);
        return colliders.Length > 0 && direction > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log($"<color=cyan>Collision Enter {collision.gameObject.layer}</color>");
        if (collision.gameObject.layer == bouncingWalls)
        {
            Debug.Log($"<color=lime>Direction Change</color>");

            direction *= -1;
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {

                Debug.Log($"<color=red>GameButtons over</color>");
            }
        }
    }

    private void Move()
    {
        Vector3 targetVelocity = new Vector2(direction * speed * Time.fixedDeltaTime * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref Velocity, MovementSmoothing);
    }
}

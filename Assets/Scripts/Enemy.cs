/*using System;*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //configuration parameters
    [Range(0, 5)] [SerializeField] private float enemySpeed = 2.5f;    

    //cached references
    private Rigidbody2D rigidbody2D;
    private CapsuleCollider2D bodyCollider2D;
    private BoxCollider2D shapeCollider2D;
    private float movingVector;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        shapeCollider2D = GetComponent<BoxCollider2D>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        movingVector = Mathf.Sign(UnityEngine.Random.Range(-2, 1));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
    }

    private void FlipSprite()
    {
        bool hasHorizontalMovement = (Mathf.Abs(rigidbody2D.velocity.x)>0);

        if (hasHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x)*-1, 1f);
        }
    }

    private void Move()
    {
        if (!bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            movingVector *= -1;
            return;
        }

        Vector2 newVelocity = new Vector2(movingVector * enemySpeed, 0.0f);
        rigidbody2D.velocity = newVelocity;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        movingVector *= -1;
    }
    
}
